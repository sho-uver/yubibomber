using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VSAI_Hand : MonoBehaviour
{
    public int finger;
    private Animator animator;
    public GameObject battleSystem;
    public VSAI_Battle battle;
    public bool isPlayer;
    public bool isAI;
    public bool turnFlag;
    public float speed;
    public GameObject target;
    public Vector2 startPosition;
    [SerializeField]
    public GameObject leftHandM;
    [SerializeField]
    public GameObject rightHandM;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    public AudioSource VSAISE;
    public int LRMAI;
    public string stickerName;
    public Vector3 stickerPos;
    public GameObject sticker;
    public int AILevel;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        AILevel = Menu.GetAILevel();
        finger = 1;
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        animator = GetComponent<Animator>();
        battle = GameObject.Find("BattleSystem").GetComponent<VSAI_Battle>();
        startPosition = gameObject.transform.position;
        VSAISE = gameObject.GetComponent<AudioSource>();
        MakeSticker();
    }

    // Update is called once per frame
    void Update()
    {
        if(turnFlag || isPlayer) {return ;}
        waitTime += Time.deltaTime;
        if (0.1f > waitTime) {return ;}
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        if (transform.position == target.transform.position)
        {
            target.GetComponent<VSAI_Hand>().PlusFinger(finger);
            turnFlag = true;
            transform.position = startPosition;
            waitTime = 0;
        }
        
    }

    public int GetFinger()
    {
        return finger;
    }

    public void SetFinger(int num)
    {
        finger = num;
        UpdateFinger(finger);
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
        UpdateFinger(finger);
        CallCSB();
    }

    void UpdateFinger(int updateFinger)
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
    
    void CallCSB()
    {
        battle.CreateSeparateButton();
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }

    public void SetTurnFlag(bool flg)
    {
        turnFlag = flg;
    }

    public bool GetTurnFlag()
    {
        return turnFlag;
    }
    
    public void MakeSticker()
    {
        var name = "装備なし";
        switch (AILevel)
        {
            case 1:
                name = "1";
                break;
            case 2:
                name = "2";
                break;
            case 3:
                name = "3";
                break;
            case 4:
                name = "4";
                break;
            case 5:
                name = "5";
                break;
            case 6:
                name = "6";
                break;

        }
        switch(LRMAI)
        {
            // lMine
            case 1:
                stickerPos = new Vector3(0, 0, -1);
                name = PlayerPrefs.GetString("装備", "装備なし");
                break;
            // rMine
            case 2:
                stickerPos = new Vector3(0, 0, -1);
                name = PlayerPrefs.GetString("装備", "装備なし");
                break;
            // lAI
            case 3:
                stickerPos = new Vector3(0, 0, -1);
                
                break;
            // rAI
            case 4:
                stickerPos = new Vector3(0, 0, -1);
                break;

        }
        
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
            sticker = Instantiate((GameObject)Resources.Load(stickerName), stickerPos, Quaternion.identity);
            sticker.transform.SetParent(transform, false);
        }
    }

    public int GetLRMAI()
    {
        return LRMAI;
    }


}
