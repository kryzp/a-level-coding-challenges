using System;
using System.Text;

namespace DataConversion
{
	public class Program
	{
		// technically the possible bases are only limited by how many characters you can cram in here.
		// i just went with the numbers + the upper-case alphabet but you could keep adding more characters to the end.
		public static readonly char[] NUMS =
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
		};

		public static Dictionary<char, int> GenerateConversionTable()
		{
			Dictionary<char, int> result = new Dictionary<char, int>();

			for (int i = 0; i < NUMS.Length; i++)
			{
				result.Add(NUMS[i], i);
			}
			
			return result;
		}

		public static string BaseToBase(string input, int from, int to)
		{
			// steps:
			// convert input from its own base into base 10
			// convert the base 10 input into the desired base
			
			// general vars
			string result = "";
			int b10val = 0;
			int i = 0;
			var table = GenerateConversionTable();
			
			// im not sure if i could convert directly from base (from) to base (to) since
			// maths in c# occurs in base 10, but who knows.

			// convert base (from) to base 10
			for (; i < input.Length; i++)
			{
				char c = input[^(i + 1)];
				int n = table[c];
				b10val += (int)(Math.Pow(from, i) * n);
			}
			
			// convert base 10 to base (to)
			while (b10val != 0)
			{
				i = b10val % to;
				b10val /= to;
				result = NUMS[i] + result;
			}

			return result;
		}

		public static string BaseToTwosComplement(string input, int from)
		{
			// steps:
			// flip
			// add one
			
			// convert input to base 2
			string b2 = BaseToBase(input, from, 2);
			
			// general vars
			StringBuilder result = new StringBuilder("11111111111111111111111111111111");
			int i;

			// move the inverse b2 into result (flip the bits)
			for (i = 0; i < b2.Length; i++)
			{
				result[^(i + 1)] = b2[^(i + 1)] == '0' ? '1' : '0';
			}

			// flip first bit (add one)
			result[^1] = result[^1] == '0' ? '1' : '0';

			// do carryover maths
			for (i = 0; i < b2.Length; i++)
			{
				if (result[^(i + 1)] == '0' && i != 0)
				{
					result[^(i + 1)] = '1';
					break;
				}
				
				result[^(i + 1)] = '0';
			}

			return result.ToString();
		}

		public static string TwosComplementToBase(string input, int to)
		{
			// steps:
			// start at last bit value
			// subtract powers of 2

			int idx = 0;

			for (idx = 0; idx < input.Length; idx++)
			{
				if (input[idx] == '1' && idx != 0)
					break;
			}

			int result = (int)Math.Pow(2, input.Length - idx - 1);
			
			for (int i = 0; i < input.Length - 2; i++)
			{
				int m = Convert.ToInt32(input[^(i + 1)].ToString());
				result -= (int)Math.Pow(2, i) * m;
			}

			result *= -1;

			return result.ToString();
		}

		public static string BaseToFixedPointBinary(string input, int from, int decimalPoint)
		{
			// this is very messy
			// i definitely think theres a better way to do this
			
			string result = "";
			double inputNumber = Convert.ToDouble(input);
			double n = 0.0;

			for (int i = 32; i >= 0; i--)
			{
				double pow = i - decimalPoint;
				double num = Math.Pow(2, pow);

				if (inputNumber - num >= 0.0)
				{
					inputNumber -= num;
					result += "1";
				}
				else
				{
					result += "0";
				}
			}

			return result;
		}

		public static string FixedPointBinaryToBase(string input, int to, int decimalPoint)
		{
			// steps
			// same as converting regular binary to decimal but when below decimal point add on the reciprocal of the absolute power

			double result = 0.0;

			for (int i = 0; i < input.Length; i++)
			{
				int m = Convert.ToInt32(input[^(i + 1)].ToString());
				result += Math.Pow(2, (double)(i - decimalPoint)) * m;
			}

			return result.ToString();
		}
		
		public static void RunProgramOnce()
		{
			Console.Write("What base will you be using for input?: ");
			int baseUsed = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine();
			
			Console.WriteLine("What would you like to do?");
			Console.WriteLine("[1] Convert between two different bases");
			Console.WriteLine("[2] Convert negative numbers to/from binary");
			Console.WriteLine("[3] Convert fixed point integers to/from binary");
			
			Console.Write("> ");
			string option = Console.ReadLine() ?? "";

			Console.WriteLine();

			// base-to-base conversion
			if (option == "1")
			{
				Console.Write("What base are you converting to?: ");
				int to = Convert.ToInt32(Console.ReadLine());
			
				Console.Write($"Enter B{baseUsed} Input: ");
				string input = Console.ReadLine() ?? "";
			
				Console.Write($"B{to} Output: ");
				Console.WriteLine(BaseToBase(input, baseUsed, to));
			}
			
			// negative numbers to/from binary
			else if (option == "2")
			{
				Console.Write("Would you like to convert to or from binary? [t/f]: ");
				string option2 = Console.ReadLine() ?? "";
				
				Console.Write("Enter the number: " + ((option2 == "t") ? "-" : ""));
				string num = Console.ReadLine() ?? "";
				
				// convert to twos complement
				if (option2 == "t")
				{
					Console.WriteLine(BaseToTwosComplement(num, baseUsed));
				}
				
				// convert from twos complement
				else
				{
					Console.WriteLine(TwosComplementToBase(num, baseUsed));
				}
			}
			
			// fixed point integers to/from binary
			else if (option == "3")
			{
				Console.Write("Would you like to convert to or from binary? [t/f]: ");
				string option2 = Console.ReadLine() ?? "";
				
				Console.Write("Enter the number: ");
				string num = Console.ReadLine() ?? "";
				
				Console.Write("Where does the decimal point lie?: ");
				int decimalPointPosition = Convert.ToInt32(Console.ReadLine());

				// convert to fixed point binary
				if (option2 == "t")
				{
					Console.WriteLine(BaseToFixedPointBinary(num, baseUsed, decimalPointPosition));
				}
				
				// convert from fixed point binary
				else
				{
					Console.WriteLine(FixedPointBinaryToBase(num, baseUsed, decimalPointPosition));
				}
			}
		}

		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the conversion program! (32bit)");

			do
			{
				Console.WriteLine("==========================================");
				RunProgramOnce();
				Console.Write("\nWould you like to continue? [Y/n]: ");
			}
			while (Console.ReadLine() == "Y");
		}
	}
}
