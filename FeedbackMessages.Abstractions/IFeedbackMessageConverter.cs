using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessage"/> converter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFeedbackMessageConverter<out T>
    {
        /// <summary>
        /// Converts message to type parameter <see cref="T"/> instance.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        T Convert(FeedbackMessage message);

    }
}
