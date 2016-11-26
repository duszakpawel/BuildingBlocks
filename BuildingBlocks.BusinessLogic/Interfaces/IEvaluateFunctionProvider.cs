namespace BuildingBlocks.BusinessLogic.Interfaces
{
    public interface IEvaluateFunctionProvider
    {
        /// <summary>
        ///     Evaluation function. Returns score.
        /// </summary>
        /// <param name="board">board array</param>
        int Evaluate(int[,] board);
    }
}