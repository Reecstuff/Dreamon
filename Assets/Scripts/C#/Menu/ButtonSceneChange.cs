using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Change Scene on Button Click
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonSceneChange : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ChangeScene());
    }

    void ChangeScene()
    {
        if(!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    
}
