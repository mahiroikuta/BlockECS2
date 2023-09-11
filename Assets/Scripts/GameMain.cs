using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    GameEvent gameEvent;

    BallSystem ballSystem;
    BlockSystem blockSystem;
    PlayerSystem playerSystem;
    
    void Start()
    {
        gameState.gameStatus = GameStatus.Ready;
        ballSystem = new BallSystem(gameState, gameEvent);
        blockSystem = new BlockSystem(gameState, gameEvent);
        playerSystem = new PlayerSystem(gameState, gameEvent);
    }

    void Update()
    {
        ballSystem.OnUpdate();
        blockSystem.OnUpdate();
        playerSystem.OnUpdate();
    }
}
