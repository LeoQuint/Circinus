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
    private OnHoverOver m_TargetElement;
    private TextMesh m_Text;

    /// <summary>
    /// Properties
    /// </summary>
    public string Text
    {
        get { return m_Text.text; }
        set { m_Text.text = value;  }
    }

    #region Unity API 
    protected void Update()
    {
        if (m_IsShowing)
        {
            FaceCamera();
        }
    }
    #endregion

    #region Public API
    public void Init(OnHoverOver overElement)
    {
        m_OriginalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        m_FacingCamera = CameraManager.instance.CurrentCamera;
        m_TargetElement = overElement;
        m_Text = GetComponent<TextMesh>();
        if (m_TargetElement != null)
        {
            m_TargetElement.OnMouseEnterDelegate += PopUp;
            m_TargetElement.OnMouseExitDelegate += PopDown;
        }
    }

    public void PopUp()
    {
        m_IsShowing = true;
        m_Tweener.Kill();
        m_Tweener = null;
        m_Tweener = transform.DOScale(m_OriginalScale, m_AnimationDuration);
        m_Tweener.SetEase(m_EaseType);
    }

    public void PopDown()
    {
        m_IsShowing = false;
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
