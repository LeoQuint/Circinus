//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreUtility;

public class WeaponStation : ShipComponent {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected AITask m_WeaponControlTask = null;

    protected AnimatedVisuals m_Visuals;

    //weapons stats
    protected float m_WeaponCooldown = 2f;

    protected Timer m_WeaponTimer;
    protected bool m_HasGunner = false;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API

    #endregion

    #region Public API
    public override void Init(Ship ship)
    {
        base.Init(ship);

        m_WeaponTimer = new Timer(m_WeaponCooldown);
        m_HasGunner = false;
        CreateWeaponControlTask();
    }

    public override void OnShipUpdate(float deltaTime)
    {
        base.OnShipUpdate(deltaTime);

        if (m_WeaponTimer != null)
        {
            m_WeaponTimer.Update();
        }
    }

    public override void Interact(Character character)
    {
        base.Interact(character);
        m_HasGunner = true;
    }

    public override void Disengage(Character character)
    {
        base.Disengage(character);
        m_HasGunner = false;
    }
    #endregion

    #region Protect
    protected override void LoadData()
    {
        base.LoadData();
        //TODO: Load data
    }

    protected void CreateWeaponControlTask()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("target", this);
        parameters.Add("position", WorldPosition());
        m_WeaponControlTask = new AITask(AITask.TaskType.Weapons, parameters);

        m_Ship.TaskManager.AddTask(m_WeaponControlTask);
    }

    protected void Fire()
    {

    }

    protected void SearchTarget()
    {

    }
    #endregion

    #region Private
    #endregion
}
