using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <author>Christian</author>
/// <summary>
/// Set Optionmenu values and react on Optionmenu events
/// </summary>
public class OptionMenu : MonoBehaviour
{
#pragma warning disable CS0649

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider[] volumeSlider;
    [SerializeField] List<string> graphicList;

    Resolution[] resolutions;
#pragma warning restore CS0649

    #region Start Methods
    private void Start()
    {
        GetResolution();
        GetGraphic();
        GetFullscreen();

        InstantiateVolume();
    }

    /// <summary>
    /// Put aviable Screen Resolutions into the dropdown
    /// </summary>
    private void GetResolution()
    {
        List<string> options = new List<string>();
        int currentResIndex = 0;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Iterate through every resolution and set resolutionDropdown
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].ToString().ToLower());

            // Set the resolutionDropdownindex to the current Resolution
            if (resolutions[i].ToString().Equals(Screen.currentResolution.ToString()))
            {
                currentResIndex = i;
            }
        }

        // Add List to Dropdown
        resolutionDropdown.AddOptions(options);

        // Get Saved Data HERE

        resolutionDropdown.value = currentResIndex;

        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener((int value) => SetResolution(value));
    }

    /// <summary>
    /// Put aviable Qualitylevel into Dropdown
    /// </summary>
    private void GetGraphic()
    {
        graphicsDropdown.ClearOptions();
        graphicsDropdown.AddOptions(graphicList);

        // Get Saved Data HERE

        graphicsDropdown.value = QualitySettings.GetQualityLevel();

        graphicsDropdown.RefreshShownValue();
        graphicsDropdown.onValueChanged.AddListener((int value) => SetQuality(value));
    }

    void GetFullscreen()
    {
        // Get Saved Data HERE
       
        fullscreenToggle.isOn = true;
        
    }

    private void InstantiateVolume()
    {
        for (int i = 0; i < AudioManager.Instance.volumeStrings.Length; i++)
        {
            if(volumeSlider[i])
            {
                switch(i)
                {
                    case 0:
                        volumeSlider[i].onValueChanged.AddListener((float volume) => SetMasterVolume(volume));
                        break;
                    case 1:
                        volumeSlider[i].onValueChanged.AddListener((float volume) => SetMusicVolume(volume));
                        break;
                    case 2:
                        volumeSlider[i].onValueChanged.AddListener((float volume) => SetFXVolume(volume));
                        break;
                    case 3:
                        volumeSlider[i].onValueChanged.AddListener((float volume) => SetCharacterVolume(volume));
                        break;
                }
            }

            GetVolume(i);
        }
    }

    /// <summary>
    /// Put current Volume into slider
    /// </summary>
    private void GetVolume(int index)
    {
        float volume = AudioManager.Instance.GetCurrentVolume(index);
        volumeSlider[index].value = volume;
        // Save Data HERE
    }

    #endregion

    #region SetEvent Methods

    /// <summary>
    /// Set the Volume on changed value
    /// </summary>
    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance.SetCurrentVolume(0, volume);
    }


    /// <summary>
    /// Set the Volume on changed value
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetCurrentVolume(1, volume);
    }

    /// <summary>
    /// Set the Volume on changed value
    /// </summary>
    public void SetFXVolume(float volume)
    {
        AudioManager.Instance.SetCurrentVolume(2, volume);
    }

    /// <summary>
    /// Set the Volume on changed value
    /// </summary>
    public void SetCharacterVolume(float volume)
    {
        AudioManager.Instance.SetCurrentVolume(3, volume);
    }

    /// <summary>
    /// Set graphicQuality on changed value
    /// </summary>
    public void SetQuality(int qualityIndex)
    {
        // Save Data HERE
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    /// <summary>
    /// Set Fullscreen true/false
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        // Save Data HERE

        Screen.fullScreen = isFullscreen;
    }

    /// <summary>
    /// Set resolution on changed value
    /// </summary>
    public void SetResolution(int resolutionIndex)
    {
        // Save Data HERE


        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, fullscreenToggle.isOn);
    }
    #endregion
}
