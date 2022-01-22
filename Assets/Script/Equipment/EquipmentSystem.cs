using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject dropdown;
    [SerializeField]
    public AudioClip finishSE;
    [SerializeField]
    public AudioClip actionSE;
    [SerializeField]
    public AudioClip bomberSE;
    [SerializeField]
    public AudioClip battleBGM;
    public AudioSource equipmentSystemSE;

    // Start is called before the first frame update
    void Start()
    {
        equipmentSystemSE = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetString("装備", dropdown.GetComponent<Dropdown>().options[dropdown.GetComponent<Dropdown>().value].text);
    }

    public void Menu()
    {
        equipmentSystemSE.clip = actionSE;
        equipmentSystemSE.Play();
        SceneManager.LoadScene("Menu");
    }
}
