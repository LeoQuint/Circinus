using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormating {

    private const string THOUSANDS = "K";
    private const string MILLIONS = "M";
    private const string BILLIONS = "B";
    private const string TRILLIONS = "T";
    private const string QUADRILLIONS = "Q";
    private const string COMMA = ",";

    public static string ToLargeValues(this int val)
    {
        string str ="";
        if (val < 999)
        {
            str = val.ToString();
        }
        else if (val < 999999)
        {
            string frac = ((val / 100) % 10).ToString();
            str = ((int)(val / 1000)).ToString()+ COMMA + frac + THOUSANDS;
        }
        else if (val < 999999999)
        {
            string frac = ((val / 100000) % 10).ToString();
            str = ((int)(val / 1000000)).ToString() + COMMA + frac + MILLIONS;
        }

        return str;
    }

    public static string ToLargeValues(this ulong val)
    {
        string str = "";
        if (val < 999)
        {
            str = val.ToString();
        }
        else if (val < 999999)
        {
            str = ((ulong)(val / 1000)).ToString() + THOUSANDS;
        }
        else if (val < 999999999)
        {
            str = ((ulong)(val / 1000000)).ToString() + MILLIONS;
        }
        else if (val < 999999999999)
        {
            str = ((ulong)(val / 1000000)).ToString() + BILLIONS;
        }
        else if (val < 999999999999999)
        {
            str = ((ulong)(val / 1000000)).ToString() + TRILLIONS;
        }
        else if (val < 999999999999999999)
        {
            str = ((ulong)(val / 1000000)).ToString() + QUADRILLIONS;
        }
        return str;
    }

    public static string ToRoman(this int number)
    {
        if ((number < 0) || (number > 3999))
        {
            console.logError("number must be between 0 and 3999");
        }
        if (number < 1)
        {
            return string.Empty;
        }
        if (number >= 1000)
        {
            return "M" + ToRoman(number - 1000);
        }
        if (number >= 900)
        {
            return "CM" + ToRoman(number - 900);
        }
        if (number >= 500)
        {
            return "D" + ToRoman(number - 500);
        } 
        if (number >= 400)
        {
            return "CD" + ToRoman(number - 400);
        } 
        if (number >= 100)
        {
            return "C" + ToRoman(number - 100);
        }
        if (number >= 90)
        {
            return "XC" + ToRoman(number - 90);
        } 
        if (number >= 50)
        {
            return "L" + ToRoman(number - 50);
        } 
        if (number >= 40)
        {
            return "XL" + ToRoman(number - 40);
        }
        if (number >= 10)
        {
            return "X" + ToRoman(number - 10);
        } 
        if (number >= 9)
        {
            return "IX" + ToRoman(number - 9);
        }
        if (number >= 5)
        {
            return "V" + ToRoman(number - 5);
        } 
        if (number >= 4)
        {
            return "IV" + ToRoman(number - 4);
        } 
        if (number >= 1)
        {
            return "I" + ToRoman(number - 1);
        }
        console.logError("Error in convertion.");
        return string.Empty;
    }
}
