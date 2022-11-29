
/*
Write a program which reads in a single number (between 1 and 123456789 inclusive) and determines
if it is an anagram number. If the number is not an anagram number you should output the word NO.
If it is an anagram number you should output each single digit it can be multiplied by to
make an anagram of itself.

Any anagrams formed by multiplying an anagram number by a digit are said to be generated
by that anagram number. Two numbers are anagrams of each other if they can both be formed by
rearranging the same combination of digits.
*/

namespace AnagramNumber
{
	public class Program
	{
		public static List<int> GetDigitsInNumber(int n)
		{
			List<int> result = new List<int>();
			while (n != 0)
			{
				if (!result.Contains(n % 10))
					result.Add(n % 10);
				n /= 10;
			}
			return result;
		}

		public static bool CompareContents(List<int> a, List<int> b)
		{
			if (a.Count != b.Count)
				return false;

			int count = a.Count;
			
			for (int i = 0; i < count; i++)
			{
				bool match = false;
				
				for (int j = 0; j < count; j++)
				{
					if (a[i] == b[j])
					{
						match = true;
						break;
					}
				}

				if (!match)
					return false;
			}
			
			return true;
		}
		
		public static List<int> GetAnagramDigits(int n)
		{
			List<int> result = new List<int>();
			List<int> digitsInN = GetDigitsInNumber(n);

			for (int i = 2; i <= 9; i++)
			{
				int n2 = n * i;
				List<int> digitsInN2 = GetDigitsInNumber(n2);

				if (CompareContents(digitsInN, digitsInN2))
					result.Add(i);
			}

			return result;
		}
		
		public static void Main()
		{
			Console.Write("Enter the number: ");
			int inputNumber = Convert.ToInt32(Console.ReadLine());
			List<int> multiples = GetAnagramDigits(inputNumber);
			bool isAnagram = multiples.Count != 0;

			if (isAnagram)
			{
				foreach (int n in multiples)
					Console.WriteLine(n);
			}
			else
			{
				Console.WriteLine("NO");
			}
		}
	}
}
