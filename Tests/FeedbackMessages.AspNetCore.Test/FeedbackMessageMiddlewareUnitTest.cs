using FeedbackMessages.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class FeedbackMessageMiddlewareUnitTest : FeedbackMessagesUnitTestBase
    {

        public class MockServiceProvier : IServiceProvider
        {

            public bool IsAvailabelHttpContextAccessor { get; set; } = false;
            public bool IsAvailabelSession { get; set; } = false;

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(IHttpContextAccessor) && IsAvailabelHttpContextAccessor)
                {
                    return new Mock<IHttpContextAccessor>().Object;
                }

                if (serviceType == typeof(ISessionStore) && IsAvailabelSession)
                {
                    return new Mock<ISessionStore>().Object;
                }

                return null;
            }
        }

        [TestMethod]
        public void TestInitialize()
        {

            var serviceProvider = new MockServiceProvier();
            serviceProvider.IsAvailabelHttpContextAccessor = true;
            serviceProvider.IsAvailabelSession = true;

            IApplicationBuilder builder = new ApplicationBuilder(serviceProvider);
            builder.ApplicationServices = serviceProvider;

            builder.UseFeedackMessages();

            Assert.IsNotNull(FeedbackMessageStoreHolder.ContextAccessor);

            Assert.IsTrue(FeedbackMessageStoreHolder.IsAvailableSession);

            
        }

        [TestMethod]
        public void TestInitializeIsFailed()
        {

            var serviceProvider = new MockServiceProvier();
            serviceProvider.IsAvailabelHttpContextAccessor = false;


            IApplicationBuilder builder = new ApplicationBuilder(serviceProvider);
            builder.ApplicationServices = serviceProvider;

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                builder.UseFeedackMessages();
            });
        }

        [TestMethod]
        public void TestInitializeUnavailableSession()
        {

            var serviceProvider = new MockServiceProvier();
            serviceProvider.IsAvailabelHttpContextAccessor = true;
            serviceProvider.IsAvailabelSession = false;


            IApplicationBuilder builder = new ApplicationBuilder(serviceProvider);
            builder.ApplicationServices = serviceProvider;
            builder.UseFeedackMessages();

            Assert.IsFalse(FeedbackMessageStoreHolder.IsAvailableSession);

        }

        [TestMethod]
        public void TestInitializeAvailableSession()
        {

            var serviceProvider = new MockServiceProvier();
            serviceProvider.IsAvailabelHttpContextAccessor = true;
            serviceProvider.IsAvailabelSession = true;


            IApplicationBuilder builder = new ApplicationBuilder(serviceProvider);
            builder.ApplicationServices = serviceProvider;
            builder.UseFeedackMessages();

            Assert.IsTrue(FeedbackMessageStoreHolder.IsAvailableSession);

        }
    }
}
