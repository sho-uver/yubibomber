using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHand : MonoBehaviour
{
    public string name;
    public string stickerName;
    public Vector3 stickerPos;
    public GameObject sticker;
    public Vector3 stickerScale;
    // Start is called before the first frame update
    void Start()
    {
        name = "装備なし";
        stickerName = "noneSticker";
        stickerPos = new Vector3(0, 0, -1);
        stickerScale = new Vector3(20, 20, 1);
    }

    // Update is called once per frame
    void Update()
    {
        MakeSticker();
    }

    public void MakeSticker()
    {
        if (name == PlayerPrefs.GetString("装備", "装備なし"))
        {
            return;
        }
        Destroy(sticker);
        name = PlayerPrefs.GetString("装備", "装備なし");
        
        if (name == "装備なし")
        {
            stickerName = "noneSticker";
        }

        // ここで変換する
        if (name == "1")
        {
            stickerName = "sticker1";
        }

        if (name == "2")
        {
            stickerName = "sticker2";
        }

        if (name == "3")
        {
            stickerName = "sticker3";
        }

        if (name == "4")
        {
            stickerName = "sticker4";
        }

        if (name == "4")
        {
            stickerName = "sticker4";
        }

        if (name == "5")
        {
            stickerName = "sticker5";
        }

        if (name == "6")
        {
            stickerName = "sticker6";
        }

        if (name == "はなまる")
        {
            stickerName = "hanamaru";
        }

        if (name == "番")
        {
            stickerName = "ban";
        }

        if (name == "100点")
        {
            stickerName = "100Points";
        }
        
        if (stickerName != "None")
        {
            sticker = Instantiate((GameObject)Resources.Load(stickerName), stickerPos, Quaternion.identity);
            sticker.transform.localScale = stickerScale;
            sticker.transform.SetParent(transform, false);
            
        }
    }
}
