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
    }
}
