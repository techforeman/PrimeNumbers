using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbers.API.Models
{
    public class ResultXml
    {
     public int Id { get; set; }
        public int MinRangeData { get; set; }
        public int MaxRangeData { get; set; }

        
        public string ResultValues { get; set; }
        public string DateOfStart { get; set; }
        public string DateOfEnd { get; set; }
        public string UserName { get; set; }

    }
}
