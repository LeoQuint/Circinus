﻿//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

[System.Serializable]
[XmlRoot("ShipData")]
public class ShipData {

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
    public string Name;
    //propulsion
    public float BaseAcceleration;
    public float BaseMaxSpeed;
    //steering
    public float BaseSteeringAccelerationSpeed;
    public float BaseSteeringMaxSpeed;
    //stats
    public float HullWeight;
    public float BaseHullHealth;

	////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

	////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    //properties
    public float Weight
    {
        get { return 0f; }
    }

    public float Acceleration
    {
        get { return 0f; }
    }

    public float SteeringSpeed
    {
        get { return 0f; }
    }

    #region Unity API
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
