using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    GameEvent gameEvent;

    BallSystem ballSystem;
    
    void Start()
    {
        ballSystem = new BallSystem(gameState, gameEvent);
    }

    void Update()
    {
        
    }
}
