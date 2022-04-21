using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturn : MonoBehaviour
{
    private BallLauncher BallLauncher ;
    private Ball Ball;
    private void Awake()
    {
        BallLauncher = FindObjectOfType<BallLauncher>();
        Ball= FindObjectOfType<Ball>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallLauncher.ReturnBall();
        collision.rigidbody.gravityScale = 0;
        collision.collider.gameObject.SetActive(false);
    }
}
