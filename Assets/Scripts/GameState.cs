using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public GameObject player;
    public GameObject ballPrefab;
    public GameObject blockPrefab;

    public List<GameObject> balls;
    public List<GameObject> blocks;

    [System.NonSerialized]
    public bool isMoving;
}
