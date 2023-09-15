using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem
{
    GameState gameState;
    GameEvent gameEvent;

    GameObject ball;
    BallComponent ballComp;
    Vector3 ballPos;
    public LayerMask mask = -1;

    public BallSystem(GameState _gameState, GameEvent _gameEvent)
    {
        gameState = _gameState;
        gameEvent = _gameEvent;

        ball = gameState.ballPrefab;
        ballComp = ball.GetComponent<BallComponent>();
        ballPos = ball.transform.position;
    }

    public void OnUpdate()
    {
        if (gameState.gameStatus == GameStatus.Ready) AddDirec();
        else if (gameState.gameStatus == GameStatus.IsPlaying)
        {
            MoveBall();
            HitObject();
        }
        else if (gameState.gameStatus == GameStatus.GameClear) Debug.Log("####CLEAR");
    }

    void AddDirec()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 ballDirec = Vector3.Scale((mousePos - ballPos), new Vector3(1,1,0)).normalized;
            ballComp.direction = ballDirec;
            ballPos = ball.transform.position;
            ball.transform.position =new Vector3(
                ballPos.x + ballComp.direction.x*Time.deltaTime*ballComp.speed, ballPos.y + ballComp.direction.y*Time.deltaTime*ballComp.speed, ballPos.z
            );
            gameState.gameStatus = GameStatus.IsPlaying;
        }
    }

    void MoveBall()
    {
        ballPos = ball.transform.position;
        ball.transform.position = new Vector3(
            ballPos.x + ballComp.direction.x*Time.deltaTime*ballComp.speed, ballPos.y + ballComp.direction.y*Time.deltaTime*ballComp.speed, ballPos.z
        );
    }

    void HitObject()
    {
        Ray ray = new Ray(ballPos, ballComp.direction);

        RaycastHit hit;

        Debug.DrawLine(ray.origin, ray.origin+ray.direction * 100, Color.red, 0);

        bool isHit = Physics.SphereCast(ballPos, 0.5f, ballComp.direction, out hit, 0.1f, mask);

        if (isHit)
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.layer != 3 && hitObj.layer != 6 && hitObj.layer != 7 && hitObj.layer != 8) return;
            if (hitObj.layer == 8)
            {
                gameEvent.ballHitBlock?.Invoke(hitObj);
            }

            Vector3 normal = hit.normal;
            Vector3 ref_direc = Vector3.Reflect(ballComp.direction, normal);

            ballComp.direction = ref_direc;
        }
    }
}
