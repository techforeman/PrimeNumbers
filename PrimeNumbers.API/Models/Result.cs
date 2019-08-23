using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbers.API.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int MinRangeData { get; set; }
        public int MaxRangeData { get; set; }

        
        public string ResultValues { get; set; }
        public DateTime DateOfStart { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string UserName { get; set; }

    }
}
