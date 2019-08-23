using System.IO;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using PrimeNumbers.API.Models;
using static PrimeNumbers.API.Models.ResultXml;

namespace PrimeNumbers.API.Data
{

    public interface IDataXmlService
    {
        ResultXml[] GetResults();
    }
    public class DataXmlService : IDataXmlService
    {
        private readonly IHostingEnvironment _env;
        public DataXmlService(IHostingEnvironment env)
        {
            _env = env;
        }
        public ResultXml[] GetResults()
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataResults));
            FileStream myFileStream = new FileStream(_env.ContentRootPath + "\\ResultsData.xml", FileMode.Open);
            return ((DataResults)ser.Deserialize(myFileStream)).ResultsXML;
        }
    }
}
