using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PrimeNumbers.API.Controllers.HubConfig;
using PrimeNumbers.API.HubConfig;
using PrimeNumbers.API.Models;

namespace PrimeNumbers.API.Data
{
    public class ResultsRepositoryXML : IResultsRepositoryXML
    {
    private List<ResultXml> results = new List<ResultXml>();
    private int iNumberOfEntries = 1;
        private IProgressHub _hub;
        private IHostingEnvironment _env;
        private XDocument doc;
        

        public ResultsRepositoryXML(IHostingEnvironment env, IProgressHub hub)
        {
            _hub = hub;
            _env = env;
            doc = XDocument.Load(_env.ContentRootPath + "\\ResultsData.xml");
            foreach (var node in doc.Descendants("ResultXML"))
            {
            results.Add(new ResultXml
            {
            Id = Int32.Parse(node.Descendants("id").FirstOrDefault().Value),
            MinRangeData = Int32.Parse(node.Descendants("min_range").FirstOrDefault().Value),
            MaxRangeData = Int32.Parse(node.Descendants("max_range").FirstOrDefault().Value),
            DateOfStart = node.Descendants("date_of_start").FirstOrDefault().Value,
            DateOfEnd = node.Descendants("date_of_end").FirstOrDefault().Value,
            ResultValues = node.Descendants("result").FirstOrDefault().Value,
            UserName = node.Descendants("username").FirstOrDefault().Value
            });
            }

            iNumberOfEntries = results.Count;
            
        }

        public ResultXml AddResult(int minRange, int maxRange, string username, CancellationToken cancellationToken)
        {
            ResultXml res = new ResultXml();
            DateTime dateOfStart = DateTime.Now;
            res.DateOfStart = dateOfStart.ToString();
            string strPrimeValues = "";
            for (int i = minRange; i <= maxRange; i++)
            {
                _hub.SendToAll(i.ToString());
                Console.WriteLine(i.ToString());
                var isPrime = CheckIsPrimeAsync(i);
                if (isPrime==true)
                {
                   strPrimeValues = strPrimeValues + $"{i}; ";
                }
            }
            res.ResultValues = strPrimeValues.TrimEnd();
            res.MinRangeData = minRange;
            res.MaxRangeData = maxRange;
            DateTime dateOfEnd = DateTime.Now;
            res.DateOfEnd = dateOfEnd.ToString();
            res.UserName = username;
            
           res.Id = iNumberOfEntries++;
           XElement newNode = new XElement("ResultXML");

           XElement id = new XElement("id"); 
           id.Value = res.Id.ToString();

           XElement minRangeData = new XElement("min_range");
           minRangeData.Value = res.MinRangeData.ToString();

           XElement maxRangeData = new XElement("max_range");
           maxRangeData.Value = res.MaxRangeData.ToString();

           XElement date_of_start = new XElement("date_of_start");
           date_of_start.Value = res.DateOfStart.ToString();

           XElement date_of_end = new XElement("date_of_end");
           date_of_end.Value = res.DateOfEnd.ToString();

           XElement result = new XElement("result");
           result.Value = res.ResultValues.ToString();

           XElement usernamexml = new XElement("username");
           usernamexml.Value = res.UserName.ToString();

           newNode.Add(id, minRangeData, maxRangeData, date_of_start, date_of_end, result, usernamexml);
           doc.Root.Add(newNode); 
           SaveXMLFile();

         return res;

        }

        public bool DeleteResult(int id)
        {
            doc.Root.Descendants("ResultXML").Where(n => Int32.Parse(n.Descendants("id").First().Value ) == id).Remove() ;
            SaveXMLFile();
            return true;
        }

      

        public IEnumerable<ResultXml> GetAllResults()
        {
            return results;
        }

         public void SaveXMLFile()
        {
            doc.Save(_env.ContentRootPath + "\\ResultsData.xml");
            Console.WriteLine(_env.ContentRootPath + "\\ResultsData.xml");
        }


        private bool CheckIsPrimeAsync(int number)
        {
           
            if (number <= 1) return false;
            if (number == 2) return true;
            if(number % 2 == 0) return false;
            var boundary = (int)Math.Floor(Math.Sqrt(number));
            for (int i= 3; i<=boundary; i+=2 )
            {
                if (number % i == 0)
                    return false;
            }
                
            return true;        
            
        }
    }

    
}