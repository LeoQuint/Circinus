//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverheadBillboard : MonoBehaviour {

    public enum BillboardType
    {
        LookAt,
        LookAtBlockedX,
        ForwardFacing,
        ForwardFacingBlockedX
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
    [InEditorReadOnly]
    [SerializeField]
    private Camera m_FacingCamera;
    [Header("Animation")]
    [SerializeField]
    private float m_AnimationDuration = 0.3f;
    [SerializeField]
    private Ease m_EaseType = Ease.InOutExpo;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    public BillboardType m_BillboardType = BillboardType.ForwardFacing;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Tweener m_Tweener = null;
    private bool m_IsShowing = false;
    private Vector3 m_OriginalScale;
    
    #region Unity API
    public void Awake()
    {
        m_OriginalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        m_FacingCamera = CameraManager.instance.CurrentCamera;
    }
    #endregion

    #region Public API
    protected void Update()
    {
        FaceCamera();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PopUp();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PopDown();
        }
    }

    public void PopUp()
    {
        m_Tweener.Kill();
        m_Tweener = null;
        m_Tweener = transform.DOScale(m_OriginalScale, m_AnimationDuration);
        m_Tweener.SetEase(m_EaseType);
    }

    public void PopDown()
    {
        m_Tweener.Kill();
        m_Tweener = null;
        m_Tweener = transform.DOScale(Vector3.zero, m_AnimationDuration);
        m_Tweener.SetEase(m_EaseType);
    }
    #endregion

    #region Protect
    protected void FaceCamera()
    {
        switch (m_BillboardType)
        {
            case BillboardType.LookAt:
                {
                    transform.forward = transform.position - m_FacingCamera.transform.position;
                }
                break;
            case BillboardType.LookAtBlockedX:
                {
                    transform.forward = transform.position - m_FacingCamera.transform.position;
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = 0f;
                    transform.eulerAngles = rot;
                }
                break;
            case BillboardType.ForwardFacing:
                {
                    transform.forward = m_FacingCamera.transform.forward;
                }
                break;
            case BillboardType.ForwardFacingBlockedX:
                {
                    transform.forward = m_FacingCamera.transform.forward;
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x = 0f;
                    transform.eulerAngles = rot;
                }
                break;
            default:
                break;
        }
    }
    #endregion

    #region Private
    #endregion
}
