using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class RolePhysics : MonoBehaviour
{

    

    [Header("当前跑动速度"), SerializeField] [Header("—————————— 跑动 ——————————")]public float runNowSpeed = 0;

    [Header("跑动极限速度")] public float runTargetSpeed = 3.0f;
    [Header("跑动渐进百分比"), Range(0, 1)] public float runRaiseSpeed = 0.3f;

    
    [Header("是否在地面上")] [Header("—————————— 跳跃 ——————————")] public bool isGround = false;
    [Header("跳跃速度"), Range(0, 20)] public float jumpSpeed = 3.0f;
    [Header("是否正在持续跳跃中")] public bool jumpContinue = false;
    [Header("持续跳跃时长")] public float jumpContinueTime;
    [Header("持续跳跃增加的速度"),Range(0,1.5f)]public float jumpContinueDeltaSpeed;

    

    [Header("冲刺速度")] [Header("—————————— 冲刺 ——————————")] public float sprintSpeed;
    [Header("冲刺时长")] public float sprintTime;
    [Header("冲刺减速值"),Range(0,4)] public float sprintDownSpeed;

    private Rigidbody2D rigidBody;
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void RunLeft()
    {
        if(Mathf.Abs( rigidBody.velocity.x) - Mathf.Abs(runTargetSpeed) > 0)
        {
            return;
        }
        runNowSpeed = (runTargetSpeed + runNowSpeed) > 0.01 ? Mathf.Lerp(runNowSpeed, -runTargetSpeed, runRaiseSpeed) : runNowSpeed;
        rigidBody.velocity = new Vector2(runNowSpeed, rigidBody.velocity.y);
        TurnLeft();
    }
    public void RunRight()
    {
        if (Mathf.Abs(rigidBody.velocity.x) - Mathf.Abs(runTargetSpeed) > 0)
        {
            return;
        }
        runNowSpeed = (runTargetSpeed - runNowSpeed) > 0.01 ? Mathf.Lerp(runNowSpeed, runTargetSpeed, runRaiseSpeed) : runNowSpeed;
        rigidBody.velocity = new Vector2(runNowSpeed, rigidBody.velocity.y);
        TurnRight();
    }

    public void Jump()
    {
        if (isGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            isGround = false;
            StartCoroutine(JumpContinueTimer());
        }
    }

    public void JumpContinue()
    {
        if (jumpContinue && !isGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y + jumpContinueDeltaSpeed/(jumpSpeed / rigidBody.velocity.y));
        }
    }

    IEnumerator JumpContinueTimer()
    {
        jumpContinue = true;
        yield return new WaitForSeconds(jumpContinueTime);
        jumpContinue = false;
    }

    public void Sprint()
    {
        int dic = transform.rotation.y==0 ? 1 : -1;
        rigidBody.velocity = new Vector2(dic*sprintSpeed, rigidBody.velocity.y);
        StartCoroutine(sprintContinueTimer(dic));
    }

    IEnumerator sprintContinueTimer(int dic)
    {
        yield return new WaitForSeconds(sprintTime);
        rigidBody.velocity = new Vector2(dic * sprintDownSpeed, rigidBody.velocity.y);
    }

    public void TurnLeft()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
    }
    public void TurnRight()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y == 1 || Mathf.Abs(collision.contacts[0].normal.x) == 1)
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
