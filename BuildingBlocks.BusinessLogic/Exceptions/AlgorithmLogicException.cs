using System;

namespace BuildingBlocks.BusinessLogic.Exceptions
{
    /// <summary>
    /// Exception class thrown in algorithm logic
    /// </summary>
    public class AlgorithmLogicException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public AlgorithmLogicException() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public AlgorithmLogicException(string message) : base(message) { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="inner">inner exception object</param>
        public AlgorithmLogicException(string message, Exception inner) : base(message, inner) { }
    }
}
