using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomStringGeneratorApi.Models
{
    public class RandomStringInput
    {
        public int NumberOfStrings { get; set; }
        public int NumberOfCharacters { get; set; }
        public bool AllowNumericDigits { get; set; }
        public bool AllowUppercaseLetters { get; set; }
        public bool AllowSpecialLetters { get; set; }
        public bool AllowLowercaseLetters { get; set; }
        public bool UniqueStrings { get; set; }
        public bool IdenticalStringsAllow { get; set; }
    }
}
