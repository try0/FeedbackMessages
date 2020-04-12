using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestEquals()
        {

            var messageA = FeedbackMessage.Info("test message.");
            var messageB = FeedbackMessage.Info("test message.");
            var messageC = FeedbackMessage.Error("test message.");
            var messageD = FeedbackMessage.Error("test message!!!");

            // same level, same message
            Assert.AreEqual(messageA, messageB);

            // different levle, same message.
            Assert.AreNotEqual(messageA, messageC);
            Assert.AreNotEqual(messageB, messageC);

            // same level, different message
            Assert.AreNotEqual(messageC, messageD);
        }


        [TestMethod]
        public void TestMarkRendered()
        {

            var message = FeedbackMessage.Info("test message");

            Assert.IsFalse(message.IsRendered);

            message.MarkRendered();

            Assert.IsTrue(message.IsRendered);
        }
    }
}
