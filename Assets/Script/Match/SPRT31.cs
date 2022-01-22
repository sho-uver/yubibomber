using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SPRT31 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // プレイヤーが部屋に入る時、ゲームシステムに呼び出される
    public void SetSystem(GameObject gameObject)
    {
        if (gameObject.tag == "BattleSystem")
        {
            SeparateCall(gameObject);
        }
        if (gameObject.tag == "VSAI_BattleSystem")
        {
            VSAI_SeparateCall(gameObject);
        }
    }

    // 受け渡されたプレイヤーをUIのターゲットとして設定し、ターゲットの処理を呼び出す。 
    public void SeparateCall(GameObject gameObject)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventDate) => { gameObject.GetComponent<Battle>().Separate31(); });
        trigger.triggers.Add(entry);
    }
    public void VSAI_SeparateCall(GameObject gameObject)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventDate) => { gameObject.GetComponent<VSAI_Battle>().Separate31(); });
        trigger.triggers.Add(entry);
    }
}