using FeedbackMessages.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FeedbackMessages.Example.AspNetCore.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            this.InfoMessage("Information message.");
            this.SuccessMessage("Success message.");
            this.WarnMessage("Warning message.");
            this.ErrorMessage("Error message.");
        }
    }
}
