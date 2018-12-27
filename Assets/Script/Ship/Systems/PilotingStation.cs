//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotingStation : ShipComponent {

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
    protected AITask m_PilotingTask = null;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public override void Init(Ship ship, Tile tile)
    {
        base.Init(ship, tile);

        CreatePilotingTask();
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
    protected void CreatePilotingTask()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("target", this);
        parameters.Add("position", WorldPosition());
        m_PilotingTask = new AITask(AITask.TaskType.Pilot, parameters);

        AITaskManager.Instance.AddTask(m_PilotingTask);
    }
    #endregion

    #region Private
    #endregion
}
