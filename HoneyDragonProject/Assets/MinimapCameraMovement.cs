using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraMovement : MonoBehaviour
{
    public float minimapCameraHeight;
    Transform player;

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (player == null) return;

        Vector3 pos = new Vector3(player.position.x, minimapCameraHeight, player.position.z);
        transform.position = pos;
    }
}
