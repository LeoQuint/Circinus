//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Observer class receives notices from Subject class. 
/// Use Subject's register method to start listenning.
/// Don't forget to unregister OnDestroy.
/// </summary>
public interface Observer {

    void OnNotify(params object[] notice);

}