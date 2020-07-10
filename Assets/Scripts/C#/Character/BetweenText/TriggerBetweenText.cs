using UnityEngine;


[RequireComponent(typeof(Collider))]
public class TriggerBetweenText : MonoBehaviour
{
    [SerializeField]
    TimedTalk[] timedTalks;

    [SerializeField]
    int saveIndex = 0;

    bool triggered = false;

    void Start()
    {
        InitValues();
    }

    void InitValues()
    {
        GetComponent<Collider>().isTrigger = true;

        if (SaveManager.instance)
            SaveManager.instance.OnLoadSave += LoadState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered)
            if (other.GetComponent<CallBetweenText>())
            {
                other.GetComponent<CallBetweenText>().CallBetween(timedTalks);
                triggered = true;
            }
    }

    void SaveState()
    {
        if (SaveManager.instance)
        {
            if (!SaveManager.instance.HasInteracted(saveIndex))
            {
                SaveManager.instance.Save(saveIndex);
            }
        }
    }


    /// <summary>
	/// Load the state of this Trigger from savegame
	/// </summary>
	void LoadState()
    {
        if (SaveManager.instance.HasInteracted(saveIndex))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (SaveManager.instance)
            SaveManager.instance.OnLoadSave -= LoadState;
    }
}
