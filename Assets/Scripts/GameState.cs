using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    Ready,
    IsPlaying,
    GameOver,
    GameClear,
}

[System.Serializable]
public class GameState
{
    public GameObject player;
    public GameObject ballPrefab;
    public GameObject blockPrefab;
    public int blockHp;

    public List<GameObject> balls;
    public List<GameObject> blocks;

    [System.NonSerialized]
    public GameStatus gameStatus;
}
