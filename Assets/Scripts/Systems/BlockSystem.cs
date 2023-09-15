using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem
{
    GameState gameState;
    GameEvent gameEvent;
    BlockPool blockPool;
    float time = 0;
    const float interval = 8f;
    const float height = 1.5f;

    public BlockSystem(GameState _gameState, GameEvent _gameEvent, BlockPool _blockPool)
    {
        gameState = _gameState;
        gameEvent = _gameEvent;
        gameEvent.ballHitBlock += DamageBlock;
        blockPool = _blockPool;

        InitGeneBlocks();
    }

    public void OnUpdate()
    {
        if (gameState.gameStatus == GameStatus.IsPlaying)
        {
            CountTimer();
            if (time > interval)
            {
                MoveBlocks();
                GeneBlocks();
                time = 0;
            }
        }
    }

    void InitGeneBlocks()
    {
        for (int i=0 ; i<3 ; i++)
        {
            GameObject upperBlock = blockPool.OnSpawnBlock(gameState.blockPrefab, i, 0);
            gameState.blocks.Add(upperBlock);
            GameObject middleBlock = blockPool.OnSpawnBlock(gameState.blockPrefab, i, 1);
            gameState.blocks.Add(middleBlock);
            GameObject lowerBlock = blockPool.OnSpawnBlock(gameState.blockPrefab, i, 2);
            gameState.blocks.Add(lowerBlock);
        }
    }

    void CountTimer()
    {
        time += Time.deltaTime;
    }

    void MoveBlocks()
    {
        foreach (GameObject block in gameState.blocks)
        {
            Vector3 pos = block.transform.position;
            Vector3 newPos = new Vector3(pos.x, pos.y - height, pos.z);
            block.transform.position = newPos;
        }
    }

    void GeneBlocks()
    {
        for (int i=0 ; i<3 ; i++)
        {
            GameObject block = blockPool.OnSpawnBlock(gameState.blockPrefab, i);
            gameState.blocks.Add(block);
        }
    }

    void DamageBlock(GameObject block)
    {
        BlockComponent blockComp = block.GetComponent<BlockComponent>();
        blockComp.hp--;
        if (blockComp.hp <= 0)
        {
            blockComp.hp = gameState.blockHp;
            gameEvent.onRemoveGameObject?.Invoke(block);
            gameState.blocks.Remove(block);
        }
        block.GetComponent<BlockComponent>().hp = blockComp.hp;
    }
}
