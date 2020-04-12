using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedbackMessages.Example.AspNetCore.Mvc.Models;
using FeedbackMessages.Extensions;

namespace FeedbackMessages.Example.AspNetCore.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            this.InfoMessage("Information message.");
            this.SuccessMessage("Success message.");
            this.WarnMessage("Warning message.");
            this.ErrorMessage("Error message.");

            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
