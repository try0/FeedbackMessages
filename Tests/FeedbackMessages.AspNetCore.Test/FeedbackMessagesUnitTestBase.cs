using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// Unit test base.
    /// </summary>
    public abstract class FeedbackMessagesUnitTestBase
    {
        public class MockSession : ISession
        {

            public IDictionary<string, object> SessionValues = new Dictionary<string, object>();

            public bool IsAvailable => true;

            public string Id => "MockSession";

            public IEnumerable<string> Keys => SessionValues.Keys;

            public void Clear()
            {
                SessionValues.Clear();
            }

            public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new System.NotImplementedException();
            }

            public Task LoadAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                throw new System.NotImplementedException();
            }

            public void Remove(string key)
            {
                SessionValues.Remove(key);
            }

            public void Set(string key, byte[] value)
            {
                SessionValues.Add(key, value);
            }

            public bool TryGetValue(string key, out byte[] value)
            {
                if (SessionValues.ContainsKey(key))
                {
                    value = (byte[])SessionValues[key];
                    return true;
                }
                value = null;

                return false;
            }
        }


        public class MockHttpContextAccessor : IHttpContextAccessor
        {


            public HttpContext HttpContext { get; set; }
        }


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

            var testContext = new DefaultHttpContext();
            testContext.Session = new MockSession();

            FeedbackMessageStoreHolder.IsAvailableSession = true;

            var contextAccessor = new MockHttpContextAccessor();
            contextAccessor.HttpContext = testContext;

            FeedbackMessageStoreHolder.ContextAccessor = contextAccessor;
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
