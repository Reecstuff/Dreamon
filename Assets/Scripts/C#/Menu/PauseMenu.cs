using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool pauseMenuOpen = false;

    [SerializeField]
    Canvas pauseMenu;


    void Start()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (pauseMenuOpen)
            {
                CloseMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    public void CloseMenu()
    {
        if (SceneManager.sceneCount > 1)
            return;

        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance?.PitchManual(1);


        pauseMenuOpen = false;
    }

    void ShowMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        pauseMenuOpen = true;

        AudioManager.Instance?.PitchManual(0.7f);

    }

    private void OnDisable()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        AudioManager.Instance?.PitchManual(1);
    }
}
