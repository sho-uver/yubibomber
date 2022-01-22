using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Battle : MonoBehaviourPunCallbacks
{
    public const string GAME_MODE_PROP_KEY = "GameMode";
    public const byte expectedMaxPlayers = 2;
    public string gameMode;
    [SerializeField]
    public GameObject camera;
    [SerializeField]
    public GameObject Board;
    public GameObject leftHand;
    public GameObject rightHand;
    // [SerializeField]
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
    private PhotonView photonView;
    public bool turnFlag;
    [SerializeField]
    public Text text;
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
    public float toVSAITime;
    [SerializeField]
    public AudioClip finishSE;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    [SerializeField]
    public AudioClip battleBGM;
    public AudioSource VSAISE;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = Menu.GetGameMode();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        gameStartFlag = false;
        PhotonNetwork.ConnectUsingSettings();
        restTime = 120;
        // CreateSeparateButton();
        toVSAITime = 0;
        VSAISE = gameObject.GetComponent<AudioSource>();
        VSAISE.clip = battleBGM;
        VSAISE.Play();
        Debug.developerConsoleVisible = false;
        Debug.unityLogger.logEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (toVSAITime >= 7){ToVSAI(); }
        if (PhotonNetwork.PlayerList.Length != 2)
        {
            toVSAITime += Time.deltaTime;
            return; 
        }
        if (!gameStartFlag)
        {
            // text.text = "始まるよ！";
            StartGame();
            timeText.text = "残り：120秒";
        }
        // CreateSeparateButton();
        if (turnFlag)
        {
            restTime -= Time.deltaTime;
            if (restTime < 0)
            {
                photonView.RPC(nameof(RPCResult),RpcTarget.AllViaServer);
                return;
            }
            timeText.text = "残り：" + Mathf.RoundToInt(restTime).ToString() + "秒";
        }
        
    }

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

    public void StartGame()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            sepPos1 = new Vector2(-150, -530);
            sepPos2 =new Vector2(150, -530);
            btnRotation = Quaternion.identity;
            // turnFlag = true;
            // CreateSeparateButton();
            timeText = Instantiate(text, timeTextPos1, Quaternion.identity);
            timeText.transform.SetParent(canvas.transform, false);
            // text.text = "あなたの番です。";
            leftHandM.GetComponent<Hand>().MakeSticker();
            rightHandM.GetComponent<Hand>().MakeSticker();
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            sepPos1 = new Vector2(150, 530);
            sepPos2 =new Vector2(-150, 530);
            btnRotation = Quaternion.Euler(0, 0, 180);
            // turnFlag = false;
            timeText = Instantiate(text, timeTextPos2, Quaternion.Euler(0, 0, 180));
            timeText.transform.SetParent(canvas.transform, false);
            leftHandM.GetComponent<Hand>().MakeSticker();
            rightHandM.GetComponent<Hand>().MakeSticker();
            // text.text = "相手の番です。";
        }
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
            */
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
        photonView = gameObject.GetComponent<PhotonView>();
        
        // CreateSeparateButton();
    }

    public void CreateSeparateButton()
    {
        foreach (GameObject btn in GameObject.FindGameObjectsWithTag("SeparateButton"))
        {
            Destroy(btn);
        }
        sumFinger = 0;
        int rFinger = leftHandM.GetComponent<Hand>().GetFinger();
        int lFinger = rightHandM.GetComponent<Hand>().GetFinger();
        sumFinger += rFinger;
        sumFinger += lFinger;
        Button btn1 = null;
        Button btn2 = null;
        if (gameStartFlag)
        {
            photonView.RPC(nameof(RPCChangeTurn),RpcTarget.AllViaServer);
        }
        switch (sumFinger)
        {
            case 0:
                photonView.RPC(nameof(RPCResult),RpcTarget.AllViaServer);
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
    }

    public void Finish()
    {
        PhotonNetwork.Disconnect();
        if (PlayerPrefs.GetInt("CMCount",0) == 1)
        {
            canvas.GetComponent<AdsManager>().PlayAd();
            PlayerPrefs.SetInt("CMCount", 0);
        }
        else
        {
            PlayerPrefs.SetInt("CMCount", 1);
        }
        SceneManager.LoadScene("Menu");
    }

    public void ToVSAI()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("VSAI");
    }

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
    public void Separate20()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(2);
        rightHandM.GetComponent<Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate11()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(1);
        rightHandM.GetComponent<Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate21()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(2);
        rightHandM.GetComponent<Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate30()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(3);
        rightHandM.GetComponent<Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate31()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(3);
        rightHandM.GetComponent<Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate22()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(2);
        rightHandM.GetComponent<Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate40()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(4);
        rightHandM.GetComponent<Hand>().SetFinger(0);
        CreateSeparateButton();
    }

    public void Separate41()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(4);
        rightHandM.GetComponent<Hand>().SetFinger(1);
        CreateSeparateButton();
    }

    public void Separate32()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(3);
        rightHandM.GetComponent<Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate42()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(4);
        rightHandM.GetComponent<Hand>().SetFinger(2);
        CreateSeparateButton();
    }

    public void Separate33()
    {
        if (!turnFlag){return; }
        leftHandM.GetComponent<Hand>().SetFinger(3);
        rightHandM.GetComponent<Hand>().SetFinger(3);
        CreateSeparateButton();
    }
}
