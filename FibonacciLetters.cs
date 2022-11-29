using System;

/*
Each letter in the alphabet can be given a value based on its position in the alphabet, A being 1 through to Z being 26. Two letters can produce a third letter by adding together their values, deducting 26 from the sum if it is greater than 26, and finding the letter whose value equals this result. 
For example:  
A and B produce the letter C since 1+2=3 and A, B and C are respectively letters 1, 2 and 3 in the alphabet. 
P and Q produce the letter G since 16+17=33, 33-26=7 and P, Q and G are respectively letters 16,  17 and 7 in the alphabet. 
We can generate a sequence of letters by starting with two letters and repeatedly using the last two letters in the sequence to produce another letter. 
Graphical user interface, text, Word
Description automatically generated with medium confidenceFor example (starting with A and A) we have: A, A, B, C, E, H, M, U, H, C, … 
Write a program which reads in two capital letters (the 1st  letter in a sequence followed by the 2nd letter) then an integer n (1 <= n <= 1,000,000). You should output a single capital letter, the nth letter in the sequence that starts with the input letters.  
*/

namespace FibonacciLetters
{
	public class Program
	{
		public static char CalcFibonacci(char c0, char c1, char c2, int n)
		{
			if (n <= 0)
				return c2;

			int t1 = c0 - 'A';
			int t2 = c1 - 'A';
			int t3 = t1 + t2 + 1;
			t3 %= 'Z' - 'A';

			c2 = (char)t3;
			c2 += 'A';

			c0 = c1;
			c1 = c2;

			return CalcFibonacci(c0, c1, c2, n - 1);
		}
		
		public static void Main()
		{
			Console.Write("Character 1: ");
			char c0 = (Console.ReadLine() ?? "A").ToUpper()[0];
			
			Console.Write("Character 2: ");
			char c1 = (Console.ReadLine() ?? "B").ToUpper()[0];
			
			int n = 0;
			
			do
			{
				Console.Write("Characters to print: ");
				n = Convert.ToInt32(Console.ReadLine() ?? "");
			}
			while (n < 1 || n > 1000000);

			Console.WriteLine();
			
			Console.WriteLine($"Character 1 in sequence: {c0}");
			Console.WriteLine($"Character 2 in sequence: {c1}");

			for (int j = 0; j < n; j++)
			{
				char c2 = CalcFibonacci(c0, c1, c0, j + 1);
				Console.WriteLine($"Character {j + 3} in sequence: {c2}");
			}
		}
	}
}
