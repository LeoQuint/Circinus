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
}
