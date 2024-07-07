namespace Example.Model
{
    using System;

    /// <summary>
    /// An exception that, if thrown, will be handled and converted into a friendly error message
    /// on a query instead of resulting in a server error.
    /// </summary>
    public class PublicMessageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicMessageException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PublicMessageException(string message)
             : base(message)
        {
        }
    }
}
