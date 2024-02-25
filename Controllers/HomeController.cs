using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShowcaseWebdev.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShowcaseWebdev.Controllers
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

        public IActionResult Contact()
        {
            string csrfToken = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("CSRFToken", csrfToken);
            ViewBag.CsrfToken = csrfToken; // Pass CSRF token to view bag
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Form(string FirstName, string LastName, string Email, string Phonenumber, string g_recaptcha_response, string csrfToken)
        {
            // Verify reCAPTCHA token
            var recaptchaSecretKey = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";
            var recaptchaUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={recaptchaSecretKey}&response={g_recaptcha_response}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(recaptchaUrl);
                var responseContent = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(responseContent);

                if (!(bool)json.success)
                {
                    // reCAPTCHA verification failed
                    return BadRequest("reCAPTCHA verification failed");
                }
            }

            string storedCsrfToken = HttpContext.Session.GetString("CSRFToken");

            // Validate CSRF token
            if (csrfToken != storedCsrfToken)
            {
                // CSRF token validation failed
                return BadRequest("CSRF token validation failed");
            }

            // reCAPTCHA verification successful, proceed with form submission
            Mail mail = new Mail(FirstName, LastName, Email, Phonenumber);
            if (mail == null || string.IsNullOrEmpty(mail.FirstName) || string.IsNullOrEmpty(mail.LastName) || string.IsNullOrEmpty(mail.Email) || string.IsNullOrEmpty(mail.Phonenumber))
            {
                return BadRequest("Form data is incomplete");
            }

            var contactHandler = new ContactHandler();
            bool success = await contactHandler.HandleFormSubmission(mail);
            if (success)
            {
                return Ok("Email sent successfully");
            }
            else
            {
                return BadRequest("Failed to send email");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
