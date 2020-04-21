using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Example.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FeedbackMessageSettings.CreateInitializer()
                .SetMessageRendererFactory(() =>
                {

                    var messageRenderer = new FeedbackMessageRenderer();
                    messageRenderer.OuterTagName = "div";
                    messageRenderer.InnerTagName = "span";

                    messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.INFO, "class", "ui info message");
                    messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.SUCCESS, "class", "ui success message");
                    messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.WARN, "class", "ui warning message");
                    messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.ERROR, "class", "ui error message");

                    return messageRenderer;
                })
                .SetScriptBuilderInstance(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
                .Initialize();
        }
    }
}
