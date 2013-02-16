using System;
using System.Text;

class DoubleToBinary
{
    public static string ToBinary(int number)
    {
        StringBuilder binary = new StringBuilder();//container for bit values
        while (number > 0)
        {
            binary.Append(number % 2);//current bit value
            number = number / 2;
        }
        string binResult = binary.ToString();//reverse row
        binResult = ReverseString(binResult);
        return binResult;
    }
    public static string returnBinaryFraction(double fraction)
    {
        StringBuilder fractBinary = new StringBuilder();//container for endbase values
        while (fraction != 1.0)
        {
            fraction = fraction * 2;
            if (fraction == 1.0) { fractBinary.Append('1'); break; }
            if (fraction > 1.0)
            {
                fractBinary.Append('1');
                fraction = fraction - 1;
            }
            else
            { fractBinary.Append('0'); }
        }

        string binFraction = fractBinary.ToString();
        return binFraction;
    }
    public static char returnSign(string number)
    {
        char sign = '0';
        if (number[0] == '-')
        {
            sign = '1';//in case number<0 ste the most left bit to 1 and remove sign

        }
        return sign;
    }


    static string ReturnExponent(string wholePart, string fractionPart)//return exponent
    {
        int exponent;
        if (wholePart.Length != 0) exponent = wholePart.Length - 1; 
        else exponent = -fractionPart.IndexOf('1') - 1; 
        return ToBinary(127 + exponent).PadLeft(8, '0'); // Convert power to binary times moving left(-) or right(+) plus 127 
    }
    // mantissa  last 23 bits
    static string ReturnMantissa(string wholePart, string fractionPart)
    {
        string mantissa;
        if (wholePart.Length != 0)
        {
            mantissa = wholePart.Substring(1) + fractionPart;
        }
        else
        {
            mantissa = fractionPart.Substring(fractionPart.IndexOf('1') + 1);
        }// in case of no integer part - get first non-zero in fraction
        mantissa = mantissa.PadRight(23, '0');
        return mantissa; 
    }
    //method to reverse string
    public static string ReverseString(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter float number:");
        string number = Console.ReadLine();
        char sign = returnSign(number);
        number = number.Remove(0, 1);
        string wholePart = number.Substring(0, number.IndexOf('.'));
        int whole = int.Parse(wholePart);
         wholePart = ToBinary(whole); // 123.456 -> 123 in binary
        string fractionPart = returnBinaryFraction(double.Parse(number) - whole);
        string exponent = ReturnExponent(wholePart, fractionPart);
        string mantissa = ReturnMantissa(wholePart, fractionPart);
        Console.WriteLine("Binary representation of the number ai as follows sign exponent mantissa:");
        Console.WriteLine("{0} {1} {2}",sign,exponent,mantissa); 
    }
}