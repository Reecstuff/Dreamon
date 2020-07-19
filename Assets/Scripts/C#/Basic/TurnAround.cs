using UnityEngine;

/// <summary>
/// Just rotate this object around y axis
/// </summary>
public class TurnAround : MonoBehaviour
{
    [Range(0,20)]
    [SerializeField]
    float speed = 10;

    private void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
