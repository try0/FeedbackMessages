using FeedbackMessages.Extensions;
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


            var messageRenderer = new FeedbackMessageRenderer();
            messageRenderer.OuterTagName = "div";
            messageRenderer.InnerTagName = "span";

            messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.INFO, "class", "ui info message");
            messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.SUCCESS, "class", "ui success message");
            messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.WARN, "class", "ui warn message");
            messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.ERROR, "class", "ui error message");

            FeedbackMessagePanel.MessageRenderer = messageRenderer;




            BtnResponseRedirect.Click += (object sender, EventArgs eClick) =>
            {
                this.InfoMessage(Message.Text);

                Response.Redirect("SecondPage.aspx");
            };

            BtnServerTransfer.Click += (object sender, EventArgs eClick) =>
            {
                this.InfoMessage(Message.Text);

                Server.Transfer("SecondPage.aspx");
            };

            BtnSubmit.Click += (object sender, EventArgs eClick) =>
            {
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