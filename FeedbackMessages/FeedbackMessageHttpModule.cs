using FeedbackMessages;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(FeedbackMessageHttpModule), "Initialize")]

namespace FeedbackMessages
{
    /// <summary>
    /// HttpModule that for initialize and finalize the message store.
    /// </summary>
    public class FeedbackMessageHttpModule : IHttpModule
    {

        /// <summary>
        /// Initializes message store. This method called from pre-application-start process automatically.
        /// </summary>
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(FeedbackMessageHttpModule));

            FeedbackMessageStore.Initialize(FeedbackMessageStoreHolder.Instance);
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += (object sender, EventArgs e) =>
            {
                FeedbackMessageStore.Load();

                HttpContext.Current.AddOnRequestCompleted(ctx =>
                {
                    FeedbackMessageStore.Flash();
                });
            };

            context.PostRequestHandlerExecute += (object sender, EventArgs e) =>
            {
                FeedbackMessageStore.Flash();
            };

        }


    }
}
