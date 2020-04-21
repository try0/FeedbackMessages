using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// Unit test base.
    /// </summary>
    public abstract class FeedbackMessagesUnitTestBase
    {


        public TestContext TestContext { get; set; }


        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        [ClassCleanup()]
        public static void MyClassCleanup()
        {

        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }



        public HttpContext InitializeHttpContext()
        {

            var testContext = new HttpContext(
                new HttpRequest("", "http://dotnet.try0.jp", ""),
                new HttpResponse(new StringWriter())
            );


            var sessionStateContainer = new HttpSessionStateContainer(
                "",
                new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(),
                20000,
                true,
                HttpCookieMode.UseCookies,
                SessionStateMode.InProc,
                false
            );
            SessionStateUtility.AddHttpSessionStateToContext(testContext, sessionStateContainer);

            HttpContext.Current = testContext;

            FeedbackMessageStore.Initialize(FeedbackMessageStoreHolder.Instance);
            return testContext;
        }

        public FeedbackMessageSettings InitializeSettings()
        {
            var renderer = new FeedbackMessageRenderer();
            var scriptBuilder = new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}')");
            var config = new FeedbackMessageSettings.FeedbackMessageConfig();

            FeedbackMessageSettings.CreateInitializer()
                .SetMessageRendererInstance(renderer)
                .SetScriptBuilderInstance(scriptBuilder)
                .SetConfigInstance(config)
                .Initialize();

            return FeedbackMessageSettings.Instance;
        }

        public void InfoMessage(object message)
        {
            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Info(message));
        }

        public void SuccessMessage(object message)
        {
            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Success(message));
        }

        public void WarnMessage(object message)
        {
            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Warn(message));
        }

        public void ErrorMessage(object message)
        {
            FeedbackMessageStore.Current.AddMessage(FeedbackMessage.Error(message));
        }
    }
}
