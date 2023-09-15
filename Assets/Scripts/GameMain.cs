using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    [SerializeField]
    GameEvent gameEvent;
    BlockPool blockPool;

    BallSystem ballSystem;
    BlockSystem blockSystem;
    PlayerSystem playerSystem;
    
    void Start()
    {
        gameState.gameStatus = GameStatus.Ready;
        blockPool = new BlockPool(gameState, gameEvent);
        ballSystem = new BallSystem(gameState, gameEvent);
        blockSystem = new BlockSystem(gameState, gameEvent, blockPool);
        playerSystem = new PlayerSystem(gameState, gameEvent);
    }

    void Update()
    {
        ballSystem.OnUpdate();
        blockSystem.OnUpdate();
        playerSystem.OnUpdate();
    }
}
