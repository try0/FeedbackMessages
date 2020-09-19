using FeedbackMessages.Utils.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FeedbackMessages.Components
{

    public class FeedbackMessagePanel : ComponentBase
    {
        private EditContext previousEditContext;
        private readonly EventHandler<ValidationStateChangedEventArgs> validationStateChangedHandler;

        /// <summary>
        /// Whether to render validation error messages.
        /// </summary>
        [Parameter]
        public bool ShowValidationErrors { get; set; } = false;

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        [CascadingParameter] EditContext CurrentEditContext { get; set; }

        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer { get; set; } = FeedbackMessageSettings.Instance.MessageRenderer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessagePanel()
        {
            validationStateChangedHandler = (sender, eventArgs) => {
                if (ShowValidationErrors)
                {
                    StateHasChanged();
                }
            };
        }

        protected override void OnParametersSet()
        {
            if (CurrentEditContext != previousEditContext)
            {
                DetachValidationStateChangedListener();
                CurrentEditContext.OnValidationStateChanged += validationStateChangedHandler;
                previousEditContext = CurrentEditContext;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (ShowValidationErrors && CurrentEditContext != null)
            {
                FeedbackMessageUtil.AppendValidationErrorsToStore(CurrentEditContext);
            }

            builder.OpenElement(0, "div");
            if (AdditionalAttributes != null)
            {
                builder.AddMultipleAttributes(1, AdditionalAttributes);
            }
            builder.AddMarkupContent(0, MessageRenderer.RenderMessages().ToString());
            builder.CloseElement();
        }


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            FeedbackMessageStore.Current.CleanRendered();
            FeedbackMessageStore.Flash();
        }

        private void DetachValidationStateChangedListener()
        {
            if (previousEditContext != null)
            {
                previousEditContext.OnValidationStateChanged -= validationStateChangedHandler;
            }
        }

    }
}
