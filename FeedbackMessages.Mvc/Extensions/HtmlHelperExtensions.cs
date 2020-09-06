using FeedbackMessages.Utils.Mvc;
using System.Web;
using System.Web.Mvc;

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
        public static IHtmlString FeedbackMessagePanel(this HtmlHelper helper, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return MvcHtmlString.Create(FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders feedback messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="messageRenderer"></param>
        /// <param name="showValidationErrors"></param>
        /// <returns></returns>
        public static IHtmlString FeedbackMessagePanel(this HtmlHelper helper, FeedbackMessageRenderer messageRenderer, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return MvcHtmlString.Create(messageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders script block for display messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="showValidationErrors"></param>
        /// <returns></returns>
        public static IHtmlString FeedbackMessageScript(this HtmlHelper helper, bool showValidationErrors = false)
        {
            if (showValidationErrors)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(helper.ViewContext);
            }

            return MvcHtmlString.Create("<script>" + FeedbackMessageSettings.Instance.ScriptBuilder.GetDomReadyScript() + "</script>");
        }
    }
}
