using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedbackMessages.Example.AspNetCore.Mvc.Models;
using FeedbackMessages.Extensions;
using FeedbackMessages.Example.Mvc.Models;

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

        [HttpPost]
        public ActionResult Index(MessageModel model, string submitButton)
        {
            this.InfoMessage(model.Message);

            if (ModelState.IsValid)
            {
                switch (submitButton)
                {
                    case "SecondPage":
                        return RedirectToAction("SecondPage");
                    default:
                        return View();
                }
            }

            return View(model);
        }

        public ActionResult SecondPage()
        {
            this.InfoMessage("SecondPage");
            return View();
        }

        public ActionResult AjaxFeedbackMessage()
        {

            this.InfoMessage("Ajax Information message.");
            this.SuccessMessage("Ajax Success message.");
            this.WarnMessage("Ajax Warning message.");
            this.ErrorMessage("Ajax Error message.");


            var messageHtml = FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString();

            return new ContentResult()
            {
                ContentType = "text/html",
                Content = messageHtml
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
