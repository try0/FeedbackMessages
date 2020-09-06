using FeedbackMessages.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FeedbackMessages.Example.AspNetCore.RazorPages.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        [Required]
        public string Message { get; set; }

        public void OnGet()
        {
            this.InfoMessage("Information message.");
            this.SuccessMessage("Success message.");
            this.WarnMessage("Warning message.");
            this.ErrorMessage("Error message.");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this.SuccessMessage("OK");
            }

            return Page();
        }
    }
}
