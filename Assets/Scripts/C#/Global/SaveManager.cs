using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Managing Gamesave
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public AutoSave currentAutoSave;
    public bool isNewGame = true;

    public delegate void LoadSave();
    public event LoadSave OnLoadSave;


    BinaryFormatter formatter;
    string autoPath;
    SaveData data;


    private void Awake()
    {
        InitValues();
    }


    /// <summary>
    /// Load the saved Game
    /// </summary>
    public void LoadSavedGame()
    {
        if (SaveExists())
            SetDataValues();
    }

    public void LoadSaveInScene()
    {
        if(!isNewGame && SaveExists())
        {
            if(data.currentScene == SceneManager.GetActiveScene().buildIndex)
            {
                OnLoadSave();
            }
        }
    }

    #region Save Methods

    /// <summary>
    /// Save Interaction with Object
    /// </summary>
    /// <param name="interactIndex">"0" is default value</param>
    public void Save(int interactIndex)
    {
        if (interactIndex != 0)
        {
            data.AddInteract(interactIndex);
            SaveGame();
        }
    }

    /// <summary>
    /// Save result of interaction
    /// </summary>
    /// <param name="resultIndex">"0" is default value</param>
    /// <param name="resultValue">the result</param>
    public void Save(int index, bool resultValue)
    {
        if (index != 0)
        {
            data.AddInteract(index);
            data.AddResult(index, resultValue);
            SaveGame();
        }
    }

    /// <summary>
    /// Save current position of player
    /// </summary>
    public void Save(Vector3 pos, string animationState, int footstepIndex)
    {
        data.ChangePosition(pos);
        data.animationStateName = animationState;
        data.footstepIndex = footstepIndex;
        SaveGame();
    }

    #endregion

    #region Get Methods

    /// <summary>
    /// Check if player interacted with this object
    /// </summary>
    /// <param name="index">"0" is default value</param>
    /// <returns>Returns true if player interacted with object</returns>
    public bool HasInteracted(int index)
    {
        if (index != 0)
            return data.GetInteraction(index);
            
        return false;
    }

    /// <summary>
    /// Check result of player actions
    /// </summary>
    /// <param name="index">index of resultObject</param>
    /// <returns>Returns "null" if player didn't interacted with it</returns>
    public bool? HasResult(int index)
    {
        if (index != 0)
            return data.GetResult(index);

        return null;
    }

    /// <summary>
    /// Get the saved Playerposition
    /// </summary>
    /// <returns>Returns the Playerposition</returns>
    public Vector3 GetPlayerPosition()
    {
        return new Vector3(data.position[0], data.position[1], data.position[2]);
    }

    /// <summary>
    /// Get Animationstate name
    /// </summary>
    /// <returns>Animationstate as string</returns>
    public string GetAnimationState()
    {
        return data.animationStateName;
    }

    public int GetFootstepIndex()
    {
        return data.footstepIndex;
    }

    #endregion

    /// <summary>
    /// Create a new Game
    /// </summary>
    public void NewGame()
    {
        if (SaveExists())
        {
            File.Delete(autoPath);
            data = new SaveData();
            isNewGame = true;
        }
    }

    /// <summary>
    /// Check for SaveFile
    /// </summary>
    /// <returns></returns>
    public bool SaveExists()
    {
        return File.Exists(autoPath);
    }

    void SaveGame()
    {
        data.currentScene = SceneManager.GetActiveScene().buildIndex;

        using (FileStream stream = File.Create(autoPath))
        {
            formatter.Serialize(stream, data);
        }
    }

    void InitValues()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;

        formatter = new BinaryFormatter();
        autoPath = Path.Combine(Application.streamingAssetsPath, "auto.dream");
        data = LoadData();

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Loads the saved Player into his Scene
    /// </summary>
    void SetDataValues()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)data.currentScene)
        {
            SceneManager.LoadScene((int)data.currentScene);
        }
    }

    SaveData LoadData()
    {
        if (SaveExists())
        {
            using (FileStream stream = File.OpenRead(autoPath))
            {
                return formatter.Deserialize(stream) as SaveData;
            }
        }
        else
        {
            return new SaveData();
        }
    }
}
