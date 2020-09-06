using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Example.Mvc.Models
{
    public class MessageModel
    {
        public static FeedbackMessageRenderer MESSAGE_RENDERER = new FeedbackMessageRenderer();

        static MessageModel() {
            MESSAGE_RENDERER.OuterTagName = "div";
            MESSAGE_RENDERER.InnerTagName = "span";

            MESSAGE_RENDERER.AppendOuterAttributeValue(FeedbackMessageLevel.INFO, "class", "ui info message MessageModel");
            MESSAGE_RENDERER.AppendOuterAttributeValue(FeedbackMessageLevel.SUCCESS, "class", "ui success message MessageModel");
            MESSAGE_RENDERER.AppendOuterAttributeValue(FeedbackMessageLevel.WARN, "class", "ui warn message MessageModel");
            MESSAGE_RENDERER.AppendOuterAttributeValue(FeedbackMessageLevel.ERROR, "class", "ui error message MessageModel");

 
        }

        [Required]
        public string Message { get; set; }
    }

}