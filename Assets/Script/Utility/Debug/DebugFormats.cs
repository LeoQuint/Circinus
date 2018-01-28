using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class console
{
    public static void log(object message)        
    {
        Debug.Log(message + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
    }

    public static void logStatus(object message)
    {
        Debug.Log("<b><color=green>Status: "+ message + "</color></b>" + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
    }

    public static void logInfo(object message)
    {
        Debug.Log("<b><color=blue>Info: " + message + "</color></b>" + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
    }

    public static void logWarning(object message)
    {
        Debug.LogWarning(message + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
    }

    public static void logError(object message)
    {
        Debug.LogError(message + "\n" + UnityEngine.StackTraceUtility.ExtractStackTrace());
    }
}
