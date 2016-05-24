<Query Kind="Program" />

void Main() {
	int N = 5;
	int K = 3;
	
	var set = Enumerable.Range(0,N);

	for (int i = 0; i < Helpers.NumCombinations(N, K); i++) {	
		var combination = set.GetIthCombination(K, i);
		var combinationStr = string.Join(" ", combination.Select(n => n.ToString()).ToArray());

		int index = combination.FindIForCombination(N, K);
		Console.WriteLine($"{index} - {combinationStr}");
	}
}

public static class Helpers {

	public static int NumCombinations(int n, int k) {
		if (n == k) { return 1; }

		// TODO - Need to improve this.  There are more efficient ways
		// of computing binomial coefficients that dont have 
		// as much risk of overflow.
		int nFact = Enumerable.Range(1, n).Aggregate((agg, next) => agg * next);
		int kFact = Enumerable.Range(1, k).Aggregate((agg, next) => agg * next);
		int nMinusKFact = Enumerable.Range(1, n - k).Aggregate((agg, next) => agg * next);

		return nFact / (kFact * nMinusKFact);
	}

	public static IEnumerable<int> GetIthCombination(this IEnumerable<int> set, int k, int i) {
		if (k == 1) {
			return set.Skip(i).Take(1);
		} else {
			int numWithHead = NumCombinations(set.Count() - 1, k - 1);
			
			if (i < numWithHead) {
				return set.Take(1).Concat(set.Skip(1).GetIthCombination(k - 1, i));
			}
			else {
				return set.Skip(1).GetIthCombination(k, i - numWithHead);
			}
		}
	}

	public static int FindIForCombination(this IEnumerable<int> set, int n, int k)
	{
		var head = set.First();
		var tail = set.Skip(1);
		int i = 0;

		int x = 0;
		while(tail.Any()) {
			if (x != head) {
				i += NumCombinations(n - 1, k - 1);
				n--;
			}
			else {
				head = tail.First();
				tail = tail.Skip(1);
				n -= 1;
				k -= 1;
			}
			
			x++;
		}
		
		i += head - x;
		
		return i;
	}
}