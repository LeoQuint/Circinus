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
        private List<BurningInfo> m_Neighbours;

        private bool AreNeighboursSet
        {
            get { return m_Neighbours != null; }
        }

        public List<BurningInfo> Neighbours
        {
            get{ return m_Neighbours; }
        }

        public bool IsExtinguished
        {
            get { return m_WasExtinguished; }
        }

        public bool HasIgnited
        {
            get
            {
                return Status == eBurningStatus.IGNITING && m_IgnitionRank >= GameConstants.IGNITE_TICKS_REQUIRED;
            }
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

        public void SetNeighbours(List<BurningInfo> neighbours)
        {
            m_Neighbours = neighbours;
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
   
    private BurningInfo[][] m_BurningComponents;
    private List<BurningInfo> m_BurningLocations = new List<BurningInfo>();

    private Queue<BurningComponent> m_PooledBurningComponent = new Queue<BurningComponent>();    

#if UNITY_EDITOR
    #region DEBUG
    private void OnDrawGizmosSelected()
    {
        if (m_BurningComponents != null)
        {
            for (int x = 0; x < m_BurningComponents.Length; ++x)
            {
                for (int y = 0; y < m_BurningComponents[0].Length; ++y)
                {
                    Vector3 localPosition = new Vector3(x * Tile.CUBE_SIZE, y * Tile.CUBE_SIZE, 0f);
                    switch (m_BurningComponents[x][y].Status)
                    {
                        case eBurningStatus.NONE:
                            { }
                            break;
                        case eBurningStatus.IGNITING:
                            {
                                float col = m_BurningComponents[x][y].IgnitionRank / GameConstants.IGNITE_TICKS_REQUIRED;
                                Gizmos.color = new Color(col, col, 0, 0.5F);
                                Gizmos.DrawCube(transform.position + localPosition, Vector3.one * Tile.CUBE_SIZE);
                            }
                            break;
                        case eBurningStatus.BURNING:
                            {
                                float col = m_BurningComponents[x][y].FireRank / GameConstants.MAX_FIRE_RANK;
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

        InitFireData();

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

        if (m_BurningComponents[location.x][location.y].ActiveBurningComponent == null)
        {
            //we got a new fire burning

            //add animation
            BurningComponent bc = GetFire();
            bc.Init(location);
            m_BurningComponents[location.x][location.y].ActiveBurningComponent = bc;
            m_BurningLocations.Add(m_BurningComponents[location.x][location.y]);
            //generate task
            GenerateTask(m_BurningComponents[location.x][location.y]);
        }

        m_BurningComponents[location.x][location.y].FireRank += amount;
    }

    public bool RemoveFireRank(Vector2Int location, float amount)
    {
        if (location.x < 0 || location.x >= m_Layout.Width ||
             location.y < 0 || location.y >= m_Layout.Height)
        {
            Debug.LogError("Trying to add Fire to a Location outside this Layout.");
            return true;
        }

        m_BurningComponents[location.x][location.y].FireRank -= amount;

        if (m_BurningComponents[location.x][location.y].ActiveBurningComponent != null)
        {
            if (m_BurningComponents[location.x][location.y].IsExtinguished)
            {
                ReleaseFire(m_BurningComponents[location.x][location.y].ActiveBurningComponent);
                m_BurningComponents[location.x][location.y].ActiveBurningComponent = null;
                m_BurningLocations.Remove(m_BurningComponents[location.x][location.y]);
                m_BurningComponents[location.x][location.y].IgnitionRank = 0f;
                m_BurningComponents[location.x][location.y].Status = eBurningStatus.NONE;
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }

    }
    #endregion

    #region Protect
    //Adds damage to tiles, increase fire rank and ignition in nearby tiles.
    protected void UpdateBurningBehaviour(float deltaTime)
    {
        List<Vector2Int> newlyCaughtFire = new List<Vector2Int>();
        for (int i = 0; i < m_BurningLocations.Count; ++i)
        {
            //each burning tile grow in strengh
            m_BurningLocations[i].FireRank += (GameConstants.FIRE_GROWTH_PER_SECS * deltaTime);
            //each burning tile adds to ignitions
            foreach (BurningInfo bi in m_BurningLocations[i].Neighbours)
            {
                bi.IgnitionRank += (GameConstants.FIRE_ADJACENTE_IGNITE_TICKS_PER_RANK_SECS * m_BurningLocations[i].FireRank * deltaTime);

                if (bi.HasIgnited && !newlyCaughtFire.Contains(bi.Position))
                {
                    newlyCaughtFire.Add(bi.Position);
                }
            }
        }

        for (int i = 0; i < newlyCaughtFire.Count; ++i)
        {
            AddFireRank(newlyCaughtFire[i], 1f);
        }
    }
    #endregion

    #region Private
    private void InitFireData()
    {
        m_BurningComponents = new BurningInfo[m_Layout.Width][];
        //create all info
        for (int x = 0; x < m_Layout.Width; ++x)
        {
            m_BurningComponents[x] = new BurningInfo[m_Layout.Height];
            for (int y = 0; y < m_Layout.Height; ++y)
            {
                m_BurningComponents[x][y] = new BurningInfo(x, y);
            }
        }
        //set all the neighbours
        for (int x = 0; x < m_BurningComponents.Length; ++x)
        {
            for (int y = 0; y < m_BurningComponents[0].Length; ++y)
            {
                m_BurningComponents[x][y].SetNeighbours(GetNeighbours(x, y));
            }
        }
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

    private List<BurningInfo> GetNeighbours(int x, int y)
    {
        List<BurningInfo> neighbours = new List<BurningInfo>();
        //up
        if (y < m_Layout.Height-1)
        {
            neighbours.Add(m_BurningComponents[x][y + 1]);
        }
        //down
        if (y > 0)
        {
            neighbours.Add(m_BurningComponents[x][y - 1]);
        }
        //left
        if (x > 0)
        {
            neighbours.Add(m_BurningComponents[x - 1][y]);
        }
        //right
        if (x < m_Layout.Width - 1)
        {
            neighbours.Add(m_BurningComponents[x + 1][y]);
        }
        return neighbours;
    }
    #endregion

    #region Task Generation
    private void GenerateTask(BurningInfo bi)
    {        
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("position", bi.Position);
        parameters.Add("controller", this);
        AITask firefightingTask = new AITask(AITask.TaskType.FireFight, parameters);
        AITaskManager.Instance.AddTask(firefightingTask);
    }
    #endregion
}
