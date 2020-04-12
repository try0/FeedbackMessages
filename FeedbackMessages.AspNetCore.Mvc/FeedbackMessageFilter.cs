using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FeedbackMessages
{
    /// <summary>
    /// Filter that for initialize and finalize the message store.
    /// </summary>
    public class FeedbackMessageFilter : Attribute, IActionFilter, IPageFilter
    {
        /// <summary>
        /// Default instance
        /// </summary>
        public static readonly FeedbackMessageFilter Instance = new FeedbackMessageFilter();

        /// <summary>
        /// Finalizes message store.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            FeedbackMessageStore.Flash();
        }

        /// <summary>
        /// Initializes message store.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            FeedbackMessageStore.Load();
        }

        /// <summary>
        /// Finalizes message store.
        /// </summary>
        /// <param name="context"></param>
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            FeedbackMessageStore.Flash();
        }

        /// <summary>
        /// Initializes message store.
        /// </summary>
        /// <param name="context"></param>
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            FeedbackMessageStore.Load();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
