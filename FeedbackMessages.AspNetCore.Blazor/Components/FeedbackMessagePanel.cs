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
using System.Transactions;

namespace FeedbackMessages.Components
{
    /// <summary>
    /// Web component that render feedback messages. 
    /// </summary>
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
                    if (ShowValidationErrors && CurrentEditContext != null)
                    {
                        FeedbackMessageUtil.AppendValidationErrorsToStore(CurrentEditContext);
                    }
                }
            };
        }

        /// <summary>
        /// Refresh this component. Delegate processing to StateHasChanged().
        /// </summary>
        public void RefreshRender() {
            base.StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

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
