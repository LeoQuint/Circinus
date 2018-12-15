//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameConstants {

    /// <summary>
    /// How long the AI's turns lasts. Used in the simulation. 
    /// The player will be unaware of any turn taking.
    /// </summary>
    public const float TURN_DURATION = 10f;

    public const int MAX_DECISION_NORMAL = 5;


    ///SHIP
    //Fire:
    public const float MAX_FIRE_RANK = 100f;
    public const float FIRE_GROWTH_PER_SECS = 1f;
    public const float FIRE_ADJACENTE_IGNITE_TICKS_PER_RANK_SECS = 0.1f;
    public const float IGNITE_TICKS_REQUIRED = 100f;
    public const float IGNITE_TICKS_RATIO_AFTER_IGNITION = 0.5f;   
    public const float FIRE_DAMAGE_PER_RANK_PER_SECS = 0.1f;


}