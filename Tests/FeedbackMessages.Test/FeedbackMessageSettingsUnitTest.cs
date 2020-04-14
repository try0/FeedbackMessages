using FeedbackMessages.Frontends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageSettingsUnitTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestInitializeSettings() {

            var renderer = new FeedbackMessageRenderer();
            var scriptBuilder = new FeedbackMessageScriptBuilder();
            var config = new FeedbackMessageSettings.FeedbackMessageConfig();

            FeedbackMessageSettings.Initializer
                .SetMessageRenderer(renderer)
                .SetScriptBuilder(scriptBuilder)
                .SetConfig(config)
                .Initialize();


            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.MessageRenderer, renderer));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.ScriptBuilder, scriptBuilder));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.Config, config));

        }
    }
}
