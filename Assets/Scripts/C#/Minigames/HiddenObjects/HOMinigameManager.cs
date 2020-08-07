using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOMinigameManager : MinigameManager
{
    public bool isEnd = false;

    public override void StartNewMinigame()
    {
        Cursor.visible = false;
        cameraController.MoveToOffset(player.transform);
        cameraController.StartResetCameraToPlayer();
        player.motor.ResumeAgent();

        SetMinigameActive();
    }

    public override void EndMinigame()
    {
        isEnd = true;

        player.motor.ResumeAgent();
        player.motor.FollowTarget(GetComponent<Belphe>());
    }
}
