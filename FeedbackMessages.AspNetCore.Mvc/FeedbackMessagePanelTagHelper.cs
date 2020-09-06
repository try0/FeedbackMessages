using FeedbackMessages.Utils.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FeedbackMessages
{

    /// <summary>
    /// FeedbackMessagePanel tag helper.
    /// </summary>
    public class FeedbackMessagePanelTagHelper : TagHelper
    {
        /// <summary>
        /// Whether to render validation error messages.
        /// </summary>
        public bool ShowValidationErrors { get; set; } = false;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Render messages.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ShowValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(ViewContext);
            }

            output.TagName = "div";
            output.Content.AppendHtml(FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString());
        }

    }
}
