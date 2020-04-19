# FeedbackMessages

[![Build status](https://ci.appveyor.com/api/projects/status/p8ia0pbkjtqx9i50?svg=true)](https://ci.appveyor.com/project/try0/feedbackmessages) 
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FeedbackMessages&metric=coverage)](https://sonarcloud.io/dashboard?id=FeedbackMessages) 
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FeedbackMessages&metric=alert_status)](https://sonarcloud.io/dashboard?id=FeedbackMessages)

ASP.NET feedback messages utility.  
Display your feedbacks for web clients and users easily.  
Messages that could not be rendered in the current request are persisted in the session.

## Version

0.2.0

version >=.NETFramework 4.6.1, .NET Standard 2.0


## Usage

### NuGet
[FeedbackMessages](https://www.nuget.org/packages/FeedbackMessages/0.2.0) .NETFramework WebForms

[FeedbackMessages.Mvc](https://www.nuget.org/packages/FeedbackMessages.Mvc/0.2.0) .NETFramework Mvc

[FeedbackMessages.AspNetCore.Mvc](https://www.nuget.org/packages/FeedbackMessages.AspNetCore.Mvc/0.2.0) .NETCore Mvc, RazorPages


---


### Initialize settings (optional)

In your application's start up process.
```C#
FeedbackMessageSettings.Initializer
    // custom renderer for feedback-message-panel
    .SetMessageRenderer(() => {

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
    .SetScriptBuilder(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
    // init configs
    .Initialize();
```

#### Default
* FeedbackMessageRenderer renders ul and li tags that has "feedback-LEVEL" class attribute value.  
![output](https://user-images.githubusercontent.com/17096601/79125786-b2d68580-7dd9-11ea-9bd4-4e067d844d17.PNG)

* FeedbackMessageScriptBuilder throws Exception.


---


### WebForms .NETFramework  

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

this.AppendFeedbackMessageScripts();
```

---


### Mvc .NETFramework

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

@Html.FeedbackMessageScripts()
```


---


### Mvc .NETCore
Add FeedbackMessages.AspNetCore.Mvc dependency.

Initialize FeedbackMessages in Startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        // Required add filter
        options.Filters.Add(FeedbackMessageFilter.Instance);
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
@Html.FeedbackMessageScripts()

```


---


### RazorPages .NETCore
Add FeedbackMessages.AspNetCore.Mvc dependency.

Initialize FeedbackMessages in Startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(options =>
    {
        // Required add filter
        options.Filters.Add(FeedbackMessageFilter.Instance);
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

