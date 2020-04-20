using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageSettingsUnitTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestInitializeSettings()
        {

            var renderer = new FeedbackMessageRenderer();
            var scriptBuilder = new FeedbackMessageScriptBuilder(msg => msg.ToString());
            var config = new FeedbackMessageSettings.FeedbackMessageConfig();
            var storeSerializer = new FeedbackMessageStoreSerializer();

            FeedbackMessageSettings.Initializer
                .SetMessageRendererInstance(renderer)
                .SetScriptBuilderInstance(scriptBuilder)
                .SetConfigInstance(config)
                .SetStoreSerializerInstance(storeSerializer)
                .Initialize();


            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.MessageRenderer, renderer));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.ScriptBuilder, scriptBuilder));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.Config, config));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.StoreSerializer, storeSerializer));

        }


        [TestMethod]
        public void TestInitializeSettingsFactory()
        {

            var renderer = new FeedbackMessageRenderer();
            var scriptBuilder = new FeedbackMessageScriptBuilder(msg => msg.ToString());
            var config = new FeedbackMessageSettings.FeedbackMessageConfig();
            var storeSerializer = new FeedbackMessageStoreSerializer();

            FeedbackMessageSettings.Initializer
                .SetMessageRendererFactory(() => renderer)
                .SetScriptBuilderFactory(() => scriptBuilder)
                .SetConfigFactory(() => config)
                .SetStoreSerializerFactory(() => storeSerializer)
                .Initialize();


            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.MessageRenderer, renderer));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.ScriptBuilder, scriptBuilder));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.Config, config));
            Assert.IsTrue(Object.ReferenceEquals(FeedbackMessageSettings.Instance.StoreSerializer, storeSerializer));

        }
    }
}
