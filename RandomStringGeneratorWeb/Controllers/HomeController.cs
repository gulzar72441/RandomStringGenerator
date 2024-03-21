using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomStringGeneratorWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RandomStringGeneratorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateRandomStrings(RandomStringRequest formData)
        {
            var _httpClient = new HttpClient();
            var requestBody = new
            {
                NumberOfStrings = formData.NumberOfStrings,
                NumberOfCharacters = formData.NumberOfCharacters,
                AllowNumericDigits = formData.AllowNumericDigits,
                AllowUppercaseLetters = formData.AllowUppercaseLetters,
                AllowLowercaseLetters = formData.AllowLowercaseLetters,
                UniqueStrings = formData.UniqueStrings,
                IdenticalStringsAllow = formData.IdenticalStringsAllow,
                AllowSpecialLetters = formData.AllowSpecialLetters
            };
            var apiUrl = "https://localhost:44345/api/RandomStrings"; // Replace with your API endpoint
            try
            {
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Read response content as string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response
                    var responseData = JsonSerializer.Deserialize<List<string>>(responseContent);

                    return Json(responseData);
                }
                else
                {
                    return BadRequest("Failed to generate random strings");
                }

                // Return response data

            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                return BadRequest("Failed to communicate with the external API.");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GenerateStrings()
        {
            var _httpClient = new HttpClient();
            var apiUrl = "https://localhost:44345/api/RandomStrings/getstrings"; // Replace with your API endpoint
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read response content as string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response
                    var responseData = JsonSerializer.Deserialize<List<string>>(responseContent);

                    return Json(responseData);
                }
                else
                {
                    return BadRequest("Failed to generate random strings");
                }

                // Return response data

            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it as needed
                return BadRequest("Failed to communicate with the external API.");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
