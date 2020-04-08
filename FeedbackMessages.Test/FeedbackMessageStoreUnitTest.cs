using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> Unit Tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessageStoreUnitTest : FeedbackMessagesUnitTestBase
    {
        public FeedbackMessageStoreUnitTest()
        {
        }


        /// <summary>
        /// <see cref="FeedbackMessageStore.Load"/> test.
        /// </summary>
        [TestMethod]
        public void TestLoad()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY] = store;


            FeedbackMessageStore.Load();

            var loadedStore = HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNotNull(loadedStore);
            Assert.IsTrue(store == loadedStore);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashEmptyStore()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNull(flashedStore);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashExistsUnrenderedMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test message."));

            HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNotNull(flashedStore);
            Assert.IsTrue(store == flashedStore);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessage(FeedbackMessage)"/> test.
        /// </summary>
        [TestMethod]
        public void TestAddMessage()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            var feedbackMessage = FeedbackMessage.Info("Test info message.");

            store.AddMessage(feedbackMessage);

            List<FeedbackMessage> infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);
            Assert.AreEqual(infoMessages.Count, 1);
            Assert.IsTrue(infoMessages.Contains(feedbackMessage));

            List<FeedbackMessage> errorMessages = store.GetFeedbackMessages(FeedbackMessageLevel.ERROR);
            Assert.AreEqual(errorMessages.Count, 0);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessages(IEnumerable{FeedbackMessage})"/> test.
        /// </summary>
        [TestMethod]
        public void TestAddMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            List<FeedbackMessage> messages = new List<FeedbackMessage>();

            var info = FeedbackMessage.Info("Test info message.");
            var warn = FeedbackMessage.Warn("Test warn message.");

            messages.Add(info);
            messages.Add(warn);

            store.AddMessages(messages);

            List<FeedbackMessage> infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);
            Assert.AreEqual(infoMessages.Count, 1);
            Assert.IsTrue(infoMessages.Contains(info));

            List<FeedbackMessage> successMessages = store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS);
            Assert.AreEqual(successMessages.Count, 0);

            List<FeedbackMessage> warnMessages = store.GetFeedbackMessages(FeedbackMessageLevel.WARN);
            Assert.AreEqual(warnMessages.Count, 1);
            Assert.IsTrue(warnMessages.Contains(warn));

            List<FeedbackMessage> errorMessages = store.GetFeedbackMessages(FeedbackMessageLevel.ERROR);
            Assert.AreEqual(errorMessages.Count, 0);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.HasUnrenderedMessage"/> test.
        /// </summary>
        [TestMethod]
        public void TestHasUnrenderedMessage()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            Assert.IsFalse(store.HasUnrenderedMessage());

            var message = FeedbackMessage.Warn("Test warn message");
            store.AddMessage(message);

            Assert.IsTrue(store.HasUnrenderedMessage());

            message.MarkRender();

            Assert.IsFalse(store.HasUnrenderedMessage());

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.CleanRendered"/> test.
        /// </summary>
        [TestMethod]
        public void TestCleanRendered()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            Assert.IsFalse(store.HasUnrenderedMessage());


            var infoMessage1 = FeedbackMessage.Info("Test warn message");
            store.AddMessage(infoMessage1);
            var infoMessage2 = FeedbackMessage.Info("Test warn message");
            store.AddMessage(infoMessage2);
            var infoMessage3 = FeedbackMessage.Info("Test warn message");
            store.AddMessage(infoMessage3);

            store.CleanRendered();

            var infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);

            Assert.AreEqual(infoMessages.Count, 3);

            infoMessage1.MarkRender();
            infoMessage2.MarkRender();
            store.CleanRendered();

            var infoMessagesCleanedRendered = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);

            Assert.AreEqual(infoMessagesCleanedRendered.Count, 1);
            Assert.IsTrue(infoMessagesCleanedRendered.Contains(infoMessage3));

            infoMessage3.MarkRender();
            store.CleanRendered();

            Assert.IsFalse(store.HasUnrenderedMessage());

        }
    }
}
