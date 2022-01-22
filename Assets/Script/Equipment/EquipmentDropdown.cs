using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDropdown : MonoBehaviour
{
    //Create a List of new Dropdown options
    List<string> DropOptions;
    //This is the Dropdown
    Dropdown Dropdown;
    public string a;

    void Start()
    {
        
        DropOptions = new List<string>();
        DropOptions.Add("装備なし");
        if(PlayerPrefs.GetString("1", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("1", "none"));
        }
        if(PlayerPrefs.GetString("2", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("2", "none"));
        }
        if(PlayerPrefs.GetString("3", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("3", "none"));
        }
        if(PlayerPrefs.GetString("4", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("4", "none"));
        }
        if(PlayerPrefs.GetString("5", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("5", "none"));
        }
        if(PlayerPrefs.GetString("6", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("6", "none"));
        }
        if(PlayerPrefs.GetString("はなまる", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("はなまる", "none"));
        }
        if(PlayerPrefs.GetString("番", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("番", "none"));
        }
        if(PlayerPrefs.GetString("100点", "none") != "none")
        {
            DropOptions.Add(PlayerPrefs.GetString("100点", "none"));
        }
        
        //Fetch the Dropdown GameObject the script is attached to
        Dropdown = GetComponent<Dropdown>();
        //Clear the old options of the Dropdown menu
        Dropdown.ClearOptions();
        //Add the options created in the List above
        Dropdown.AddOptions(DropOptions);
    }
}
