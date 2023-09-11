using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem
{
    GameState gameState;
    GameEvent gameEvent;

    public PlayerSystem(GameState _gameState, GameEvent _gameEvent)
    {
        gameState = _gameState;
        gameEvent = _gameEvent;
    }

    public void OnUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float hor = Input.GetAxis("Horizontal")/30;
        Vector3 pos = gameState.player.transform.position;
        Vector3 newPos = new Vector3(pos.x+hor, pos.y, pos.z);
        if (newPos.x > 3.7) newPos.x = 3.7f;
        else if (newPos.x < -3.7f) newPos.x = -3.7f;
        else gameState.player.transform.position = newPos;
    }
}
