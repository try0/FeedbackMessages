using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageConverterUnitTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestStringConverter()
        {
            var converter = new FeedbackMessageStringConverter();

            Assert.IsNotNull(converter.StringFactory);

            var message = FeedbackMessage.Info("Convert test");

            Assert.AreEqual("Convert test", converter.Convert(message));


            converter.StringFactory = msg => "prefix-" + msg.ToString();

            Assert.AreEqual("prefix-Convert test", converter.Convert(message));
        }
    }
}
