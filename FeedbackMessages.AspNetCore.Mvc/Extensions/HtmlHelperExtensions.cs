using FeedbackMessages.Utils.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedbackMessages.Extensions
{
    /// <summary>
    /// HtmlHelper extensions for message store.
    /// </summary>
    public static class HtmlHelperExtensions
    {

        /// <summary>
        /// Renders feedback messages as ul, li.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="showValidationErrors"></param>
        /// <returns></returns>
        public static IHtmlContent FeedbackMessagePanel(this IHtmlHelper helper, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return new HtmlString(FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders feedback messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="messageRenderer"></param>
        /// <param name="showValidationErrors"></param>
        /// <returns></returns>
        public static IHtmlContent FeedbackMessagePanel(this IHtmlHelper helper, FeedbackMessageRenderer messageRenderer, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return new HtmlString(messageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders script block for display messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="showValidationErrors"></param>
        /// <returns></returns>
        public static IHtmlContent FeedbackMessageScript(this IHtmlHelper helper, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return new HtmlString("<script>" + FeedbackMessageSettings.Instance.ScriptBuilder.GetDomReadyScript() + "</script>");
        }
    }
}
