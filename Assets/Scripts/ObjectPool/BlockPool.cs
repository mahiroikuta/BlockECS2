using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class BlockPool
{
    GameState gameState;
    private Dictionary<int, List<GameObject>> pool = new Dictionary<int, List<GameObject>>();

    const float height = 1.5f;
    const float width = 2.5f;

    public BlockPool(GameState _gameState, GameEvent _gameEvent)
    {
        gameState = _gameState;
        _gameEvent.onRemoveGameObject += OnRemoveBlock;
    }

    private void OnRemoveBlock(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public GameObject OnSpawnBlock(GameObject block, int x, int y = 0)
    {
        int hash = block.GetHashCode();
        if (pool.ContainsKey(hash))
        {
            List<GameObject> targetPool = pool[hash];
            int count = targetPool.Count;
            for(int j=0 ; j<count ; j++)
            {
                if (targetPool[j].activeSelf == false)
                {
                    targetPool[j].SetActive(true);
                    return targetPool[j];
                }
            }

            GameObject targetBlock = GameObject.Instantiate(targetPool[0], new Vector3(x*width-2.5f, 8-y*height, -1), Quaternion.identity);
            targetPool.Add(targetBlock);
            targetBlock.SetActive(true);
            return targetBlock;
        }

        GameObject targetBlock2 = GameObject.Instantiate(block, new Vector3(x*width-2.5f, 8-y*height, -1), Quaternion.identity);
        List<GameObject> poolList = new List<GameObject>();
        poolList.Add(targetBlock2);
        pool.Add(hash, poolList);
        targetBlock2.SetActive(true);
        return targetBlock2;
    }
}
