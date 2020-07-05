using UnityEditor;
using UnityEngine;

/// <author>Christian</author>
/// <summary>
/// Outline all Renderers of Gameobject
/// </summary>
public class OutlineObject : MonoBehaviour
{
    public bool hasInteracted = false;


    [SerializeField]
    Color outlineColor = Color.white;

    [SerializeField]
    float outlineSpacing = 0.01f;

    Renderer[] rendererCollection;

    MaterialPropertyBlock propBlock;

    Camera cam;

    RaycastHit hit;

    float maxDistance = 10;

    #region Unity Methods

    void Start()
    {
        InitValues();
    }

    protected virtual void OnMouseEnter()
    {
        // Take this to Interactable
        SwitchOutlined(true);
    }

    protected virtual void OnMouseExit()
    {
        // Take this to Interactable
        SwitchOutlined(false);
    }

    private void OnDisable()
    {
        SwitchOutlined(false);
    }

    #endregion

    /// <summary>
    /// Initialise Values
    /// </summary>
    protected virtual void InitValues()
    {
        cam = Camera.main;
        rendererCollection = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }


    /// <summary>
    /// Switch to Renderer to Outline
    /// </summary>
    void SwitchOutlined(bool outlined)
    {
        if(rendererCollection != null)
        if(!hasInteracted)
        {
            for (int i = 0; i < rendererCollection.Length; i++)
            {
                // Get the current PropertyBlock
                rendererCollection[i].GetPropertyBlock(propBlock);

                // Change Values
                propBlock.SetFloat("_currentTime", Time.time);
                propBlock.SetFloat("_outline_thickness", outlineSpacing);
                propBlock.SetColor("_outline_color", outlineColor);
                propBlock.SetFloat("_enable", (outlined ? 1.0f : 0.0f));

                // Write it back in
                rendererCollection[i].SetPropertyBlock(propBlock);
            }
        }
        else
        {
            for (int i = 0; i < rendererCollection.Length; i++)
            {
                // Get the current PropertyBlock
                rendererCollection[i].GetPropertyBlock(propBlock);

                // Change Values
                propBlock.SetFloat("_enable", (0.0f));

                // Write it back in
                rendererCollection[i].SetPropertyBlock(propBlock);
            }
        }
    }
}
