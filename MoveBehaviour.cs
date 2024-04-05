using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour, I_Moveable
{
    private Rigidbody2D rb2d_;
    [SerializeField] private float moveSpeed_ = 5.0f;
    [SerializeField] private float dashSpeed_ = 20f;
    [SerializeField] private float dashDuration_ = 0.2f;

    // Properties for moveSpeed_
    public float MoveSpeed
    {
        get { return moveSpeed_; }
        set { moveSpeed_ = value; }
    }

    // Properties for dashSpeed_
    public float DashSpeed
    {
        get { return dashSpeed_; }
        set { dashSpeed_ = value; }
    }

    // Properties for dashDuration_
    public float DashDuration
    {
        get { return dashDuration_; }
        set { dashDuration_ = value; }
    }

    private void Awake()
    {
        ComponentUtility.AssignRigidbody2D(gameObject, out rb2d_);
    }
    public void MoveLeft()
    {
        rb2d_.velocity = new Vector2(-MoveSpeed, rb2d_.velocity.y);
    }

    public void MoveRight()
    {
        rb2d_.velocity = new Vector2(MoveSpeed, rb2d_.velocity.y);
    }

    public void StopMove()
    {
        rb2d_.velocity = new Vector2(0, rb2d_.velocity.y);
    }

    public void DashLeft()
    {
        StartCoroutine(Dash(-DashSpeed));
    }

    public void DashRight()
    {
        StartCoroutine(Dash(DashSpeed));
    }

    private IEnumerator Dash(float dashSpeed)
    {
        float startTime = Time.time;
        while (Time.time < startTime + DashDuration)
        {
            rb2d_.velocity = new Vector2(dashSpeed, rb2d_.velocity.y);
            yield return null;
        }
        StopMove(); // Optional: Reset velocity after dashing
    }
}
