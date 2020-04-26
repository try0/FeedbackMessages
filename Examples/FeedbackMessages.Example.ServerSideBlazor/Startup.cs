
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using static FeedbackMessages.FeedbackMessage; 

namespace FeedbackMessages.Example.ServerSideBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddMvcOptions(options =>
            {
                options.Filters.Add(FeedbackMessageActionFilter.Instance);
            });
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
