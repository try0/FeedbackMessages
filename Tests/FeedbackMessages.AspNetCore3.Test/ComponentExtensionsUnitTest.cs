using FeedbackMessages.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{

    public class MockComponent : ComponentBase
    {
    }

    [TestClass]
    public class ComponentExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestAddMessages()
        {

            InitializeHttpContext();

            var component = new MockComponent();

            component.InfoMessage("情報メッセージ1");
            component.InfoMessage("情報メッセージ2");

            component.SuccessMessage("success");
            component.WarnMessage("warn");
            component.ErrorMessage("error");


            var store = FeedbackMessageStore.Current;
            Assert.AreEqual(5, store.Count);
            Assert.AreEqual(2, store.GetFeedbackMessages(FeedbackMessageLevel.INFO).Count);
            Assert.AreEqual(1, store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS).Count);
            Assert.AreEqual(1, store.GetFeedbackMessages(FeedbackMessageLevel.WARN).Count);
            Assert.AreEqual(1, store.GetFeedbackMessages(FeedbackMessageLevel.ERROR).Count);

        }
    }
}
