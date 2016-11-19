namespace BuildingBlocks.BusinessLogic.Algorithm
{
    /// <summary>
    /// Evaluate function provider
    /// </summary>
    public class EvaluateFunctionProvider
    {
        /// <summary>
        /// Evaluation function. Returns score.
        /// Teraz najlepszy wynik oznacza największy wynik. Można zmienić, wtedy zmiana w Algorithm z OrderByDescending do OrderBy
        /// Teraz wynik to najwyżej wysunięty prostokąt 
        /// TODO: Lepsza ocena położenia 
        /// </summary>
        /// <param name="content">content array</param>
        /// <returns></returns>
        public int Evaluate(bool[,] content)
        {
            var height = content.GetLength(1);
            var width = content.GetLength(0);

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    if (content[i, j] == true)
                    {
                        return j;
                    }
                }
            }

            return 0;
        }
    }
}
