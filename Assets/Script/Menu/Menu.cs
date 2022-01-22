using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Menu : MonoBehaviourPunCallbacks
{
    public static string gameMode;
    [SerializeField]
    public AudioClip finishSE;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    [SerializeField]
    public AudioClip battleBGM;
    public AudioSource VSAISE;
    public static int AILevel;
    [SerializeField]
    public GameObject dropdown;
    [SerializeField]
    public Text moneyText; 
    [SerializeField]
    public GameObject rule;
    [SerializeField]
    public Text battleCount;
    [SerializeField]
    public Text getSticker;
    [SerializeField]
    public GameObject getStickerPanel;
    public string names;
    public static string sticker1;
    public static string sticker2;
    public static string sticker3;
    public static string sticker4;
    public static string sticker5;
    public static string sticker6;
    public static string hanamaru;
    public static string ban;
    public static string hundredPoints;

    public void RandomMode()
    {
        VSAISE.clip = actionSE;
        VSAISE.Play();
        gameMode = "random";
        AILevel = dropdown.GetComponent<Dropdown>().value + 1;
        SceneManager.LoadScene("Battle");
    }

    public void VSAI()
    {
        VSAISE.clip = actionSE;
        VSAISE.Play();
        gameMode = "VSAI";
        AILevel = dropdown.GetComponent<Dropdown>().value + 1;
        SceneManager.LoadScene("VSAI");
    }

    public void Equipment()
    {
        VSAISE.clip = actionSE;
        VSAISE.Play();
        SceneManager.LoadScene("EquipmentScene");
    }
    public void Start()
    {
        VSAISE = gameObject.GetComponent<AudioSource>();
        moneyText.text = "おこづかい" + PlayerPrefs.GetInt("money",0).ToString() + "円";
        battleCount.text = "バトルした回数：" + PlayerPrefs.GetInt("BattleCount",0).ToString() + "回";
        CheckSticker();
        // Debug.developerConsoleVisible = false;
        // Debug.unityLogger.logEnabled = false;
    }

    public void OpenRule()
    {
        rule.SetActive(true);
    }

    public void CloseRule()
    {
        rule.SetActive(false);
    }

    public void CloseGetSticker()
    {
        getStickerPanel.SetActive(false);
    }

    public static string GetGameMode(){return gameMode; }

    public static int GetAILevel(){return AILevel; }
    public void CheckSticker()
    {
        if (sticker1 != null && sticker1 != PlayerPrefs.GetString("1", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「1」\n";
        }
        if (sticker2 != null && sticker2 != PlayerPrefs.GetString("2", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「2」\n";
        }
        if (sticker3 != null && sticker3 != PlayerPrefs.GetString("3", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「3」\n";
        }
        if (sticker4 != null && sticker4 != PlayerPrefs.GetString("4", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「4」\n";
        }
        if (sticker5 != null && sticker5 != PlayerPrefs.GetString("5", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「5」\n";
        }
        if (sticker6 != null && sticker6 != PlayerPrefs.GetString("6", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「6」\n";
        }
        if (hanamaru != null && hanamaru != PlayerPrefs.GetString("はなまる", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「はなまる」\n";
        }
        if (ban != null && ban != PlayerPrefs.GetString("番", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「番」\n";
        }
        if (hundredPoints != null && hundredPoints != PlayerPrefs.GetString("100点", "none"))
        {
            getStickerPanel.SetActive(true);
            names += "「100点」\n";
        }
        if (names != "")
        {
            getStickerPanel.SetActive(true);
            getSticker.text = names;
        }
        sticker1 = PlayerPrefs.GetString("1", "none");
        sticker2 = PlayerPrefs.GetString("2", "none");
        sticker3 = PlayerPrefs.GetString("3", "none");
        sticker4 = PlayerPrefs.GetString("4", "none");
        sticker5 = PlayerPrefs.GetString("5", "none");
        sticker6 = PlayerPrefs.GetString("6", "none");
        hanamaru = PlayerPrefs.GetString("はなまる", "none");
        ban = PlayerPrefs.GetString("番", "none");
        hundredPoints = PlayerPrefs.GetString("100点", "none");
    }
}
