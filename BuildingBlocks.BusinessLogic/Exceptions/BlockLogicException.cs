using System;

namespace BuildingBlocks.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception class thrown in block logic
    /// </summary>
    public class BlockLogicException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public BlockLogicException() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public BlockLogicException(string message) : base(message) { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="inner">inner exception object</param>
        public BlockLogicException(string message, Exception inner) : base(message, inner) { }
    }
}
