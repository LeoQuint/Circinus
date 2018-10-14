//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config {
    
    public virtual string Filename
    {
        get { return "config"; }
    }

    public virtual SavedPath SavedPath
    {
        get{return SavedPath.Configuration; }
    }
}