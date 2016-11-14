namespace BuildingBlocks.BusinessLogic.Algorithm
{
    public class EvaluateFunction
    {
        // Teraz najlepszy wynik oznacza największy wynik. Można zmienić, wtedy zmiana w Algorithm z OrderByDescending do OrderBy
        // Teraz wynik to najwyżej wysunięty prostokąt 
        // TODO Lepsza ocena położenia 
        public static int Evaluate(bool[,] content)
        {
            for (var j = 0; j < content.GetLength(1); j++)
            {
                for (var i = 0; i < content.GetLength(0); i++)
                {
                    if (content[i, j])
                        return j;
                }
            }
            return 0;
        }
    }
}
