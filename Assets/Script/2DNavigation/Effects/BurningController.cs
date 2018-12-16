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

    public class BurningInfo
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
                m_FireRank = Mathf.Clamp( value, 0f, GameConstants.MAX_FIRE_RANK);
                
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

                if (ActiveBurningComponent != null)
                {
                    ActiveBurningComponent.SetFireRank(m_FireRank);
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

        public BurningInfo(Vector2Int position)
        {
            Position = position;
            Status = eBurningStatus.NONE;
            m_FireRank = 0f;
            m_IgnitionRank = 0f;
            ActiveBurningComponent = null;
            m_WasExtinguished = false;
        }

        public BurningInfo(int x, int y)
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
   
    private BurningInfo[][] m_ActiveBurningComponent;
    private List<BurningInfo> m_BurningLocations = new List<BurningInfo>();
    private List<BurningInfo> m_IgnitingLocations = new List<BurningInfo>();

    private Queue<BurningComponent> m_PooledBurningComponent = new Queue<BurningComponent>();

#if UNITY_EDITOR
    #region DEBUG
    private void OnDrawGizmosSelected()
    {
        if (m_ActiveBurningComponent != null)
        {
            for (int x = 0; x < m_ActiveBurningComponent.Length; ++x)
            {
                for (int y = 0; y < m_ActiveBurningComponent[0].Length; ++y)
                {
                    Vector3 localPosition = new Vector3(x * Tile.CUBE_SIZE, y * Tile.CUBE_SIZE, 0f);
                    switch (m_ActiveBurningComponent[x][y].Status)
                    {
                        case eBurningStatus.NONE:
                            { }
                            break;
                        case eBurningStatus.IGNITING:
                            {
                                float col = m_ActiveBurningComponent[x][y].IgnitionRank / GameConstants.IGNITE_TICKS_REQUIRED;
                                Gizmos.color = new Color(col, col, 0, 0.5F);
                                Gizmos.DrawCube(transform.position + localPosition, Vector3.one * Tile.CUBE_SIZE);
                            }
                            break;
                        case eBurningStatus.BURNING:
                            {
                                float col = m_ActiveBurningComponent[x][y].FireRank / GameConstants.MAX_FIRE_RANK;
                                Gizmos.color = new Color(col, 0f, 0, 0.5F);
                                Gizmos.DrawCube(transform.position + localPosition, Vector3.one * Tile.CUBE_SIZE);
                            }
                            break;
                    }                   
                }
            }
        }
       
    }
    #endregion
#endif

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
        m_ActiveBurningComponent = new BurningInfo[m_Layout.Width][];
        for (int x = 0; x < m_Layout.Width; ++x)
        {
            m_ActiveBurningComponent[x] = new BurningInfo[m_Layout.Height];
            for (int y = 0; y < m_Layout.Height; ++y)
            {
                m_ActiveBurningComponent[x][y] = new BurningInfo(x,y);
            }
        }

        FillPool();
    }

    public void OnShipUpdate(float deltaTime)
    {
        UpdateBurningBehaviour(deltaTime);
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
            m_BurningLocations.Add(m_ActiveBurningComponent[location.x][location.y]);
        }

        m_ActiveBurningComponent[location.x][location.y].FireRank += amount;
    }

    public void RemoveFireRank(Vector2Int location, float amount)
    {
        if (location.x < 0 || location.x >= m_Layout.Width ||
             location.y < 0 || location.y >= m_Layout.Height)
        {
            Debug.LogError("Trying to add Fire to a Location outside this Layout.");
            return;
        }

        m_ActiveBurningComponent[location.x][location.y].FireRank -= amount;

        if (m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent != null)
        {
            ReleaseFire(m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent);
            m_ActiveBurningComponent[location.x][location.y].ActiveBurningComponent = null;
            m_BurningLocations.Remove(m_ActiveBurningComponent[location.x][location.y]);
        }
    }
    #endregion

    #region Protect
    //Adds damage to tiles, increase fire rank and ignition in nearby tiles.
    protected void UpdateBurningBehaviour(float deltaTime)
    {
        //each burning tile grow in strengh
        for (int i = 0; i < m_BurningLocations.Count; ++i)
        {
            m_BurningLocations[i].FireRank += (GameConstants.FIRE_GROWTH_PER_SECS * deltaTime);         
        }
    }  
    #endregion

    #region Private
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

    #region Task Generation
    private List<AITask> m_ActiveTasks = new List<AITask>();

    private void UpdateTasks()
    {

    }

    private void GenerateTask(Vector2Int location)
    {

    }

    private void SetFirefightingTasks()
    {

    }
    #endregion
}
