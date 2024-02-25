using ShowcaseWebdev.Models;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics;
using System.Xml.Linq;

namespace ShowcaseWebdev
{
    public class ContactHandler
    {
        static string APIKey = "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";
        public static async Task<bool> SendMail(Mail mail)
        {
            /*var apiKey = APIKey;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("agricolajamie@gmail.com", $"{mail.FirstName + " " + mail.LastName}"),
                Subject = "Showcase",
                PlainTextContent = $"phone: {mail.Phonenumber} \nemail: {mail.Email}"
            };
            msg.AddTo(new EmailAddress("agricolajamie@gmail.com", "Jamie Agricola"));
            try
            {
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine(response.StatusCode);
                // Process response...
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }*/

            var apiKey = APIKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("agricolajamie@gmail.com", "Example User");
            var subject = "Your Reservation Details";
            var to = new EmailAddress("agricolajamie@gmail.com", "Example User");

            // Create the email message
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: "Je string hier 2", htmlContent: null);

            // Send the email
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

        public async Task<bool> HandleFormSubmission(Mail mail)
        {
            if (ValidateFields(mail))
            {
                return await SendMail(mail);
            }
            else
            {
                return false;
            }
        }

        private static bool ValidateFields(Mail mail)
        {
            mail.FirstName = SanitizeInput(mail.FirstName);
            mail.LastName = SanitizeInput(mail.LastName);
            mail.Phonenumber = SanitizeInput(mail.Phonenumber);
            mail.Email = SanitizeInput(mail.Email);

            if (string.IsNullOrWhiteSpace(mail.FirstName) ||
                string.IsNullOrWhiteSpace(mail.LastName) ||
                mail.FirstName.Length > 60 || 
                mail.LastName.Length > 60 ||
                !Regex.IsMatch(mail.Phonenumber, @"^\d{10}$") ||
                string.IsNullOrWhiteSpace(mail.Email) ||
                string.IsNullOrWhiteSpace(mail.Phonenumber) ||
                !IsValidEmail(mail.Email))
            {
                return false;
            }
            return true;
        }

        private static string SanitizeInput(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
