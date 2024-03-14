using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomStringGeneratorApi.DataBaseContext;
using RandomStringGeneratorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomStringGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomStringsController : ControllerBase
    {
        private readonly RandomStringContext _context;
        public RandomStringsController(RandomStringContext context)
        {
            _context = context;
        }
        [HttpPost]
        public ActionResult<IEnumerable<string>> GenerateRandomStrings([FromBody] RandomStringInput request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var random = new Random();
                var allowedChars = new StringBuilder();
                if (request.AllowNumericDigits)
                    allowedChars.Append("0123456789");

                if (request.AllowUppercaseLetters)
                    allowedChars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

                if (request.AllowLowercaseLetters)
                    allowedChars.Append("abcdefghijklmnopqrstuvwxyz");
                var strings = Enumerable.Range(0, request.NumberOfStrings)
                            .Select(_ => GenerateRandomString(request.NumberOfCharacters, allowedChars, random));

                if (!request.UniqueStrings)
                {
                    foreach (var item in strings)
                    {
                        _context.Add(new RandomString
                        {
                            Value = item,
                            CreatedAt = DateTime.Now
                        });
                    }
                    return Ok(strings);
                }
                else
                {
                    foreach (var item in strings)
                    {
                        _context.Add(new RandomString
                        {
                            Value = item,
                            CreatedAt = DateTime.Now
                        });
                    }
                    return Ok(strings.Distinct());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while generating random strings.");
            }

        }

        private string GenerateRandomString(int length, StringBuilder allowedChars, Random random)
        {
            return new string(Enumerable.Range(0, length)
                .Select(_ => allowedChars[random.Next(allowedChars.Length)]).ToArray());
        }
    }
}
