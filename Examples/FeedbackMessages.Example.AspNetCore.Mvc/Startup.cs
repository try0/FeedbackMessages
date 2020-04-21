using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Example.AspNetCore.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(FeedbackMessageActionFilter.Instance);
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            });
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();
            app.UseFeedackMessages();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
