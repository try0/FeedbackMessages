using FeedbackMessages.Example.Mvc.Models;
using FeedbackMessages.Extensions;
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
                ContentEncoding = System.Text.Encoding.UTF8,
                Content = messageHtml
            };
        }

    }
}