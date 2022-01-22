using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class Drop : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Action OnPointerEnterAction;
    public Action OnPointerExitAction;
    public Action OnDropAction;
    private PhotonView photonView;

    private void OnDestroy()
    {
        OnPointerEnterAction = null;
        OnPointerExitAction = null;
        OnDropAction = null;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (OnPointerEnterAction != null)
        {
            OnPointerEnterAction();
        }
    }

    public void OnPointerExit(PointerEventData pinterEventData)
    {
        if (OnPointerExitAction != null)
        {
            OnPointerExitAction();
        }
    }

    public void OnDrop(PointerEventData pointerEventData)
    {
        if (gameObject.GetComponent<Hand>().GetFinger() == 0){return; }
        if (photonView.IsMine){return; }
        if (!pointerEventData.pointerDrag.GetComponent<PhotonView>().IsMine){return; }
        if (!pointerEventData.pointerDrag.GetComponent<Drag>().GetTurnFlag()){return; }
        if (OnDropAction != null)
        {
            OnDropAction();
        }
        gameObject.GetComponent<Hand>().PlusFinger((pointerEventData.pointerDrag.GetComponent<Hand>().GetFinger()));
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
