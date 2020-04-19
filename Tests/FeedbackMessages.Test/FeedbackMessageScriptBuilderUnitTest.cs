using FeedbackMessages.Frontends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageScriptBuilderUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestGetScripts()
        {
            InitializeHttpContext();

            var builder = new FeedbackMessageScriptBuilder(msg => msg.ToString());

            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Info("Info message"));

            var str = builder.GetScripts();


            Assert.IsTrue(str.Equals("Info message;"));

            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Info("Info message1"));
            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Info("Info message2"));

            var str2 = builder.GetScripts();


            Assert.IsTrue(str2.Contains("Info message1;"));
            Assert.IsTrue(str2.Contains("Info message2;"));

        }

        [TestMethod]
        public void TestGetOnDomReadyScripts()
        {
            InitializeHttpContext();

            var builder = new FeedbackMessageScriptBuilder(msg => msg.ToString());

            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Info("Info message"));

            var str = builder.GetDomReadyScript();

            Assert.IsTrue(str.Contains(builder.GetScripts()));

        }

        [TestMethod]
        public void TestSetFactory()
        {
            InitializeHttpContext();

            Func<FeedbackMessage, string> func = msg => msg.ToString();
            var builder = new FeedbackMessageScriptBuilder(func);

            var message = FeedbackMessage.Info("Info message");

            Assert.IsNotNull(builder.ScriptFactory);
            Assert.AreEqual(builder.ScriptFactory.Convert(message), func.Invoke(message));

        }
    }
}
