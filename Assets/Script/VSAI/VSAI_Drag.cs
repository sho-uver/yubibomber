using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class VSAI_Drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Action OnBeginDragAction;
    public Action OnDragAction;
    public Action OnEndDragAction;
    private CanvasGroup _canvasGroup;
    private Vector2 _startPosition;
    private Camera _uiCamera;
    public bool turnFlag;
    public bool isAI;
    public bool isPlayer; 

    private void Start()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        SetScreenSpaceCamera(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>());
        _startPosition = transform.position;
    }

    public void Update()
    {
    
    }

    private void OnDestroy()
    {
        OnBeginDragAction = null;
        OnDragAction = null;
        OnEndDragAction = null;
        _uiCamera = null;
    }

    public void SetScreenSpaceCamera(Camera uiCamera)
    {
        _uiCamera = uiCamera;
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        if (gameObject.GetComponent<VSAI_Hand>().GetFinger() == 0){return; }
        if (!turnFlag){return; }
        if (isAI){return; }
        if (enabled == false){return; }
        _canvasGroup.blocksRaycasts = false;
        if (OnBeginDragAction != null)
        {
            OnBeginDragAction();
        }
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (!turnFlag){return; }
        if (isAI){return; }
        if (gameObject.GetComponent<VSAI_Hand>().GetFinger() == 0){return; }
        if (enabled == false){return; }
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
        // if (!turnFlag){return; }
        if (gameObject.GetComponent<VSAI_Hand>().GetFinger() == 0){return; }
        _canvasGroup.blocksRaycasts = true;
        transform.position = _startPosition;
        if (enabled == false){return; }
        if (OnEndDragAction != null){OnEndDragAction();}
    }

    public void OnBeginDragAI(Vector2 moveAI)
    {

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
