//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : ILoopable
{
    public static Logger MainLog = new Logger();

    private List<string> MainLogText = new List<string>();

    #region Unity API
    public void Start()
    {
        //throw new NotImplementedException();
    }

    public void Update()
    {
        System.IO.File.WriteAllLines("Log.txt", new List<string>(MainLogText).ToArray());
    }
    #endregion

    #region Public API
   

    public void log(string log)
    {
        MainLogText.Add(log);
    }

    public void log(System.Exception e)
    {
        MainLogText.Add(e.StackTrace);
    }
    #endregion

    #region Static
    public static void Initialize()
    {
        MainLoopable.Instance.RegisterLoop(MainLog);
    }

    public static void Log(string log)
    {
        MainLog.log(log);
    }

    public static void Log(System.Exception e)
    {
        MainLog.log(e);
    }

    public void OnApplicationQuit()
    {
        //throw new NotImplementedException();
    }
    #endregion
}
