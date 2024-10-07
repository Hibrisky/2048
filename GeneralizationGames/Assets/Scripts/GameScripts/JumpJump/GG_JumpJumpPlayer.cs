using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JumpJumpPlayer : MonoBehaviour
{

    Rigidbody2D rigid;

    public float jumpPower = 0f;
    [SerializeField]
    GG_JumpJumpManager jumpManager;

    private const int upDirection = 1;

    Vector3 PlayerPos;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        PlayerPos = new Vector3(-2.183f,-2.615f,0f);
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);//������ ���ӵ��� ���� �߰� �Ÿ� ����,
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 collisionPos = contact.normal;

            if(collisionPos.y == upDirection)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            }
            else
            {
                //Game over
                //���ӿ��� ���� Ȱ��ȭ �� ���ھ� ����.
                jumpManager.Show_GameOver();
            }
        }
    }
    public void ResetPosition()
    {
        this.transform.localPosition = PlayerPos;
    }
}
