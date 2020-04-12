@echo off

call nuget pack FeedbackMessages.nuspec -Prop Configuration=Release
call nuget pack FeedbackMessages.Abstractions.nuspec -Prop Configuration=Release
call nuget pack FeedbackMessages.Mvc.nuspec -Prop Configuration=Release
call nuget pack FeedbackMessages.AspNetCore.Mvc.nuspec -Prop Configuration=Release


pause