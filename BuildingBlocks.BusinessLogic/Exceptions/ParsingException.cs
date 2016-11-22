using System;

namespace BuildingBlocks.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception class thrown in parsing logic
    /// </summary>
    public class ParsingException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ParsingException() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public ParsingException(string message) : base(message) { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="inner">inner exception object</param>
        public ParsingException(string message, Exception inner) : base(message, inner) { }
    }
}
