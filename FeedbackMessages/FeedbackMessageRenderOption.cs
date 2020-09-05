using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackMessages
{
    /// <summary>
    /// Rendering options.
    /// </summary>
    public class FeedbackMessageRenderOption
    {
        /// <summary>
        /// Whether to render validation error messages.
        /// </summary>
        public bool ShowValidationErrors { get; set; } = false;

        /// <summary>
        /// Whether to render model state error messages.
        /// </summary>
        public bool ShowModelStateErrors { get; set; } = false;

        /// <summary>
        /// Validation group name.
        /// </summary>
        public string ValidationGroup { get; set; } = "";
    }
}
