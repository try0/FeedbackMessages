using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackMessages.Example.ServerSideBlazor.Models
{
    public class IndexModel
    {
        [Required]
        public string Message { get; set; }
    }
}
