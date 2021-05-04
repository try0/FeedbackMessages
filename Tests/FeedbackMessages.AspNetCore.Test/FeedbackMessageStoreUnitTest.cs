using FeedbackMessages.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();



            httpContext.Session.SetStore(FeedbackMessageStoreHolder.ITEM_KEY, store);


            FeedbackMessageStore.Load();

            var loadedStore = httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] as FeedbackMessageStore;

            Assert.IsNotNull(loadedStore);
            Assert.AreEqual(0, loadedStore.Count);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashEmptyStore()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();

            httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = httpContext.Session.GetStore(FeedbackMessageStoreHolder.ITEM_KEY);

            Assert.IsNull(flashedStore);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashExistsUnrenderedMessages()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test message."));

            httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = httpContext.Session.GetStore(FeedbackMessageStoreHolder.ITEM_KEY);

            Assert.IsNotNull(flashedStore);
            Assert.AreEqual(1, flashedStore.Count);

            Assert.IsTrue(flashedStore.HasUnrenderedMessage());

            var message = flashedStore.GetFeedbackMessages()[0];
            Assert.AreEqual(FeedbackMessageLevel.INFO, message.Level);
            Assert.AreEqual("test message.", message.ToString());
        }

    }
}
