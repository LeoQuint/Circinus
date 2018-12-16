//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningController : MonoBehaviour {

    public enum eBurningStatus
    {
        NONE,
        IGNITING,
        BURNING
    }

    public struct sBurningInfo
    {
        public eBurningStatus Status;
        public Vector2Int Position;
        public float m_FireRank;
        public float m_IgnitionRank;
        public BurningComponent ActiveBurningComponent;
        private bool m_WasExtinguished;

        public bool IsExtinguished
        {
            get { return m_WasExtinguished; }
        }

        public float FireRank
        {
            get { return m_FireRank; }
            set
            {
                m_FireRank = value;
                if (m_FireRank > 0)
                {
                    Status = eBurningStatus.BURNING;
                }
                else if (m_IgnitionRank > 0)
                {
                    Status = eBurningStatus.IGNITING;
                    m_WasExtinguished = true;
                }
                else
                {
                    Status = eBurningStatus.NONE;
                }
            }
        }

        public float IgnitionRank
        {
            get { return m_IgnitionRank; }
            set
            {
                m_IgnitionRank = value;
                if (Status != eBurningStatus.BURNING && m_IgnitionRank > 0)
                {
                    Status = eBurningStatus.IGNITING;
                }
            }
        }

        public sBurningInfo(Vector2Int position)
        {
            Position = position;
            Status = eBurningStatus.NONE;
            m_FireRank = 0f;
            m_IgnitionRank = 0f;
            ActiveBurningComponent = null;
            m_WasExtinguished = false;
        }

        public sBurningInfo(int x, int y)
        {
            Position = new Vector2Int(x,y);
            Status = eBurningStatus.NONE;
            m_FireRank = 0f;
            m_IgnitionRank = 0f;
            ActiveBurningComponent = null;
            m_WasExtinguished = false;
        }

        public void Reset()
        {
            Status = eBurningStatus.NONE;
            m_FireRank = 0f;
            m_IgnitionRank = 0f;
            ActiveBurningComponent = null;
            m_WasExtinguished = false;
        }
    }
    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    private const int FIRE_ANIMATION_POOL_SIZE = 5;
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField] protected GameObject m_FireAnimPrefab;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    //debug
    public int x;
    public int y;
    public float debugAmount;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private FloorLayout m_Layout;
   
    private sBurningInfo[][] m_ActiveBurningComponent;
    private Queue<BurningComponent> m_PooledBurningComponent = new Queue<BurningComponent>();

    #region Unity API
    [ContextMenu("Add Fire Rank")]
    private void Test()
    {
        AddFireRank(new Vector2Int(x,y), debugAmount);
    }

    [ContextMenu("Remove Fire Rank")]
    private void Test2()
    {
        RemoveFireRank(new Vector2Int(x, y), debugAmount);
    }
    #endregion

    #region Public API
    public void Init(FloorLayout layout)
    {
        m_Layout = layout;
        m_ActiveBurningComponent = new sBurningInfo[m_Layout.Width][];
        for (int i = 0; i < m_Layout.Width; ++i)
        {
            m_ActiveBurningComponent[i] = new sBurningInfo[m_Layout.Height];
        }

        FillPool();
    }

    public void OnShipUpdate(float deltaTime)
    {
        SetFireRanks(deltaTime);
    }

    public void AddFireRank(Vector2Int location, float amount)
    {
        if (location.x < 0 || location.x >= m_Layout.Width ||
            location.y < 0 || location.y >= m_Layout.Height)
        {
            Debug.LogError("Trying to add Fire to a Location outside this Layout.");
            return;
        }

        if (m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent == null)
        {
            //add animation
            BurningComponent bc = GetFire();
            bc.Init(location);
            m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent = bc;
        }

        m_ActiveBurningComponent[location.x][location.y].FireRank 
            = Mathf.Clamp(m_ActiveBurningComponent[location.x][location.y].FireRank + amount, 0f, GameConstants.MAX_FIRE_RANK);
    }

    public void RemoveFireRank(Vector2Int location, float amount)
    {
        if (location.x < 0 || location.x >= m_Layout.Width ||
             location.y < 0 || location.y >= m_Layout.Height)
        {
            Debug.LogError("Trying to add Fire to a Location outside this Layout.");
            return;
        }

        m_ActiveBurningComponent[location.x][location.y].FireRank
            = Mathf.Clamp(m_ActiveBurningComponent[location.x][location.y].FireRank - amount, 0f, GameConstants.MAX_FIRE_RANK);

        if (m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent != null)
        {
            ReleaseFire(m_ActiveBurningComponent[location.x][location.y]);
            m_ActiveBurningComponent[location.x][location.y] = null;
        }
    }

    public void AddIgnitionRank(Vector2Int location, int amount)
    {
        if (location.x < 0 || location.x >= m_FireRank.Length ||
           location.y < 0 || location.y >= m_FireRank[0].Length)
        {
            Debug.LogError("Trying to Ignite a Location outside this Layout.");
            return;
        }
    }

    public bool IsBurning(Vector2Int location)
    {
        return m_FireRank[location.x][location.y] > 0;
    }
    #endregion

    #region Protect
    //Adds damage to tiles, increase fire rank and ignition in nearby tiles.
    protected void UpdateBurningBehaviour()
    {

    }
    //asigns the ranks
    protected void SetFireRanks(float deltaTime)
    {
        for (int x = 0; x < m_FireRank.Length; ++x)
        {
            for (int y = 0; y < m_FireRank[0].Length; ++y)
            {
                if (m_ActiveBurningComponent[x][y] != null)
                {
                    m_ActiveBurningComponent[x][y].SetFireRank(m_FireRank[x][y]);
                }
            }
        }
    }
    #endregion

    #region Private
    private void UpdateTasks()
    {

    }

    private void GenerateTask()
    {

    }

    private void FillPool()
    {
        for (int i = 0; i < FIRE_ANIMATION_POOL_SIZE; ++i)
        {
            GameObject g = GameObject.Instantiate(m_FireAnimPrefab, transform);
            BurningComponent bc = g.GetComponentInChildren<BurningComponent>();
            bc.ResetComponent();
            m_PooledBurningComponent.Enqueue(bc);
        }
    }

    private BurningComponent GetFire()
    {
        if (m_PooledBurningComponent.Count == 0)
        {
            GameObject g = GameObject.Instantiate(m_FireAnimPrefab, transform);
            BurningComponent bc = g.GetComponentInChildren<BurningComponent>();
            bc.ResetComponent();
            m_PooledBurningComponent.Enqueue(bc);
        }

        return m_PooledBurningComponent.Dequeue();
    }

    private void ReleaseFire(BurningComponent burningComponent)
    {
        //reset
        burningComponent.ResetComponent();
        //re queue
        m_PooledBurningComponent.Enqueue(burningComponent);
    }
    #endregion
}
