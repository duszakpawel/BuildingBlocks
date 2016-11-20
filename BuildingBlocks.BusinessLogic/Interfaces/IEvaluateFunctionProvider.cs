namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IEvaluateFunctionProvider
    {
        /// <summary>
        ///     Evaluation function. Returns score.
        /// </summary>
        /// <param name="content">content array</param>
        int Evaluate(bool[,] content);
    }
}