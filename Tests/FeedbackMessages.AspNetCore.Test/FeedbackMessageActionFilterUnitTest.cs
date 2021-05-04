using FeedbackMessages.Extensions;
using FeedbackMessages.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class FeedbackMessageActionFilterUnitTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestFilterActionExecuting()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test"));
            httpContext.Session.SetStore(FeedbackMessageStoreHolder.ITEM_KEY, store);

            // loads message store from session
            var filter = new FeedbackMessageActionFilter();
            filter.OnActionExecuting(null);

            var loadedStore = httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] as FeedbackMessageStore;
            Assert.IsNotNull(loadedStore);
            Assert.AreEqual(1, loadedStore.Count);

        }

        [TestMethod]
        public void TestFilterActionExecuted()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test"));
            httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            // flashs message store to session
            var filter = new FeedbackMessageActionFilter();
            filter.OnActionExecuted(null);

            var flashedStore = httpContext.Session.GetStore(FeedbackMessageStoreHolder.ITEM_KEY);
            Assert.IsNotNull(flashedStore);
            Assert.AreEqual(1, flashedStore.Count);
        }

        [TestMethod]
        public void TestFilterPageHandlerExecuting()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test"));
            httpContext.Session.SetStore(FeedbackMessageStoreHolder.ITEM_KEY, store);

            // loads message store from session
            var filter = new FeedbackMessageActionFilter();
            filter.OnPageHandlerExecuting(null);

            var loadedStore = httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] as FeedbackMessageStore;
            Assert.IsNotNull(loadedStore);
            Assert.AreEqual(1, loadedStore.Count);

        }

        [TestMethod]
        public void TestFilterPageHandlerExecuted()
        {
            var httpContext = InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test"));
            httpContext.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            // flashs message store to session
            var filter = new FeedbackMessageActionFilter();
            filter.OnPageHandlerExecuted(null);

            var flashedStore = httpContext.Session.GetStore(FeedbackMessageStoreHolder.ITEM_KEY);
            Assert.IsNotNull(flashedStore);
            Assert.AreEqual(1, flashedStore.Count);
        }
    }
}
