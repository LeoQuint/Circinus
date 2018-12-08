//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    bool CanRepair();
    void Damage(float amount);
    void Repair(float amount);
    Vector2Int WorldPosition();
}
