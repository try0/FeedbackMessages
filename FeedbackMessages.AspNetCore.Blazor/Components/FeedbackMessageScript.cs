using FeedbackMessages.Utils.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages.Components
{

    public class FeedbackMessageScript : ComponentBase
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
        public FeedbackMessageScriptBuilder ScriptBuilder { get; set; } = FeedbackMessageSettings.Instance.ScriptBuilder;

        public FeedbackMessageScript()
        {
            validationStateChangedHandler = (sender, eventArgs) =>
            {
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

            builder.OpenElement(0, "script");
            if (AdditionalAttributes != null)
            {
                builder.AddMultipleAttributes(1, AdditionalAttributes);
            }

            builder.AddMarkupContent(0, ScriptBuilder.GetDomReadyScript());
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
