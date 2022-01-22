using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Hand : MonoBehaviour
{
    public int finger;
    private PhotonView photonView;
    private Animator animator;
    public GameObject battleSystem;
    public Battle battle;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    public AudioSource VSAISE;
    public string stickerName;
    public Vector3 stickerPos;
    public GameObject sticker;

    // Start is called before the first frame update
    void Start()
    {
        finger = 1;
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        photonView = gameObject.GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        battle = GameObject.Find("BattleSystem").GetComponent<Battle>();
        VSAISE = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetFinger()
    {
        return finger;
    }

    public void SetFinger(int num)
    {
        finger = num;
        photonView.RPC(nameof(RPCUpdateFinger),RpcTarget.AllViaServer,finger);
    }

    public void PlusFinger(int plusFinger)
    {
        finger += plusFinger;
        if (finger == 5)
        {
            finger = 0;
        } 
        if (finger > 5)
        {
            finger -= 5;
        }
        photonView.RPC(nameof(RPCUpdateFinger),RpcTarget.AllViaServer,finger);
        photonView.RPC(nameof(RPCCallCSB),RpcTarget.Others);
    }

    [PunRPC]
    void RPCUpdateFinger(int updateFinger, PhotonMessageInfo info)
    {
        finger = updateFinger;
        animator.SetInteger("finger",finger);
        if (finger == 0)
        {
            VSAISE.clip = bomberSE;
            sticker.SetActive(false);
            
        }
        else
        {
            VSAISE.clip = actionSE;
            if(!sticker.activeSelf)
            {
                sticker.SetActive(true);
            }
        }
        VSAISE.Play();
    }
    
    [PunRPC]
    void RPCCallCSB()
    {
        battle.CreateSeparateButton();
    }
    
    public void MakeSticker()
    {
        photonView.RPC(nameof(RPCMakeSticker),RpcTarget.AllViaServer,PlayerPrefs.GetString("装備", "装備なし"));
        /*
        var name = "装備なし";
        stickerPos = new Vector3(0, 0, -1);
        // name = _name;
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

        //---

        if (stickerName != "None")
        {
            // PhotonNetwork.Instantiate("RightHand", new Vector2(-200f,220f), Quaternion.Euler(0,0,180));
            sticker = PhotonNetwork.Instantiate(stickerName, stickerPos, Quaternion.identity);
            sticker.transform.SetParent(transform, false);
        }
        */
    }

    [PunRPC]
    void RPCMakeSticker(string _name)
    {
        Debug.Log("MakeSticker");
        var name = "装備なし";
        stickerPos = new Vector3(0, 0, -1);
        name = _name;
        
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

        //---

        if (stickerName != "None")
        {
            // PhotonNetwork.Instantiate("RightHand", new Vector2(-200f,220f), Quaternion.Euler(0,0,180));
            //sticker = photnonView.Instantiate(stickerName, stickerPos, Quaternion.identity);
            sticker = Instantiate((GameObject)Resources.Load(stickerName), stickerPos, Quaternion.identity);
            sticker.transform.SetParent(transform, false);
        }
    }
}
