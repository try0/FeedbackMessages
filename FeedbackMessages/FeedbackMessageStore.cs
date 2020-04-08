using FeedbackMessages;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

[assembly: PreApplicationStartMethod(typeof(FeedbackMessageStore), "Initialize")]

namespace FeedbackMessages
{

    /// <summary>
    /// Message store that manage <see cref="FeedbackMessage"/>.
    /// </summary>
    [Serializable]
    public class FeedbackMessageStore
    {
        /// <summary>
        /// MessageStore key string that for use add store to session or request items.
        /// </summary>
        public static readonly string ITEM_KEY = typeof(FeedbackMessageStore).ToString();

        /// <summary>
        /// Initializes message store. This method called from pre-application-start process automatically.
        /// </summary>
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(FeedbackMessageHttpModule));
        }

        /// <summary>
        /// Gets message sotre from current http context.
        /// </summary>
        public static FeedbackMessageStore Current
        {
            get
            {
                var store = HttpContext.Current.Items[ITEM_KEY];

                if (store != null)
                {
                    return (FeedbackMessageStore)store;
                }

                store = new FeedbackMessageStore();
                HttpContext.Current.Items[ITEM_KEY] = store;
                return (FeedbackMessageStore)store;
            }
        }

        /// <summary>
        /// Loads message store from the session if exists.
        /// </summary>
        public static void Load()
        {
            FeedbackMessageStore messageStore = (FeedbackMessageStore)HttpContext.Current.Items[ITEM_KEY];
            if (messageStore != null)
            {
                return;
            }

            if (!ExistsSession())
            {
                HttpContext.Current.Items[ITEM_KEY] = new FeedbackMessageStore();
                return;
            }

            messageStore = (FeedbackMessageStore)HttpContext.Current.Session[ITEM_KEY];
            if (messageStore == null)
            {
                messageStore = new FeedbackMessageStore();
            }

            messageStore.Session = HttpContext.Current.Session;

            HttpContext.Current.Items[ITEM_KEY] = messageStore;
        }

        /// <summary>
        /// Flashs unrendere messages to session if exists.
        /// </summary>
        public static void Flash()
        {

            var messageStore = Current;
            messageStore.CleanRendered();

            if (messageStore.Session != null)
            {
                if (messageStore.HasUnrenderedMessage())
                {
                    messageStore.Session[ITEM_KEY] = messageStore;
                }
                else
                {
                    messageStore.Session[ITEM_KEY] = null;
                }

                return;
            }

            if (!ExistsSession())
            {
                return;
            }


            if (messageStore.HasUnrenderedMessage())
            {
                HttpContext.Current.Session[ITEM_KEY] = messageStore;
            }

        }


        [NonSerialized]
        private HttpSessionState session;

        public HttpSessionState Session
        {
            get { return session; }
            set { session = value; }
        }

        /// <summary>
        /// Feedback messages holder
        /// </summary>
        public IDictionary<FeedbackMessage.FeedbackMessageLevel, List<FeedbackMessage>> Messages { get; set; } = new Dictionary<FeedbackMessage.FeedbackMessageLevel, List<FeedbackMessage>>();



        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessageStore()
        {
        }

        /// <summary>
        /// Whether exists session or not.
        /// </summary>
        /// <returns></returns>
        private static bool ExistsSession()
        {
            var context = HttpContext.Current;
            return context != null && context.Session != null;
        }

        /// <summary>
        /// Gets feedback messages.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<FeedbackMessage> GetFeedbackMessages(FeedbackMessage.FeedbackMessageLevel level)
        {
            if (!Messages.ContainsKey(level))
            {
                var messageList = new List<FeedbackMessage>();
                Messages[level] = messageList;
                return messageList;
            }

            return Messages[level];
        }

        /// <summary>
        /// Adds feedback message.
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(FeedbackMessage message)
        {
            if (message == null || message.Message == null || message.IsRendered)
            {
                return;
            }

            var messageList = GetFeedbackMessages(message.Level);
            messageList.Add(message);
        }

        /// <summary>
        /// Adds feedback messages.
        /// </summary>
        /// <param name="messages"></param>
        public void AddMessages(IEnumerable<FeedbackMessage> messages)
        {
            foreach (FeedbackMessage message in messages)
            {
                AddMessage(message);
            }
        }

        /// <summary>
        /// Cleans rendered messages.
        /// </summary>
        public void CleanRendered()
        {
            foreach (var messageList in Messages.Values)
            {
                var unrenderdMessages = messageList.Where(msg => !msg.IsRendered).ToList();

                messageList.Clear();
                messageList.AddRange(unrenderdMessages);
            }
        }

        /// <summary>
        /// Whethers exists unrendered messages or not.
        /// </summary>
        /// <returns></returns>
        public bool HasUnrenderedMessage()
        {
            foreach (var messageList in Messages.Values)
            {
                var hasUnrenderd = messageList.Where(msg => !msg.IsRendered).Any();

                if (hasUnrenderd)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
