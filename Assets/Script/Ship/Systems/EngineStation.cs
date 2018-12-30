//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineStation : ShipComponent {

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
    protected AITask m_EngineTask = null;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private EngineData m_Data;

    #region Unity API
    #endregion

    #region Public API
    public override void Init(Ship ship)
    {
        base.Init(ship);

        CreateEngineTask();
    }

    public override void OnShipUpdate(float deltaTime)
    {
        base.OnShipUpdate(deltaTime);
    }

    public override void Interact(Character character)
    {
        base.Interact(character);
    }

    public override void Disengage(Character character)
    {
        base.Disengage(character);
    }
    #endregion

    #region Protect
    protected void CreateEngineTask()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("target", this);
        parameters.Add("position", WorldPosition());
        m_EngineTask = new AITask(AITask.TaskType.Engine, parameters);

        m_Ship.TaskManager.AddTask(m_EngineTask);
    }

    protected override void LoadData()
    {
        base.LoadData();

    }
    #endregion

    #region Private
    #endregion
}
