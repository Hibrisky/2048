using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JumpJumpPlayer : MonoBehaviour
{

    Rigidbody2D rigid;

    public float jumpPower = 0f;

    GG_JumpJumpManager jumpManager;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            //Game over
            //게임오버 문구 활성화 및 스코어 증가.
            jumpManager.Show_GameOver();
        }
    }




}
