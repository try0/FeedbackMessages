
using FeedbackMessages.Extensions;
using Microsoft.AspNetCore.Http;

namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> provider.
    /// </summary>
    public class FeedbackMessageStoreHolder : IFeedbackMessageStoreHolder
    {
        /// <summary>
        /// Default instance.
        /// </summary>
        public static readonly FeedbackMessageStoreHolder Instance = new FeedbackMessageStoreHolder();

        /// <summary>
        /// Http context accessor.
        /// </summary>
        public static IHttpContextAccessor ContextAccessor { get; set; }

        /// <summary>
        /// Whether available session or not. 
        /// </summary>
        public static bool IsAvailableSession { get; set; } = true;

        /// <summary>
        /// MessageStore key string that for use add store to session or request items.
        /// </summary>
        public static readonly string ITEM_KEY = typeof(FeedbackMessageStore).ToString();

        private static readonly string META_DATA_SESSION_KEY = "SESSION";

        /// <summary>
        /// Gets message store in current request.
        /// </summary>
        public FeedbackMessageStore Current
        {
            get
            {
                var store = ContextAccessor.HttpContext.Items[ITEM_KEY];

                if (store != null)
                {
                    return (FeedbackMessageStore)store;
                }

                store = new FeedbackMessageStore();
                ContextAccessor.HttpContext.Items[ITEM_KEY] = store;
                return (FeedbackMessageStore)store;
            }
        }

        /// <summary>
        /// Flashs <see cref="FeedbackMessageStore"/> to session.
        /// </summary>
        public void FlashFeedbackMessageStore()
        {
            var messageStore = Current;
            messageStore.CleanRendered();

            if (messageStore.Items.ContainsKey(META_DATA_SESSION_KEY)
                && messageStore.Items[META_DATA_SESSION_KEY] != null)
            {
                ISession session = (ISession)messageStore.Items[META_DATA_SESSION_KEY];

                if (messageStore.HasUnrenderedMessage())
                {
                    session.SetStore(ITEM_KEY, messageStore);
                }
                else
                {
                    messageStore.Items.Remove(session);
                    session.Remove(ITEM_KEY);
                }

                return;
            }

            if (!ExistsSession())
            {
                return;
            }


            if (messageStore.HasUnrenderedMessage())
            {
                ContextAccessor.HttpContext.Session.SetStore(ITEM_KEY, messageStore);
            }
            else
            {
                messageStore.Items[META_DATA_SESSION_KEY] = null;
                ContextAccessor.HttpContext.Session.Remove(ITEM_KEY);
            }
        }

        /// <summary>
        /// Loads <see cref="FeedbackMessageStore"/> from session.
        /// </summary>
        public void LoadFeedbackMessageStore()
        {

            var context = ContextAccessor.HttpContext;
            FeedbackMessageStore messageStore = (FeedbackMessageStore)context.Items[ITEM_KEY];
            if (messageStore != null)
            {
                return;
            }

            if (!ExistsSession())
            {
                context.Items[ITEM_KEY] = new FeedbackMessageStore();
                return;
            }

            messageStore = context.Session.GetStore(ITEM_KEY);
            if (messageStore == null)
            {
                messageStore = new FeedbackMessageStore();
            }

            messageStore.Items[META_DATA_SESSION_KEY] = context.Session;

            context.Items[ITEM_KEY] = messageStore;
        }


        /// <summary>
        /// Whether exists session or not.
        /// </summary>
        /// <returns></returns>
        private static bool ExistsSession()
        {
            var context = ContextAccessor.HttpContext;
            return context != null
                && IsAvailableSession
                && context.Session != null;
        }

    }
}
