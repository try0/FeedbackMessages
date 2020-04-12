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
            return MvcHtmlString.Create(new FeedbackMessageRenderer().RenderMessages().ToString());
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

    }
}
