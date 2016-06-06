<Query Kind="Program" />

void Main()
{
	int K = 3;

	for (int i = 0; i < 10; i++)
	{
		var combination = Helpers.GetIthCombination(K, i);
		var combinationStr = string.Join(" ", combination.Select(n => n.ToString()).ToArray());

		int index = combination.FindIForCombination();
		Console.WriteLine($"{index} - {combinationStr}");
	}
}

public static class Helpers {
	public static int NumCombinations(int n, int k)
    {
        if (n == k) { return 1; }

        // TODO - Need to improve this.  There are more efficient ways
        // of computing binomial coefficients that dont have 
        // as much risk of overflow.
        int nFact = Enumerable.Range(1, n).Aggregate((agg, next) => agg * next);
        int kFact = Enumerable.Range(1, k).Aggregate((agg, next) => agg * next);
        int nMinusKFact = Enumerable.Range(1, n - k).Aggregate((agg, next) => agg * next);

        int result = nFact / (kFact * nMinusKFact);
        return result;
    }

    public static IEnumerable<int> GetIthCombination(int k, int i)
    {
        if (k == 0)
            yield break;

        int n = k;
        int nextI = i;
        while (i >= NumCombinations(n, k))
        {
            n++;
            nextI = i - NumCombinations(n - 1, k);
        }
		yield return n - 1;

		foreach (var val in GetIthCombination(k - 1, nextI))
		{
			yield return val;
		}
	}

	public static int FindIForCombination(this IEnumerable<int> set)
	{
		int k = set.Count();

		if (k == 0)
			return 0;

		int n = set.First() + 1;

		if (n == k)
			return 0;

		var nc = NumCombinations(n - 1, k);
		var subNc = set.Skip(1).FindIForCombination();
		return nc + subNc;
	}
}