using System.Collections;
using UnityEngine;

public class JumpEnemy : MonoBehaviour
{
    protected enum MoveType
    {
        BackAndForth = 0, // 앞뒤로 이동.
        Jump, //제자리 점프.
        All,//위 두가지 전부다.
    }

    [SerializeField]
    MoveType moveType = MoveType.BackAndForth;

    [SerializeField]
    float speed = 0f;
    [SerializeField]
    float jumpPower = 0f;

    Rigidbody2D rigidBody;
    
    protected bool isOut = false;

    float timer = 0f;
    float jumpCooltime = 0f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        jumpCooltime += Time.deltaTime;

        if (moveType == MoveType.BackAndForth)
        {
            if ((int)timer % 2 == 0)
                this.gameObject.transform.Translate(Vector2.right * speed);
            else
                this.gameObject.transform.Translate(Vector2.left * speed);
        }
        else if(moveType == MoveType.Jump)
        {
            if (timer > 2f)
            {
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                timer = 0f;
            }
        }
        else if(moveType == MoveType.All)
        {
            if(jumpCooltime > 3f)
            {
                rigidBody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumpCooltime = 0f;
            }

            if ((int)timer % 2 == 0)
                this.gameObject.transform.Translate(Vector2.right * speed);
            else
                this.gameObject.transform.Translate(Vector2.left * speed);
        }
    }
}
