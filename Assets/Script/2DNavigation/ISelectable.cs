//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable {

    bool CanControl { get; }
    eSelectableType SelectableType { get; }
    int SelectPriority { get; }

    void Select();
    void Deselect();
}

public enum eSelectableType
{
    NONE = 0,
    TILE = 2,
    CHARACTER = 4 
}
