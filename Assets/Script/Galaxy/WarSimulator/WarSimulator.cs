//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using War;

public class WarSimulator : Subject {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static WarSimulator instance;
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    ///*************Factions*************//
    //Red
    private Faction m_RedFaction;
    private AreaDrawer m_RedArea;
    //Blue
    private Faction m_BlueFaction;
    private AreaDrawer m_BlueArea;
    //Neutral
    private Faction m_NeutralFaction;
    private AreaDrawer m_NeutralArea;
    ///************************************//
    private Galaxy m_Galaxy;

    private EFaction m_CurrentTurn;
    private float m_NextTurnTimer = 5f;

    #region Unity API
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    protected void Start()
    {
        //Init();
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Turn();
            UpdateAreas();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimulateOutbreak();
        }
    }
    #endregion

    #region Public API
    public void Init()
    {
        m_RedFaction        = new Faction(EFaction.Red);
        m_RedArea           = transform.Find("RedArea").GetComponent<AreaDrawer>();
        m_BlueFaction       = new Faction(EFaction.Blue);
        m_BlueArea          = transform.Find("BlueArea").GetComponent<AreaDrawer>();
        m_NeutralFaction    = new Faction(EFaction.Neutral);
        m_NeutralArea       = transform.Find("NeutralArea").GetComponent<AreaDrawer>();

        SimulateOutbreak();
        InitializeAreaDrawers();
        Turn();
    }

    public void Load(ref Galaxy galaxy)
    {
        console.logStatus("Loading War Simulator.");
        m_Galaxy = galaxy;
        Init();
    }

    public void Save()
    {

    }

    public void Turn()
    {
        m_NextTurnTimer = Time.time + GameConstants.TURN_DURATION;
        NextTurn();
        switch (m_CurrentTurn)
        {
            case EFaction.Red:
                m_RedFaction.TakeTurn();
                break;
            case EFaction.Blue:
                m_BlueFaction.TakeTurn();
                break;
            case EFaction.Neutral:
                m_NeutralFaction.TakeTurn();
                break;
            default:
                break;
        }
        ShowInfo();
    }

    public void SimulateInitialTurns()
    {
        for (int i = 0; i < GameConstants.OUTBREAK_DURATION_IN_TURNS; ++i)
        {
            Turn();
            Star flipped = GetMostVulnerableStar(m_CurrentTurn);
            if (flipped != null)
            {
                console.logInfo("System flipped from " + flipped.System.m_ControllingFaction.ToString() + " to " + m_CurrentTurn.ToString());
                flipped.System.m_ControllingFaction = m_CurrentTurn;
            }

        }
        GalaxyGenerator.instance.SaveGalaxy();
        InitializeAreaDrawers();//debug
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private EFaction NextTurn()
    {
        if (m_CurrentTurn == EFaction.Neutral)
        {
            m_CurrentTurn = EFaction.Red;
        }
        else
        {
            ++m_CurrentTurn;
        }
        return m_CurrentTurn;
    }

    private void ShowInfo()
    {
        int numberOfSystemControlled = 0;
        int overallStrength = 0;
        for (int i = 0; i < m_Galaxy.m_GalacticMap.Count; ++i)
        {
            for (int j = 0; j < m_Galaxy.m_GalacticMap[i].Count; ++i)
            {
                if (m_Galaxy.m_GalacticMap[i][j].m_ControllingFaction == m_CurrentTurn)
                {
                    ++numberOfSystemControlled;
                    overallStrength += m_Galaxy.m_GalacticMap[i][j].LocalForcesStrength;
                }                
            }
        }
        console.logInfo(m_CurrentTurn.ToString() + " System Controlled: " + numberOfSystemControlled + " Total Strength: " + overallStrength.ToLargeValues());
    }

    /// <summary>
    /// Simulate the start of the war by letting players pick star systems
    /// one after an other.
    /// </summary>
    private void SimulateOutbreak()
    {
        for (int i = 0; i < m_Galaxy.m_GalacticMap.Count; ++i)
        {
            for (int j = 0; j < m_Galaxy.m_GalacticMap[i].Count; ++i)
            {
                m_Galaxy.m_GalacticMap[i][j].SetControllingFaction((EFaction)Random.Range(0, (int)EFaction.COUNT));  
            }
        }            
        GalaxyGenerator.instance.SaveGalaxy();
    }    

    private Star GetMostVulnerableStar(EFaction faction)
    {
        int largestBalanceOfPower = 0;
        Star mostVulnerable = null;
        for (int i = 0; i < GalaxyGenerator.instance.StarList.Length; ++i)
        {
            for (int j = 0; j < GalaxyGenerator.instance.StarList[i].Length; ++j)
            {
                List<Star> nearStars = GalaxyGenerator.instance.StarList[i][j].NearStars;
                int redStars = 0;
                int blueStars = 0;
                for (int k = 0; k < nearStars.Count; ++k)
                {
                    int balanceOfLocalPower = 0;
                    switch (nearStars[k].System.m_ControllingFaction)
                    {
                        case EFaction.Red:
                            ++redStars;
                            break;
                        case EFaction.Blue:
                            ++blueStars;
                            break;
                        default:
                            break;
                    }
                    switch (faction)
                    {
                        case EFaction.Red:
                            {
                                balanceOfLocalPower = redStars - blueStars;
                            }
                            break;
                        case EFaction.Blue:
                            {
                                balanceOfLocalPower = blueStars - redStars;
                            }
                            break;
                        default:
                            break;
                    }
                    if (balanceOfLocalPower > largestBalanceOfPower)
                    {
                        largestBalanceOfPower = balanceOfLocalPower;
                        mostVulnerable = nearStars[k];
                    }
                }
                
            }
        }

        return mostVulnerable;
    }

    private void InitializeAreaDrawers()
    {
        m_RedArea.Init();
        m_BlueArea.Init();
        m_NeutralArea.Init();
        UpdateAreas();
    }

    private void UpdateAreas()
    {
        List<Vector3> redStars = new List<Vector3>();
        List<Vector3> blueStars = new List<Vector3>();
        List<Vector3> neutralStars = new List<Vector3>();

        for (int i = 0; i < m_Galaxy.m_GalacticMap.Count; ++i)
        {
            for (int j = 0; j < m_Galaxy.m_GalacticMap[i].Count; ++i)
            {
                switch (m_Galaxy.m_GalacticMap[i][j].m_ControllingFaction)
                {
                    case EFaction.Red:
                        redStars.Add(       m_Galaxy.m_GalacticMap[i][j].m_Position);
                        break;
                    case EFaction.Blue:
                        blueStars.Add(      m_Galaxy.m_GalacticMap[i][j].m_Position);
                        break;
                    case EFaction.Neutral:
                        neutralStars.Add(   m_Galaxy.m_GalacticMap[i][j].m_Position);
                        break;
                    case EFaction.COUNT:
                        break;
                    default:
                        break;
                }
            }
        }
        m_RedArea.Points = redStars;
        m_BlueArea.Points = blueStars;
        m_NeutralArea.Points = neutralStars;
    }
    #endregion
}
/*

AI Rules for war simulation:

    "TURN BASED"

    Turns will last a set amount of time.

    ->Star System will supply ships(Strenght) overtime.
        -Star System have varying production.
        -Better System will be better defended.
        -Star system produce ArmyGroups. The overall strength of the group will be choosen randomly based on their
         type.

    ->EArmyGroupType defines the type of "Unit".
        -This affect's the unit's overall performance executing various tasks(invasion/hit and run/defence/etc.)
        -The type can be changed but takes time and while waiting makes the unit more vulnerable(Reorganizing)
        -Each types have varying disctances they can cover without resupply(stopping at the base).

*/
