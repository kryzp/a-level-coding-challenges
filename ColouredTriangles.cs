using System;

namespace ColouredTriangles
{
	public class Program
	{
		public enum Colour
		{
			RED = 1 << 0,
			GRN = 1 << 1,
			BLU = 1 << 2,
		};

		public static Colour GetColour(Colour a, Colour b)
		{
			if (a == b)
				return a;

			return (Colour)(~((int)a | (int)b) & 7);
		}

		public static void SolveTriangle(int row, List<List<Colour>> triangle)
		{
			if (row >= triangle[0].Count)
				return;
			
			triangle.Add(new List<Colour>());

			for (int i = 0; i < triangle[0].Count - row; i++)
			{
				triangle[row].Add(GetColour(triangle[row - 1][i], triangle[row - 1][i + 1]));
			}
			
			SolveTriangle(row + 1, triangle);
		}

		public static char ColourToCharacter(Colour col)
		{
			if (col == Colour.RED)
				return 'R';
			
			if (col == Colour.GRN)
				return 'G';
			
			if (col == Colour.BLU)
				return 'B';

			return 'X';
		}

		public static Colour CharacterToColour(char c)
		{
			if (c == 'R')
				return Colour.RED;
			
			if (c == 'G')
				return Colour.GRN;
			
			if (c == 'B')
				return Colour.BLU;

			return Colour.RED;
		}

		public static void PrintTriangle(List<List<Colour>> triangle)
		{
			for (int i = 0; i < triangle[0].Count; i++)
			{
				for (int k = 0; k < i; k++)
				{
					Console.Write(" ");
				}

				for (int j = 0; j < triangle[0].Count - i; j++)
				{
					Console.Write(ColourToCharacter(triangle[i][j]) + " ");
				}

				Console.WriteLine();
			}
		}

		public static void Main(string[] args)
		{
			List<Colour> initialColours = new List<Colour>() { Colour.RED, Colour.RED, Colour.GRN, Colour.BLU, Colour.RED, Colour.GRN, Colour.BLU, Colour.BLU };
			
			Console.Write("Enter the triangle: ");
			string coloursString = Console.ReadLine() ?? "";

			for (int i = 0; i < coloursString.Length; i++)
			{
				initialColours.Add(CharacterToColour(coloursString[i]));
			}

			List<List<Colour>> triangle = new List<List<Colour>>();
			triangle.Add(initialColours);

			SolveTriangle(1, triangle);
			PrintTriangle(triangle);
		}
	}
}
