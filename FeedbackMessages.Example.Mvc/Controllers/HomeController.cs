using FeedbackMessages.Example.Mvc.Models;
using FeedbackMessages.Mvc.Extensions;
using System.Web.Mvc;

namespace FeedbackMessages.Example.Mvc.Controllers
{
    public class HomeController : Controller
    {
 
        [HttpGet]
        public ActionResult Index()
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

            switch (submitButton)
            {
                case "SecondPage":
                    return RedirectToAction("SecondPage");
                default:
                    return View();
            }
        }

        public ActionResult SecondPage()
        {
            return View();
        }

    }
}