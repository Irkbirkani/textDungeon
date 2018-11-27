using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

    public UpdatePlayerStats playerStats;
    public UpdateRoomStats roomStats;
    public ProcessInput input;

    public void SetPlayer(Entity player)
    {
        playerStats.player = player;
        roomStats.player = player;
        input.player = player;
    }
}
