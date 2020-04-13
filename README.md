# FeedbackMessages

[![Build status](https://ci.appveyor.com/api/projects/status/p8ia0pbkjtqx9i50?svg=true)](https://ci.appveyor.com/project/try0/feedbackmessages)

ASP.NET feedback messages utility.  
Display your feedbacks for web clients and users easily.  
Messages that could not be rendered in the current request are persisted in the session.

## Version
version >=.NETFramework 4.6.1, .NET Standard 2.0


## Usage

[FeedbackMessages](https://www.nuget.org/packages/FeedbackMessages/0.2.0-alpha)

[FeedbackMessages.Mvc](https://www.nuget.org/packages/FeedbackMessages.Mvc/0.2.0-alpha)

[FeedbackMessages.AspNetCore.Mvc](https://www.nuget.org/packages/FeedbackMessages.AspNetCore.Mvc/0.2.0-alpha)


---

### WebForms .NETFramework  

Add FeedbackMessages dependency.  
There is nothing you need to do to initialize. When start up application, add FeedbackMessages.FeedbackMessageHttpModule automatically.

Control that inherits System.Web.UI.Control
```C#
using FeedbackMessages.Extensions;

...

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

.aspx file
```xml
<%@ Register Assembly="FeedbackMessages" Namespace="FeedbackMessages.Components" TagPrefix="fm" %>

<!-- render message area -->
<fm:FeedbackMessagePanel runat="server" ID="FeedbackMessagePanel"></fm:FeedbackMessagePanel>
```


---


### Mvc .NETFramework

Add FeedbackMessages.Mvc dependency.  
There is nothing you need to do to initialize. When start up application, add FeedbackMessages.FeedbackMessageHttpModule automatically.

Controller that inherits System.Web.Mvc.Controller
```C#
using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

.cshtml file
```xml
@using FeedbackMessages.Extensions;

<!-- render message area -->
@Html.FeedbackMessagePanel()

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

Controller that inherits Microsoft.AspNetCore.Mvc.Controller
```C#
using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");

```

.cshtml file
```xml
@using FeedbackMessages.Extensions;

<!-- render message area -->
@Html.FeedbackMessagePanel()

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

PageModel that inherits Microsoft.AspNetCore.Mvc.RazorPages.PageModel
```C#
using FeedbackMessages.Extensions;

・・・

this.InfoMessage("Information feedback message.");
this.SuccessMessage("Success feedback message.");
this.WarnMessage("Warning feedback message.");
this.ErrorMessage("Error feedback message.");
```

.cshtml file
```xml
@addTagHelper *, FeedbackMessages.AspNetCore.Mvc
<feedback-message-panel></feedback-message-panel>
```
