using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FeedbackMessages
{
    /// <summary>
    /// FeedbackMessages middleware.
    /// </summary>
    public class FeedbackMessageMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next"></param>
        public FeedbackMessageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next.Invoke(context);
        }
    }

    /// <summary>
    /// FeedbackMessages middleware extensions.
    /// </summary>
    public static class FeedbackMessageMiddlewareExtensions
    {
        /// <summary>
        /// Initialize FeedbackMessages.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseFeedackMessages(this IApplicationBuilder builder)
        {

            // set context accessor
            var httpContextAccessor = builder.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            FeedbackMessageStoreHolder.ContextAccessor = httpContextAccessor;

            // check if a session is available 
            var sessionStore = builder.ApplicationServices.GetService<ISessionStore>();
            if (sessionStore == null)
            {
                FeedbackMessageStoreHolder.IsAvailableSession = false;
            }

            // init store
            FeedbackMessageStore.Initialize(FeedbackMessageStoreHolder.Instance);



            return builder.UseMiddleware<FeedbackMessageMiddleware>();
        }
    }
}
