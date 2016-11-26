using System;

namespace BuildingBlocks.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception class thrown in evaluation function
    /// </summary>
    public class EvaluateFunctionException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public EvaluateFunctionException() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public EvaluateFunctionException(string message) : base(message) { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="inner">inner exception object</param>
        public EvaluateFunctionException(string message, Exception inner) : base(message, inner) { }
    }
}