using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Photon.Pun;

[RequireComponent(typeof(CanvasGroup))]
public class Drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Action OnBeginDragAction;
    public Action OnDragAction;
    public Action OnEndDragAction;
    private CanvasGroup _canvasGroup;
    private Vector2 _startPosition;
    private Camera _uiCamera;
    private PhotonView photonView;
    public bool turnFlag;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        SetScreenSpaveCamera(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());
        // _startPosition = transform.position;
    }

    private void OnDestroy()
    {
        OnBeginDragAction = null;
        OnDragAction = null;
        OnEndDragAction = null;
        _uiCamera = null;
    }

    public void SetScreenSpaveCamera(Camera uiCamera)
    {
        _uiCamera = uiCamera;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        _startPosition = transform.position;
        if (gameObject.GetComponent<Hand>().GetFinger() == 0){return; }
        if (!photonView.IsMine){return; }
        if (!turnFlag)
        {
            
            return;
        }
        if (enabled == false)
        {
            return; 
        }
        
        _canvasGroup.blocksRaycasts = false;
        if (OnBeginDragAction != null)
        {
            OnBeginDragAction();
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (!turnFlag)
        {
            return; 
        }
        if (gameObject.GetComponent<Hand>().GetFinger() == 0){return; }
        if (!photonView.IsMine){return; }
        if (enabled == false)
        {
            return;
        }
        transform.position = pointerEventData.position;
        
        if (_uiCamera != null)
        {
            var position = _uiCamera.ScreenToWorldPoint(pointerEventData.position);
            position.z = transform.position.z;
            transform.position = position;
        }
        else
        {
            transform.position = pointerEventData.position;
        }
        

        if (OnDragAction != null){OnDragAction();}
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        if (!turnFlag){return; }
        if (gameObject.GetComponent<Hand>().GetFinger() == 0){return; }
        if (!photonView.IsMine){return; }
        _canvasGroup.blocksRaycasts = true;
        transform.position = _startPosition;
        if (enabled == false){return; }
        if (OnEndDragAction != null){OnEndDragAction();}

    }

    public void SetTurnFlag(bool flag)
    {
        turnFlag = flag;
    }

    public bool GetTurnFlag()
    {
        return turnFlag;
    }
}
