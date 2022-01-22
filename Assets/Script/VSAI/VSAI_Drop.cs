using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class VSAI_Drop : MonoBehaviour,IDropHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Action OnPointerEnterAction;
    public Action OnPointerExitAction;
    public Action OnDropAction;
    [SerializeField]
    public GameObject otherHand;
    public bool turnFlag;
    [SerializeField]
    public GameObject gameSystem;



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
        if (gameObject.GetComponent<VSAI_Hand>().GetFinger() == 0){return; }
        // if (!pointerEventData.pointerDrag.GetComponent<VSAI_Drag>().GetTurnFlag()){return; }
        if(otherHand == pointerEventData.pointerDrag || gameObject == pointerEventData.pointerDrag) {return; }
        if (!turnFlag) {return ;}
        if (OnDropAction != null)
        {
            OnDropAction();
        }
        gameObject.GetComponent<VSAI_Hand>().PlusFinger((pointerEventData.pointerDrag.GetComponent<VSAI_Hand>().GetFinger()));

    }
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.Find("BattleSystem");
    }

    // Update is called once per frame
    void Update()
    {
        turnFlag = gameSystem.GetComponent<VSAI_Battle>().GetTurnFlag();
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
