//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteTileData", menuName = "Data/SpriteTileData", order = 3)]
public class SpriteTileData : ScriptableObject {

    [System.Serializable]
    public struct sTileTypeSprite
    {
        public TileType m_Type;
        public Sprite m_Sprite;
    }

    [System.Serializable]
    public struct sModifierSprite
    {
        public eTileModifier m_Type;
        public Sprite m_Sprite;
    }

    [System.Serializable]
    public struct sComponentSprite
    {
        public eShipComponent m_Type;
        public Sprite m_Sprite;
    }

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
    public List<sTileTypeSprite> m_TileSprites;
    public List<sModifierSprite> m_ModifierSprites;
    public List<sComponentSprite> m_ComponentSprites;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Dictionary<TileType, Sprite> m_TileSpritesMap;
    private Dictionary<eTileModifier, Sprite> m_ModifierSpritesMap;
    private Dictionary<eShipComponent, Sprite> m_ComponentSpritesMap;


    #region Unity API
    #endregion

    #region Public API
    public void Init()
    {
        m_TileSpritesMap = new Dictionary<TileType, Sprite>();
        for (int i = 0; i < m_TileSprites.Count; ++i)
        {
            m_TileSpritesMap.Add(m_TileSprites[i].m_Type, m_TileSprites[i].m_Sprite);
        }

        m_ModifierSpritesMap = new Dictionary<eTileModifier, Sprite>();
        for (int i = 0; i < m_ModifierSprites.Count; ++i)
        {
            m_ModifierSpritesMap.Add(m_ModifierSprites[i].m_Type, m_ModifierSprites[i].m_Sprite);
        }

        m_ComponentSpritesMap = new Dictionary<eShipComponent, Sprite>();
        for (int i = 0; i < m_ComponentSprites.Count; ++i)
        {
            m_ComponentSpritesMap.Add(m_ComponentSprites[i].m_Type, m_ComponentSprites[i].m_Sprite);
        }
    }

    public Sprite GetSprite(TileType type)
    {
        return m_TileSpritesMap[type];
    }

    public Sprite GetSprite(eShipComponent type)
    {
        return m_ComponentSpritesMap[type];
    }

    public Sprite GetSprite(eTileModifier type)
    {
        return m_ModifierSpritesMap[type];
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
