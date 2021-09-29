using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CodedWebTest.Data;
using CodedWebTest.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CodedWebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebTestDBContext _ctx;

        public HomeController() 
        {
            // TODO: Get WebTestDBContext via Dependency Injection
        }

        public IActionResult Index()
        {
            return View();
        }

        // TODO: Check if email exists in the database
        public async Task<IActionResult> CheckEmail() => throw new NotImplementedException();

        // TODO: Save email to the database and user session
        public async Task<IActionResult> SaveEmail() => throw new NotImplementedException();

        // TODO: Get saved email from session
        public async Task<IActionResult> GetEmail() => throw new NotImplementedException();
    }
}
