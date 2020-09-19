using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages.Utils.Blazor
{
    public static class FeedbackMessageUtil
    {

        public static void AppendValidationErrorsToStore(EditContext editContext)
        {
            var messageStore = FeedbackMessageStore.Current;

            foreach (var validationMessage in editContext.GetValidationMessages())
            {
                var feedbackMessage = FeedbackMessage.Error(validationMessage);
                messageStore.Add(feedbackMessage);
            }

        }
    }
}
