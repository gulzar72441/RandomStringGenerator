using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomStringGeneratorApi.Models
{
    public class RandomString
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
