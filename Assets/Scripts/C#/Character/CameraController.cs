using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public float pitch = 2f;

    private float currentZoom = 10f;
    private float currentYaw = 0;

    bool fixedCamera = false;
    bool onOffsetReset = false;
    bool onLookAtLerp = false;

    public float drivingTime = 2;

    public float dampingTime = 15;

    void Update()
    {
        if (!fixedCamera)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Rotates the camera when you hold down the middle mouse button
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * 1000f, Vector3.up);

                offset = camTurnAngle * offset;
            }
        }
    }

    void LateUpdate()
    {
        if (!fixedCamera)
        {
            //The camera follows the player
            transform.position = target.position - offset * currentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);

            transform.RotateAround(target.position, Vector3.up, currentYaw);
        }
        else if(!onLookAtLerp)
        {
            if (!onOffsetReset)
                LookAtTarget(target.position);
            else
                transform.LookAt(target.position + Vector3.up * pitch);
        }
    }

    public void MoveToFixedPosition(Vector3 newPosition, Transform newTarget)
    {
        fixedCamera = true;
        target = newTarget;

        // Move Camera to Position
        transform.DOMove(newPosition, drivingTime);
    }

    public void MoveToOffset(Transform newTarget)
    {
        onOffsetReset = true;
        target = newTarget;
        transform.DOMove(target.position - offset * currentZoom, drivingTime);
    }

    public void LerpLookAt(Transform newLookAt)
    {
        onLookAtLerp = true;
        transform.DOLookAt(newLookAt.position, drivingTime / 2);
        target = newLookAt;
        CancelInvoke(nameof(ResetLookAt));
        Invoke(nameof(ResetLookAt), drivingTime / 2);
    }

    public void StartResetCameraToPlayer()
    {
        Invoke(nameof(ResetCameraToPlayer), drivingTime);
    }

    void ResetLookAt()
    {
        onLookAtLerp = false;
    }

    void ResetCameraToPlayer()
    {
        onOffsetReset = false;
        fixedCamera = false;
    }

    /// <summary>
    /// Control Speed of LookAt
    /// </summary>
    /// <param name="focusPoint">Point to focus on</param>
    void LookAtTarget(Vector3 focusPoint)
    {
        var rotation = Quaternion.LookRotation(focusPoint- transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampingTime);
    }
}