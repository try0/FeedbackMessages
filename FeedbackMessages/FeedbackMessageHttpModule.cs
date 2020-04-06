using System;
using System.Web;

namespace FeedbackMessages
{
    /// <summary>
    /// HttpModule that for initialize and finalize the message store.
    /// </summary>
    public class FeedbackMessageHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += (object sender, EventArgs e) =>
            {
                FeedbackMessageStore.Load();
            };

            context.PostRequestHandlerExecute += (object sender, EventArgs e) =>
            {
                FeedbackMessageStore.Flash();
            };
        }


    }
}
