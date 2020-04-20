using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FeedbackMessages
{

    /// <summary>
    /// FeedbackMessagePanel tag helper.
    /// </summary>
    public class FeedbackMessageScriptTagHelper : TagHelper
    {

        /// <summary>
        /// Render messages.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "script";
            output.Content.AppendHtml(FeedbackMessageSettings.Instance.ScriptBuilder.GetDomReadyScript());
        }

    }
}
