﻿using System.Collections;
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

    public float drivingTime = 2;


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
        else
        {
            if (!onOffsetReset)
                transform.LookAt(target.position);
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

    public void StartResetCameraToPlayer()
    {
        Invoke(nameof(ResetCameraToPlayer), drivingTime);
    }

    void ResetCameraToPlayer()
    {
        onOffsetReset = false;
        fixedCamera = false;
    }
}