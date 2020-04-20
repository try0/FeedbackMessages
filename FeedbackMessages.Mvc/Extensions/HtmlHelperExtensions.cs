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
        /// <returns></returns>
        public static IHtmlString FeedbackMessagePanel(this HtmlHelper helper)
        {
            return MvcHtmlString.Create(FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders feedback messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="messageRenderer"></param>
        /// <returns></returns>
        public static IHtmlString FeedbackMessagePanel(this HtmlHelper helper, FeedbackMessageRenderer messageRenderer)
        {
            return MvcHtmlString.Create(messageRenderer.RenderMessages().ToString());
        }

        /// <summary>
        /// Renders script block for display messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString FeedbackMessageScript(this HtmlHelper helper)
        {
            return MvcHtmlString.Create("<script>" + FeedbackMessageSettings.Instance.ScriptBuilder.GetDomReadyScript() + "</script>");
        }
    }
}
