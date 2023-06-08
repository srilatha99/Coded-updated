using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodedWebTest.Data;
using Microsoft.AspNetCore.Mvc;
using CodedWebTest.Entities;
using CodedWebTest.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


namespace CodedWebTest.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly WebTestDBContext _ctx;
        private readonly ISessionDataService _sessionData;
        private ILogger _logger;


        
        public HomeController(WebTestDBContext context, ISessionDataService sessionData) 
        {
            _ctx = context;
            _sessionData = sessionData;
            // TODO: Get WebTestDBContext via Dependency Injection
            // TODO: Get SessionData via Dependency Injection
        }

        public IActionResult Index()
        {
            return View();
        }

        
        // TODO: Check if email exists in the database
        [HttpPost]
        public async Task<IActionResult> CheckEmail(string email)
        {

            using (var _ctx = new WebTestDBContext())
            {
                var existingEmail = await _ctx.EmailAddress.FirstOrDefaultAsync(e => e.Address == email);

                if (existingEmail != null)
                {
                    // Email already exists in the database
                    return View("EmailExists"); 
                }
                else
                {
                    // Email does not exist in the database
                    return View("EmailDoesNotExist");
                }
            }
        }


        // TODO: Save email to the database and user session
        [HttpPost]
        public async Task<IActionResult> SaveEmail(string email)
        {
            using (var ctx = new WebTestDBContext())
            {
                // Save email to the database
                var newEmail = new EmailAddress
                {
                    EmailAddressUid = Guid.NewGuid(),
                    Address = email,
                    CreatedDate = DateTime.Now
                };
                ctx.EmailAddress.Add(newEmail);
                await ctx.SaveChangesAsync();
            }

            // Save email to user session
            //HttpContext.Session.SetString("Email", email);
            _sessionData.EmailAddress = email;

            return RedirectToAction("Index", "Home");
        }

        // TODO: Get saved email from session
        public async Task<IActionResult> GetEmail()
        {
            //string email = HttpContext.Session.GetString("Email");
            string email = _sessionData.EmailAddress;

            ViewBag.Email = email; // Set the email value in ViewData

            return View("DisplayEmail");
        }
    }
}
