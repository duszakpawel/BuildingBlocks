using System;

namespace BuildingBlocks.BusinessLogic.Exceptions
{
    public class SimulationTerminatedException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        public SimulationTerminatedException() { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        public SimulationTerminatedException(string message) : base(message) { }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="inner">inner exception object</param>
        public SimulationTerminatedException(string message, Exception inner) : base(message, inner) { }
    }
}