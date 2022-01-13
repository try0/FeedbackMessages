# FeedbackMessages

[![Build status](https://ci.appveyor.com/api/projects/status/p8ia0pbkjtqx9i50?svg=true)](https://ci.appveyor.com/project/try0/feedbackmessages) 
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FeedbackMessages&metric=coverage)](https://sonarcloud.io/dashboard?id=FeedbackMessages) 
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FeedbackMessages&metric=alert_status)](https://sonarcloud.io/dashboard?id=FeedbackMessages)

Feedback messages utility for .NET Web application.  
Display your feedbacks for web clients easily.  
Messages that could not be rendered are persisted in the session until rendered.

## Version

0.6.0 (pre-release)

version >=.NETFramework 4.6.1, .NET Standard 2.0

## Demo

[FeedbackMessages.Example.WebForms](http://feedbackmessages-webforms.azurewebsites.net)


## Usage

### NuGet
[FeedbackMessages](https://www.nuget.org/packages/FeedbackMessages/0.6.0) WebForms

[FeedbackMessages.Mvc](https://www.nuget.org/packages/FeedbackMessages.Mvc/0.6.0) Mvc (.NETFramework)

[FeedbackMessages.AspNetCore.Mvc](https://www.nuget.org/packages/FeedbackMessages.AspNetCore.Mvc/0.6.0) Mvc, RazorPages

[FeedbackMessages.AspNetCore.Blazor](https://www.nuget.org/packages/FeedbackMessages.AspNetCore.Blazor/0.6.0) ServerSideBlazor


---


### Initialize settings (optional)

In your application's start up process.
```C#
FeedbackMessageSettings.CreateInitializer()
    // custom renderer for feedback-message-panel
    .SetMessageRendererFactory(() => {

        var messageRenderer = new FeedbackMessageRenderer();
        messageRenderer.OuterTagName = "div";
        messageRenderer.InnerTagName = "span";

        messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.INFO, "class", "ui info message");
        messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.SUCCESS, "class", "ui success message");
        messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.WARN, "class", "ui warning message");
        messageRenderer.AppendOuterAttributeValue(FeedbackMessageLevel.ERROR, "class", "ui error message");

        return messageRenderer;
    })
    // custom script builder.
    .SetScriptBuilderInstance(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
    // custom store serializer.
    .SetStoreSerializerInstance(new FeedbackMessageStoreSerializer()
    {
        Deserializer = serial => /* TODO */ new FeedbackMessageStore(),
        Serializer = store => /* TODO */ ""
    })
    // init configs
    .Initialize();
```

#### Default
* FeedbackMessageRenderer renders ul and li tags that has "feedback-level" class attribute value.  
![output](https://user-images.githubusercontent.com/17096601/79125786-b2d68580-7dd9-11ea-9bd4-4e067d844d17.PNG)

* FeedbackMessageScriptBuilder throws Exception.

* FeedbackMessageStoreSerializer use System.Text.Json.JsonSerializer

---


### WebForms  

Add FeedbackMessages dependency.  
There is nothing you need to do to initialize. When start up application, add FeedbackMessages.FeedbackMessageHttpModule automatically.

Add messages.
```C#
// Control that inherits System.Web.UI.Control

using FeedbackMessages.Extensions;

...

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

In the case of display messages as html element.
```xml
<!-- .aspx file -->

<%@ Register Assembly="FeedbackMessages" Namespace="FeedbackMessages.Components" TagPrefix="fm" %>

<!-- render message area -->
<fm:FeedbackMessagePanel runat="server" ID="FeedbackMessagePanel"></fm:FeedbackMessagePanel>
```

In the case of display messages using JavaScript. 
```C#
// Control that inherits System.Web.UI.Control

using FeedbackMessages.Extensions;

this.AppendFeedbackMessageScript();
```

---


### Mvc (.NETFramework)

Add FeedbackMessages.Mvc dependency.  
There is nothing you need to do to initialize. When start up application, add FeedbackMessages.FeedbackMessageHttpModule automatically.

Add messages.
```C#
// Controller that inherits System.Web.Mvc.Controller

using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

In the case of display messages as html element.
```xml
<!-- .cshtml file -->

@using FeedbackMessages.Extensions;

<!-- render message area -->
@Html.FeedbackMessagePanel()

```

In the case of display messages using JavaScript. 
```xml
<!-- .cshtml file -->

@using FeedbackMessages.Extensions;

@Html.FeedbackMessageScript()
```

Ajax lazy load
```C#
public class YourController : Controller
{
    public ActionResult AjaxFeedbackMessage()
    {
        var messageHtml = FeedbackMessageSettings.Instance.MessageRenderer.RenderMessages().ToString();

        return new ContentResult()
        {
            ContentType = "text/html",
            ContentEncoding = System.Text.Encoding.UTF8,
            Content = messageHtml
        };
    }
}
```
```html
<script>

    $(function () {
       $.ajax({
            type: "POST", // GET
            url: "/Your/AjaxFeedbackMessage",
            success: function (messageHtml) {
                if (!messageHtml) {
                    return;
                }

                $("#ajax-feedback-msg-container").html(messageHtml);
            },
            error: function (jqXHR, status, error) {
                // TODO
            }
        });
    });
</script>

<div id="ajax-feedback-msg-container"></div>
```
---


### Mvc
Add FeedbackMessages.AspNetCore.Mvc dependency.

Initialize FeedbackMessages in Startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        // Required add filter
        options.Filters.Add(FeedbackMessageActionFilter.Instance);
    });

    // Required add context accessor
    services.AddHttpContextAccessor();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Required use middleware
    app.UseFeedackMessages();
}
```

Add messages.
```C#
// Controller that inherits Microsoft.AspNetCore.Mvc.Controller

using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

In the case of display messages as html element.
```xml
<!-- .cshtml file -->

@using FeedbackMessages.Extensions;

<!-- render message area -->
@Html.FeedbackMessagePanel()

```

In the case of display messages using JavaScript. 
```xml
<!-- .cshtml file -->

@using FeedbackMessages.Extensions;

<!-- render message area -->
@Html.FeedbackMessageScript()

```


---


### RazorPages
Add FeedbackMessages.AspNetCore.Mvc dependency.

Initialize FeedbackMessages in Startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        // Required add filter
        options.Filters.Add(FeedbackMessageActionFilter.Instance);
    });

    // Required add context accessor
    services.AddHttpContextAccessor();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Required use middleware
    app.UseFeedackMessages();
}
```

Add messages.
```C#
// PageModel that inherits Microsoft.AspNetCore.Mvc.RazorPages.PageModel

using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");
```

In the case of display messages as html element.
```xml
<!-- .cshtml file -->

@addTagHelper *, FeedbackMessages.AspNetCore.Mvc
<feedback-message-panel></feedback-message-panel>
```

In the case of display messages using JavaScript. 
```xml
<!-- .cshtml file -->

@addTagHelper *, FeedbackMessages.AspNetCore.Mvc
<feedback-message-script></feedback-message-script>
```


---



### Blazor(server-side)
Add FeedbackMessages.AspNetCore.Blazor dependency.

Initialize FeedbackMessages in Startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
    // Required add context accessor
    services.AddHttpContextAccessor();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Required use middleware
    app.UseFeedackMessages();
}
```

Add messages.
```C#
// Component that inherits Microsoft.AspNetCore.Components.ComponentBase

using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");
```
```xml
<!-- .razor file -->

@using FeedbackMessages.Extensions;

@code {
    this.InfoMessage("Information message.");
    this.SuccessMessage("Success message.");
    this.WarnMessage("Warning message.");
    this.ErrorMessage("Error message.");
}
```

In the case of display messages as html element.
```xml
<!-- .razor file -->

@namespace FeedbackMessages.Components
<FeedbackMessagePanel @ref="feedbackMessagePanel"></FeedbackMessagePanel>
```

In the case of display messages using JavaScript. 
```xml
<!-- .razor file -->

@namespace FeedbackMessages.Components
<FeedbackMessageScript @ref="feedbackMessageScript"></FeedbackMessageScript>
```


Refresh feedback message component.  
To redraw, the component needs to be refreshed.
```
feedbackMessagePanel.RefreshRender();
//feedbackMessageScript.RefreshRender();
```

---

