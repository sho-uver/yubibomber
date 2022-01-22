using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Photon.Pun;
// using Photon.Realtime;
using UnityEngine.SceneManagement;
// using Hashtable = ExitGames.Client.Photon.Hashtable;

public class VSAI_Battle : MonoBehaviour
{
    // public const string GAME_MODE_PROP_KEY = "GameMode";
    // public const byte expectedMaxPlayers = 2;
    // public string gameMode;
    public int AILevel;
    [SerializeField]
    public GameObject camera;
    [SerializeField]
    public GameObject Board;
    public GameObject leftHand;
    public GameObject rightHand;
    [SerializeField]
    public GameObject canvas;
    public bool gameStartFlag;
    [SerializeField]
    public GameObject leftHandM;
    [SerializeField]
    public GameObject rightHandM;
    public Vector2 sepPos1;
    public Vector2 sepPos2;
    // pattern2
    [SerializeField]
    public Button btn11;
    [SerializeField]
    public Button btn20;
    // pattern3
    [SerializeField]
    public Button btn21;
    [SerializeField]
    public Button btn30;
    // pattern4
    [SerializeField]
    public Button btn31;
    [SerializeField]
    public Button btn22;
    [SerializeField]
    public Button btn40;
    // pattern5
    [SerializeField]
    public Button btn41;
    [SerializeField]
    public Button btn32;
    // pattern6
    [SerializeField]
    public Button btn42;
    [SerializeField]
    public Button btn33;
    public Quaternion btnRotation;
    public int sumFinger;
    // private PhotonView photonView;
    public bool turnFlag;
    [SerializeField]
    public Text turnText;
    [SerializeField]
    public GameObject desk;
    [SerializeField]
    public Text timeText;
    public float restTime;
    public Vector2 textPos1;
    public Vector2 textPos2;
    public Vector2 timeTextPos1;
    public Vector2 timeTextPos2;
    public Vector2 boardPos1;
    public Vector2 boardPos2;
    public Vector2 deskPos1;
    public Vector2 deskPos2;
    [SerializeField]
    public GameObject leftHandAI;
    [SerializeField]
    public GameObject rightHandAI;
    [SerializeField]
    public AudioClip finishSE;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    [SerializeField]
    public AudioClip battleBGM;
    public AudioSource VSAISE;
    public int lFingerM;
    public int rFingerM;
    public int lFingerAI;
    public int rFingerAI;
    public int money;



    // Start is called before the first frame update
    void Start()
    {
        // gameMode = Menu.GetGameMode();
        AILevel = Menu.GetAILevel();
        switch(AILevel)
        {
            case 1:
                money = 100;
                break;
            case 2:
                money = 200;
                break;
            case 3:
                money = 300;
                break;
            case 4:
                money = 400;
                break;
            case 5:
                money = 500;
                break;
            case 6:
                money = 600;
                break;
        }
        // canvas = GameObject.FindGameObjectWithTag("Canvas");
        gameStartFlag = true;
        int lFingerM = leftHandM.GetComponent<VSAI_Hand>().GetFinger();
        int rFingerM = rightHandM.GetComponent<VSAI_Hand>().GetFinger();
        int lFingerAI = leftHandAI.GetComponent<VSAI_Hand>().GetFinger();
        int rFingerAI = rightHandAI.GetComponent<VSAI_Hand>().GetFinger();
        if (Random.Range(1,3) == 1 )
        {
            turnFlag = true;
        }
        else
        {
            turnFlag = false;
        }
        // PhotonNetwork.ConnectUsingSettings();
        // restTime = 120;
        // CreateSeparateButton();
        sepPos1 = new Vector2(-150, -530);
        sepPos2 =new Vector2(150, -530);
        btnRotation = Quaternion.identity;
        CreateSeparateButton();
        VSAISE = gameObject.GetComponent<AudioSource>();
        VSAISE.clip = battleBGM;
        VSAISE.Play();

        // Debug.developerConsoleVisible = false;
        // Debug.unityLogger.logEnabled = false;
        // CreateSeparateButton();
        /*
        if (turnFlag)
        {
            turnText.text = "あなたの番です。";
        }else if (!turnFlag)
        {
            turnText.text = "相手の番です。";
        }
        leftHandM.GetComponent<VSAI_Drag>().SetTurnFlag(turnFlag); 
        rightHandM.GetComponent<VSAI_Drag>().SetTurnFlag(turnFlag);
        leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
        leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        */
        //leftHandM.GetComponent<Drag>().SetTurnFlag(turnFlag); 
        //rightHandM.GetComponent<Drag>().SetTurnFlag(turnFlag);
        //gameStartFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // if (PhotonNetwork.PlayerList.Length != 2){return; }
        if (!gameStartFlag)
        {
            // text.text = "始まるよ！";
            StartGame();
            timeText.text = "残り：120秒";
        }
        // CreateSeparateButton();
        */
        if (turnFlag)
        {
            /*
            restTime -= Time.deltaTime;
            if (restTime < 0)
            {
                // photonView.RPC(nameof(RPCResult),RpcTarget.AllViaServer);
                Result();
                return;
            }
            */
            Rect safeArea = Screen.safeArea;
            // timeText.text = "残り：" + Mathf.RoundToInt(restTime).ToString() + "秒";
        }
        
    }
/*
    public override void OnConnectedToMaster()
    {
        switch (gameMode)
        {
            case "random":
                JoinRandomRoom(gameMode);
                break;
            case "friendCreate":
                break;
            case "friendJoin":
                break;
        }
    }

    public void JoinRandomRoom(string gameMode)
    {
        Hashtable expectedCustomRoomProperties = new Hashtable { { GAME_MODE_PROP_KEY, gameMode } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers);
        // PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Join Random Failed with error code {0} and error message {1}", returnCode, message);
        // here usually you create a new room
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomPropertiesForLobby = new string[] {GAME_MODE_PROP_KEY};
        roomOptions.CustomRoomProperties = new Hashtable { { GAME_MODE_PROP_KEY, gameMode } };
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // canvas = PhotonNetwork.Instantiate("Canvas", new Vector2(0f, 0f), Quaternion.identity);
            // leftHand = PhotonNetwork.Instantiate("LeftHand", new Vector2(-200f,-270f), Quaternion.identity);
            // rightHand = PhotonNetwork.Instantiate("RightHand", new Vector2(200f,-270f), Quaternion.identity);
            Instantiate(Board, boardPos1, Quaternion.identity);
            Instantiate(desk, deskPos1, Quaternion.identity);
            text = Instantiate(text, textPos1, Quaternion.identity);
            text.transform.SetParent(canvas.transform, false);
            // leftHand.transform.SetParent(canvas.transform, false);
            // rightHand.transform.SetParent(canvas.transform, false);
            turnFlag = true;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
            canvas.transform.rotation = Quaternion.Euler(0,0,180);
            // canvas = PhotonNetwork.Instantiate("Canvas", new Vector2(0f, 0f), Quaternion.identity);
            leftHandM = PhotonNetwork.Instantiate("LeftHand", new Vector2(200f,220f), Quaternion.Euler(0,0,180));
            rightHandM = PhotonNetwork.Instantiate("RightHand", new Vector2(-200f,220f), Quaternion.Euler(0,0,180));
            Instantiate(Board, boardPos2, Quaternion.identity);
            Instantiate(desk, deskPos2, Quaternion.Euler(0, 0, 180));
            text = Instantiate(text, textPos2, Quaternion.Euler(0, 0, 180));
            text.transform.SetParent(canvas.transform, false);
            // leftHand.transform.SetParent(canvas.transform, false);
            // rightHand.transform.SetParent(canvas.transform, false);
            camera.transform.rotation = Quaternion.Euler(0,0,180);
            turnFlag = false;
        }
    }
    */
/*
    public void StartGame()
    {
        // photonView = gameObject.GetComponent<PhotonView>();
        //if (PhotonNetwork.IsMasterClient)
        //{
            sepPos1 = new Vector2(-150, -530);
            sepPos2 =new Vector2(150, -530);
            btnRotation = Quaternion.identity;
            // turnFlag = true;
            // CreateSeparateButton();
            // timeText = Instantiate(text, timeTextPos1, Quaternion.identity);
            // timeText.transform.SetParent(canvas.transform, false);
            // text.text = "あなたの番です。";
        /*}
        else if (!PhotonNetwork.IsMasterClient)
        {
            sepPos1 = new Vector2(150, 530);
            sepPos2 =new Vector2(-150, 530);
            btnRotation = Quaternion.Euler(0, 0, 180);
            // turnFlag = false;
            timeText = Instantiate(text, timeTextPos2, Quaternion.Euler(0, 0, 180));
            timeText.transform.SetParent(canvas.transform, false);
            // text.text = "相手の番です。";
        }
        */
            /*
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject leftHandE = GameObject.Find("LeftHand");
                if (leftHandE == null)
                {
                    Debug.Log("null");
                }
                rightHand = GameObject.Find("RightHand");
                leftHandE.transform.SetParent(canvas.transform, false);
                rightHand.transform.SetParent(canvas.transform, false);
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                leftHand.transform.SetParent(canvas.transform, false);
                rightHand.transform.SetParent(canvas.transform, false);
            }
            
        CreateSeparateButton();
        if (turnFlag)
        {
            text.text = "あなたの番です。";
        }else if (!turnFlag)
        {
            text.text = "相手の番です。";
        }
        leftHandM.GetComponent<Drag>().SetTurnFlag(turnFlag); 
        rightHandM.GetComponent<Drag>().SetTurnFlag(turnFlag);
        gameStartFlag = true;
        // photonView = gameObject.GetComponent<PhotonView>();
        // CreateSeparateButton();
    }
    */

    public void CreateSeparateButton()
    {
        foreach (GameObject btn in GameObject.FindGameObjectsWithTag("SeparateButton"))
        {
            Destroy(btn);
        }
        sumFinger = 0;
        int rFinger = leftHandM.GetComponent<VSAI_Hand>().GetFinger();
        int lFinger = rightHandM.GetComponent<VSAI_Hand>().GetFinger();
        sumFinger += rFinger;
        sumFinger += lFinger;
        Button btn1 = null;
        Button btn2 = null;
        /*
        if (gameStartFlag)
        {
            photonView.RPC(nameof(RPCChangeTurn),RpcTarget.AllViaServer);
        }
        */
        switch (sumFinger)
        {
            case 0:
                // photonView.RPC(nameof(RPCResult),RpcTarget.AllViaServer);
                break;
            case 2:
                if (lFinger == 1 && rFinger == 1)
                {
                    btn1 = Instantiate(btn20, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT20>().SetSystem(this.gameObject);
                    break;
                }
                if (lFinger == 2 || rFinger == 2)
                {
                    btn1 = Instantiate(btn11, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT11>().SetSystem(this.gameObject);
                    break;
                }
                break;
            case 3:
                if ((lFinger == 1 && rFinger == 2) || (lFinger == 2 && rFinger == 1))
                {
                    btn1 = Instantiate(btn30, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT30>().SetSystem(this.gameObject);
                    break;
                }
                if (lFinger == 3 || rFinger == 3)
                {
                    btn1 = Instantiate(btn21, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT21>().SetSystem(this.gameObject);
                    break;
                }
                break;
            case 4:
                if (lFinger == 2 && rFinger == 2)
                {
                    btn1 = Instantiate(btn31, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT31>().SetSystem(this.gameObject);
                    btn2 = Instantiate(btn40, sepPos2, btnRotation);
                    btn2.transform.SetParent(canvas.transform, false);
                    btn2.GetComponent<SPRT40>().SetSystem(this.gameObject);
                    break;
                }
                if ((lFinger == 1 && rFinger == 3) || (lFinger == 3 && rFinger == 1))
                {
                    btn1 = Instantiate(btn22, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT22>().SetSystem(this.gameObject);
                    btn2 = Instantiate(btn40, sepPos2, btnRotation);
                    btn2.transform.SetParent(canvas.transform, false);
                    btn2.GetComponent<SPRT40>().SetSystem(this.gameObject);
                    break;
                }
                if (lFinger == 4 || rFinger == 4)
                {
                    btn1 = Instantiate(btn31, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT31>().SetSystem(this.gameObject);
                    btn2 = Instantiate(btn22, sepPos2, btnRotation);
                    btn2.transform.SetParent(canvas.transform, false);
                    btn2.GetComponent<SPRT22>().SetSystem(this.gameObject);
                    break;
                }
                break;
            case 5:
                if ((lFinger == 1 && rFinger == 4) || (lFinger == 4 && rFinger == 1))
                {
                    btn1 = Instantiate(btn32, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT32>().SetSystem(this.gameObject);
                    break;
                }
                if ((lFinger == 2 && rFinger == 3) || (lFinger == 3 && rFinger == 2))
                {
                    btn1 = Instantiate(btn41, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT41>().SetSystem(this.gameObject);
                    break;
                }
                break;
            case 6:
                if ((lFinger == 3 && rFinger == 3) || (lFinger == 3 && rFinger == 3))
                {
                    btn1 = Instantiate(btn42, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT42>().SetSystem(this.gameObject);
                    break;
                }
                if ((lFinger == 2 && rFinger == 4) || (lFinger == 4 && rFinger == 2))
                {
                    btn1 = Instantiate(btn33, sepPos1, btnRotation);
                    btn1.transform.SetParent(canvas.transform, false);
                    btn1.GetComponent<SPRT33>().SetSystem(this.gameObject);
                    break;
                }
                break;
        }
        ChangeTurn();
    }

    void Result()
    {
        if (sumFinger == 0 || restTime < 0)
        {
            turnText.text = "あなたの負け!w";
            // PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money",0) + (money/2));
        }
        else
        {
            turnText.text = "あなたの勝ち!!";
            // PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money",0) + money);
            if (AILevel == 1)
            {
                PlayerPrefs.SetString("1","1");
            }

            if (AILevel == 2)
            {
                PlayerPrefs.SetString("2","2");
            }

            if (AILevel == 3)
            {
                PlayerPrefs.SetString("3","3");
            }

            if (AILevel == 4)
            {
                PlayerPrefs.SetString("4","4");
            }

            if (AILevel == 5)
            {
                PlayerPrefs.SetString("5","5");
            }

            if (AILevel == 6)
            {
                PlayerPrefs.SetString("6","6");
            }

            if (PlayerPrefs.GetString("1","none") != "none" && PlayerPrefs.GetString("2","none") != "none"
                && PlayerPrefs.GetString("3","none") != "none" && PlayerPrefs.GetString("4","none") != "none"
                && PlayerPrefs.GetString("5","none") != "none" && PlayerPrefs.GetString("6","none") != "none")
            {
                PlayerPrefs.SetString("番","番");
            }
        }
        VSAISE.Stop();
        VSAISE.clip = finishSE;
        VSAISE.Play();
        

        PlayerPrefs.SetInt("BattleCount", PlayerPrefs.GetInt("BattleCount", 0) + 1);
        if (PlayerPrefs.GetInt("BattleCount", 0) == 10 )
        {
            PlayerPrefs.SetString("はなまる","はなまる");
        }

        if (PlayerPrefs.GetInt("BattleCount", 0) == 100 )
        {
            PlayerPrefs.SetString("100点","100点");
        }

        Invoke("Finish", 3);
    }

    public void Finish()
    {
        if (PlayerPrefs.GetInt("CMCount",0) == 1)
        {
            canvas.GetComponent<AdsManager>().PlayAd();
            PlayerPrefs.SetInt("CMCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("CMCount", 1);
        }
        // PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }

    /*
    [PunRPC]
    void RPCChangeTurn()
    {
        if (turnFlag)
        {
            turnFlag = false;
            text.text = "相手の番です。";
        }else if (!turnFlag)
        {
            turnFlag = true;
            text.text = "あなたの番です。";
        }
        Debug.Log(turnFlag);
        leftHandM.GetComponent<Drag>().SetTurnFlag(turnFlag); 
        rightHandM.GetComponent<Drag>().SetTurnFlag(turnFlag);
    }

    [PunRPC]
    void RPCResult()
    {
        if (sumFinger == 0 || restTime < 0)
        {
            text.text = "あなたの負け!w";
        }
        else
        {
            text.text = "あなたの勝ち!!";
        }
        Invoke("Finish", 3);
    }
    */

    void ChangeTurn()
    {
        lFingerM = leftHandM.GetComponent<VSAI_Hand>().GetFinger();
        rFingerM = rightHandM.GetComponent<VSAI_Hand>().GetFinger();
        lFingerAI = leftHandAI.GetComponent<VSAI_Hand>().GetFinger();
        rFingerAI = rightHandAI.GetComponent<VSAI_Hand>().GetFinger();
        if (lFingerAI + rFingerAI == 0)
        {
            Result();
            return;
        }
        if (lFingerM + rFingerM == 0)
        {
            Result();
            return;
        }
        if (turnFlag)
        {
            turnFlag = false;
            turnText.text = "相手の番です。";
        }else if (!turnFlag)
        {
            turnFlag = true;
            turnText.text = "あなたの番です。";
        }
        leftHandM.GetComponent<VSAI_Drag>().SetTurnFlag(turnFlag); 
        rightHandM.GetComponent<VSAI_Drag>().SetTurnFlag(turnFlag);

        // AIの処理
        if (turnFlag){return ;}
        switch(AILevel)
        {
            case 1:
                Invoke("Level1",0.5f);
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            case 6:
                Invoke("Level6",1f);
                break;
        }

        /*
        if (lFingerM + lFingerAI == 5 && lFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && lFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        */
    }

    public void Level1()
    {
        if (lFingerM + lFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        /*
        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        */
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }

    public void Level2()
    {
        /*
        if (lFingerM + lFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerAI != 0 && lFingerAI != 0 && lFingerM != 0 && rFingerM != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        */

        if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }

    public void Level3()
    {
        if (lFingerM + lFingerAI == 5 && lFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && lFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }

    public void Level4()
    {
        // 勝ちパターン
        // AI MINEの順で大きい数字から書く
        // 3121
        if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2231
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3210
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3030
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 0 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4010
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3221
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }


        // 3310
        else if (lFingerAI == 3 && rFingerAI == 3 && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(4);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }
        
        // 2110
        else if (((lFingerAI == 2 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(0);
            ChangeTurn();
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 1041
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3021
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        
        // 3010
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3110
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2210
        else if ((lFingerAI == 2 && rFingerAI == 2)
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // ---
        /*
        // 負け回避

        // 1を残さないようにするシリーズ
        // 1144 つみ
        // 1131
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 1132
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 1133　つみ
        // 1143
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2133 つみ

        // 2111
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2121
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2131
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2141
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2122
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2123
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2124
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 3111
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        
        // 3121
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3131
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3141
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3142
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3143
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3144
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        //4111
        else if (((lFingerAI == 4 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4121
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4122
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4123
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4124
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // --

        

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        //2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2011
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2021
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //4011
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        //3022
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //2033
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && lFingerM == 3 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        

        // 3044
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3043
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3042
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3041
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3040
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 4020
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 0 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4021
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 1 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4022
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4023
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 3) || (lFingerM == 3 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4024
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2022
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 2 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2042
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4042
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 4 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        */


        
        // ---
        // 基本パターン
        else if (lFingerM + lFingerAI == 5 && lFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && lFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }

    public void Level5()
    {
        // 勝ちパターン
        // AI MINEの順で大きい数字から書く
        /*
        // 3121
        if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2231
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3210
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3030
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 0 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4010
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3221
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }


        // 3310
        else if (lFingerAI == 3 && rFingerAI == 3 && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(4);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }
        
        // 2110
        else if (((lFingerAI == 2 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(0);
            ChangeTurn();
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 1041
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3021
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        
        // 3010
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3110
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2210
        else if ((lFingerAI == 2 && rFingerAI == 2)
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        */
        // ---

        // 負け回避

        // 1を残さないようにするシリーズ
        // 1144 つみ
        // 1131
        if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 1132
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 1133　つみ
        // 1143
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2133 つみ

        // 2111
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2121
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2131
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2141
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2122
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2123
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2124
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 3111
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        
        // 3121
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3131
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3141
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3142
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3143
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3144
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        //4111
        else if (((lFingerAI == 4 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4121
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4122
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4123
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4124
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // --

        

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        //2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2011
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2021
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //4011
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        //3022
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //2033
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && lFingerM == 3 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        

        // 3044
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3043
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3042
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3041
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3040
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 4020
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 0 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4021
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 1 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4022
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4023
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 3) || (lFingerM == 3 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4024
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2022
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 2 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2042
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4042
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 4 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }


        
        // ---
        // 基本パターン
        else if (lFingerM + lFingerAI == 5 && lFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && lFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }

    public void Level6()
    {
        // 勝ちパターン
        // AI MINEの順で大きい数字から書く
        // 3121
        if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2231
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3210
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3030
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 0 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4010
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 0 && lFingerM == 0 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 4 &&  lFingerM == 0 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3221
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }


        // 3310
        else if (lFingerAI == 3 && rFingerAI == 3 && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(4);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }
        
        // 2110
        else if (((lFingerAI == 2 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 2)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(0);
            ChangeTurn();
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 1041
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3021
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        
        // 3010
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3110
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2210
        else if ((lFingerAI == 2 && rFingerAI == 2)
                    && ((lFingerM == 0 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // ---

        // 負け回避

        // 1を残さないようにするシリーズ
        // 1144 つみ
        // 1131
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 1132
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 1133　つみ
        // 1143
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2133 つみ

        // 2111
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2121
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2131
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2141
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2122
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2123
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 2124
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 3111
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        
        // 3121
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3131
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3141
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3142
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 3143
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3144
        else if (lFingerAI == 1 && rFingerAI == 3 && lFingerM == 4 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        //4111
        else if (((lFingerAI == 4 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(3);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4121
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4122
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4123
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 3 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 3 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 && lFingerM == 2 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 &&  lFingerM == 2 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        // 4124
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 1 && rFingerAI == 4 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 4 && rFingerAI == 1 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // --

        

        // 3122
        else if (((lFingerAI == 3 && rFingerAI == 1) || (lFingerAI == 1 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }
        //2010
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2011
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 1 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2021
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2031
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 3 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 1 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 2041
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 4 && rFingerM == 1) || (lFingerM == 1 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //4011
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 1 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        //3022
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        //2033
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && lFingerM == 3 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        

        // 3044
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 3043
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 3 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 3 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 3)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 3)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3042
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3041
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 1 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 1 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 3 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 1)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 3 &&  lFingerM == 4 && rFingerM == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 3040
        else if (((lFingerAI == 3 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 3)) 
                    && ((lFingerM == 4 && rFingerM == 0) || (lFingerM == 0 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 4020
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 0 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 0)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4021
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 1 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 1)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4022
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && lFingerM == 2 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4023
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 3) || (lFingerM == 3 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 4024
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 2 && rFingerM == 4) || (lFingerM == 4 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }

        // 2022
        else if (((lFingerAI == 2 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 2)) 
                    && ((lFingerM == 2 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 2)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(1);
            ChangeTurn();
        }

        // 2042
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 4 && rFingerM == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 4 && rFingerM == 2)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 2 && rFingerAI == 0 && lFingerM == 2 && rFingerM == 4)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerAI == 0 && rFingerAI == 2 &&  lFingerM == 2 && rFingerM == 4)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        // 4042
        else if (((lFingerAI == 4 && rFingerAI == 0) || (lFingerAI == 0 && rFingerAI == 4)) 
                    && ((lFingerM == 4 && rFingerM == 2) || (lFingerM == 2 && rFingerM == 4)))
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            rightHandAI.GetComponent<VSAI_Hand>().SetFinger(2);
            ChangeTurn();
        }


        
        // ---
        // 基本パターン
        else if (lFingerM + lFingerAI == 5 && lFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (lFingerM + rFingerAI == 5 && lFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (rFingerM + lFingerAI == 5 && rFingerM != 0 && lFingerAI != 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if (rFingerM + rFingerAI == 5 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }

        else if (lFingerM == 1 && lFingerAI == 2 && rFingerM == 1 && rFingerAI == 1)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if (lFingerM == 1 && lFingerAI == 1 && rFingerM == 1 && rFingerAI == 2)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 3:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 4:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI != 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
                    break;
            }
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
                case 2:
                    leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
                    leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
                    break;
            }
        }
        else if(lFingerM == 0 && lFingerAI == 0 && rFingerM != 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag);
        }
        else if(lFingerM == 0 && lFingerAI != 0 && rFingerM != 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(rightHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI != 0 && rFingerM == 0 && rFingerAI == 0)
        {
            leftHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            leftHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
        else if(lFingerM != 0 && lFingerAI == 0 && rFingerM == 0 && rFingerAI != 0)
        {
            rightHandAI.GetComponent<VSAI_Hand>().SetTarget(leftHandM);
            rightHandAI.GetComponent<VSAI_Hand>().SetTurnFlag(turnFlag); 
        }
    }



    public void Separate20()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate11()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(1);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate21()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate30()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(3);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate31()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(3);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate22()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate40()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(4);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate41()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(4);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate32()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(3);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate42()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(4);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate33()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<VSAI_Hand>().SetFinger(3);
        rightHandM.GetComponent<VSAI_Hand>().SetFinger(3);
        CreateSeparateButton();
    }

    public bool GetTurnFlag()
    {
        return turnFlag;
    }
}
