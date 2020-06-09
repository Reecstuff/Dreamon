using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float pitch = 2f;

    private float currentZoom = 10f;
    private float currentYaw = 0;

    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Rotates the camera when you hold down the middle mouse button
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * 1000f, Vector3.up);

            offset = camTurnAngle * offset;
        }
    }

    void LateUpdate()
    {
        //The camera follows the player
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}