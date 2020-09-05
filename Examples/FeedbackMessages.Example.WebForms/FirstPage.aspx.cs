using FeedbackMessages.Extensions;
using FeedbackMessages.Utils;
using System;
using System.Web.UI;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Example.WebForms
{
    public partial class FirstPage : Page
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BtnResponseRedirect.Click += (object sender, EventArgs eClick) =>
            {
                this.AppendValidationErrorsToStore();

                if (Page.IsValid)
                {
                    this.InfoMessage(Message.Text);
                }

                Response.Redirect("SecondPage.aspx");
            };

            BtnServerTransfer.Click += (object sender, EventArgs eClick) =>
            {
                this.AppendValidationErrorsToStore();

                if (Page.IsValid)
                {
                    this.InfoMessage(Message.Text);
                }


                Server.Transfer("SecondPage.aspx");
            };

            BtnSubmit.Click += (object sender, EventArgs eClick) =>
            {
                if (!Page.IsValid)
                {
                    return;
                }

                this.InfoMessage(Message.Text);
            };

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InfoMessage("Information message.");
                this.SuccessMessage("Success message.");
                this.WarnMessage("Warning message.");
                this.ErrorMessage("Error message.");
            }
        }
    }
}