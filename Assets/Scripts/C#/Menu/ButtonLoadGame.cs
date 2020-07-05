using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonLoadGame : MonoBehaviour
{
    [SerializeField]
    string loadText = "continue";

    void Start()
    {
        if(SaveManager.instance)
        {
            if(SaveManager.instance.SaveExists())
            {
                GetComponent<Button>().onClick.AddListener(() => SaveManager.instance.LoadSavedGame());
                GetComponentInChildren<TextMeshProUGUI>().text = loadText;
                SaveManager.instance.isNewGame = false;
            }
            else
            {
                SaveManager.instance.isNewGame = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
