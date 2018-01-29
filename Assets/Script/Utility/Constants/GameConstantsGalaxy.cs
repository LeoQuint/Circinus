//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War;

public partial class GameConstants {

    /// <summary>
    /// Range of movement of army group types.
    /// </summary>
    public readonly Dictionary<EArmyGroupType, float> ARMY_GROUP_RANGE = new Dictionary<EArmyGroupType, float>()
    {
        { EArmyGroupType.PlanetaryDefence, 5f },
        { EArmyGroupType.ExpeditionaryForce, 10f },
        { EArmyGroupType.MainCorp, 25f },
        { EArmyGroupType.ScoutingForce, 50f },
        { EArmyGroupType.FastDeployementForce, 100f }
    };

    public const float GALAXY_MAX_DISTANCE_TO_NEAR_STARS = 20f;
}
