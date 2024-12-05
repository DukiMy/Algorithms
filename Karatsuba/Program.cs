using System.Globalization;
using System.Numerics;
using static System.Console;

class Karatsuba
{
    private static (bool isValid, BigInteger number) multiplicand;
    private static (bool isValid, BigInteger number) multiplier;
    
    static int Main()
    {
        BigInteger x = 0;
        BigInteger y = 0;

        while (true)
        {
            Write("Enter the multiplicand:\t");
            multiplicand = TakeInput();
        
            Write("Enter the multiplier: \t");
            multiplier = TakeInput();

            if (!multiplicand.isValid || !multiplier.isValid)
            {
                WriteLine($"Multiplicand is valid: \t{multiplicand.isValid}");
                WriteLine($"Multiplier is valid: \t{multiplier.isValid}");
                continue;
            }

            x = multiplicand.number;
            y = multiplier.number;
            break;
        }

        // Perform multiplication using Karatsuba
        BigInteger result = KaratsubaFunc(x, y);

        // Output the result
        WriteLine($"Result of {x} x {y} using Karatsuba: {result}");

        return 0;
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
        bool valid = BigInteger.TryParse(
            ReadLine(),
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out BigInteger number
        );

        return (valid, number);
    }
}
