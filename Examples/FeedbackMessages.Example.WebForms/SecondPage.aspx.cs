using FeedbackMessages.Extensions;
using System;
using System.Web.UI;

namespace FeedbackMessages.Example.WebForms
{
    public partial class SecondPage : Page
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            var renderOption = new FeedbackMessageRenderOption()
            {
                ShowValidationErrors = true
            };
            this.AppendFeedbackMessageScripts(renderOption);

            //var messageRenderer = new FeedbackMessageRenderer();
            //messageRenderer.OuterTagName = "div";
            //messageRenderer.InnerTagName = "span";

            //messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.INFO,  "class", "ui info message");
            //messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.SUCCESS, "class", "ui success message");
            //messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.WARN, "class", "ui warn message");
            //messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.ERROR, "class", "ui error message");

            //FeedbackMessagePanel.MessageRenderer = messageRenderer;

        }


    }
}