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
        /// <returns></returns>
        public static IHtmlContent FeedbackMessagePanel(this IHtmlHelper helper)
        {
            return new HtmlString(new FeedbackMessageRenderer().RenderMessages().ToString());
        }

        /// <summary>
        /// Renders feedback messages.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="messageRenderer"></param>
        /// <returns></returns>
        public static IHtmlContent FeedbackMessagePanel(this IHtmlHelper helper, FeedbackMessageRenderer messageRenderer)
        {
            return new HtmlString(messageRenderer.RenderMessages().ToString());
        }

    }
}
