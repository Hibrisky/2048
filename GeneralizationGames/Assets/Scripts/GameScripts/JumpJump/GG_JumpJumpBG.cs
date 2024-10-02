using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_JumpJumpBG : MonoBehaviour
{
    public float speed = 0.025f;

    private bool isStart = false;

    public bool Start
    {
        get { return isStart; }
        set { isStart = value; }
    }

    private void Update()
    {
        if (isStart == false) 
            return;

        gameObject.transform.Translate(Vector2.left * speed);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector2(17.1f,transform.position.y);
    }
}
