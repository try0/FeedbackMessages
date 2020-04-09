# FeedbackMessages
ASP.NET feedback messages utility.  
Feedback messages to web-client, users easily.  
Messages that could not be rendered in the current request are persisted in the session.


## Usage

In code behind of component that inherits System.Web.UI.Control
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
