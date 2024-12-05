using System.Diagnostics;
using System.Numerics;
using static System.Console;

class Karatsuba
{
    static void Main(string[] args)
    {
        WriteLine("Karatsuba Multiplication");
        WriteLine("------------------------");
        WriteLine("This program demonstrates the Karatsuba multiplication algorithm.");
        WriteLine("It multiplies two numbers using the Karatsuba algorithm.");
        WriteLine();

        BigInteger x = 0;
        BigInteger y = 0;

        while (true)
        {
            Write("Enter the multiplicand: ");
            (bool validMultiplicand, BigInteger multiplicand) = TakeInput();
        
            Write("Enter the multiplier: ");
            (bool validMultiplier, BigInteger multiplier) = TakeInput();

            if (validMultiplicand && validMultiplier)
            {
                x = multiplicand;
                y = multiplier;
                break;
            }
            else
            {
                WriteLine("Invalid input. Please enter valid numbers.");
            }
        }

        // Perform multiplication using Karatsuba
        BigInteger result = KaratsubaFunc(x, y);

        // Output the result
        WriteLine($"Result of {x} x {y} using Karatsuba: {result}");
    }

    static BigInteger KaratsubaFunc(BigInteger x, BigInteger y)
    {
        // Base case for small numbers
        if (x < 10 || y < 10)
        {
            return x * y;
        }

        // Determine the size of the numbers
        int n = Math.Max(x.ToString().Length, y.ToString().Length);
        int half_n = n / 2;

        // Split the numbers into two parts
        BigInteger a = x / BigInteger.Pow(10, half_n);
        BigInteger b = y / BigInteger.Pow(10, half_n);
        BigInteger c = x % BigInteger.Pow(10, half_n);
        BigInteger d = y % BigInteger.Pow(10, half_n);

        // Recursively calculate the three products
        BigInteger z2 = KaratsubaFunc(a, b); // High parts
        BigInteger z0 = KaratsubaFunc(c, d);   // Low parts
        BigInteger z1 = KaratsubaFunc(a + c, b + d) - z2 - z0;

        // Combine the results
        return z2 * BigInteger.Pow(10, 2 * half_n) + z1 * BigInteger.Pow(10, half_n) + z0;

    }

    static (bool, BigInteger) TakeInput()
    {
        bool valid = BigInteger.TryParse(ReadLine(), out BigInteger number);
        return (valid, number);
    }
}