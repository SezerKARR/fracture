using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private BlockCreater blockCreater;
    private LaunchPreview launchPreview;
    private int remainBall;
    
    private List<Ball> balls = new List<Ball>();
    [SerializeField]
    private Ball ballPrefab;
    private int ballsReady;
    
    private void Awake()
    {
        blockCreater = FindObjectOfType<BlockCreater>();
        launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }
    public void ReturnBall()
    {
        ballsReady++;
        remainBall--;
        

        if (ballsReady == balls.Count)
        {
            blockCreater.SpawnRowOfBlocks();
            CreateBall();
        }
        Debug.Log(remainBall);
        Debug.Log("ball"+ballsReady);
    }

    private void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        balls.Add(ball);
        ballsReady++;
    }
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;
        if (remainBall == 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                StartDrag(worldPosition);
            }
            else if (Input.GetMouseButton(0))
            {
                ContinueDrag(worldPosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {

                EndDrag();
                remainBall = ballsReady;
            }
        }
        
    }
    private void EndDrag()
    {
        StartCoroutine(LaunchBalls());
        launchPreview.LineRendererEnable(false);
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = endDragPosition - startDragPosition;
        direction.Normalize();
        foreach(var ball in balls)
        {
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);
            yield return new WaitForSeconds(0.1f);
        }
        ballsReady = 0;
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;
        Vector3 direction = endDragPosition - startDragPosition;
        launchPreview.SetEndPoint(transform.position-direction);
        launchPreview.LineRendererEnable(true);
    }
    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }
}
