using UnityEngine;

/// <author>Christian</author>
/// <summary>
/// Outline all Renderers of Gameobject
/// </summary>
public class OutlineObject : MonoBehaviour
{

    [SerializeField]
    Color outlineColor = Color.white;

    Renderer[] rendererCollecton;

    MaterialPropertyBlock propBlock;

    #region Unity Methods

    void Start()
    {
        InitValues();
    }

    private void OnMouseEnter()
    {
        SwitchOutlined(true);
    }

    private void OnMouseExit()
    {
        SwitchOutlined(false);
    }

    #endregion
    
    /// <summary>
    /// Initialise Values
    /// </summary>
    void InitValues()
    {
        rendererCollecton = GetComponentsInChildren<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }


    /// <summary>
    /// Switch to Renderer to Outline
    /// </summary>
    void SwitchOutlined(bool outlined)
    {
        for (int i = 0; i < rendererCollecton.Length; i++)
        {
            // Get the current PropertyBlock
            rendererCollecton[i].GetPropertyBlock(propBlock);

            // Change Values
            propBlock.SetFloat("_currentTime", Time.time);
            propBlock.SetColor("_outline_color", outlineColor);
            propBlock.SetFloat("_enable", (outlined ? 1.0f : 0.0f));

            // Write it back in
            rendererCollecton[i].SetPropertyBlock(propBlock);
        }
    }
}
