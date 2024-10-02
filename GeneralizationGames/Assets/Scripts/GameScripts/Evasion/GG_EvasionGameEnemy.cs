using UnityEngine;

public class GG_EvasionGameEnemy : MonoBehaviour
{
    //�ش� ��ü�� �÷��̾�ִ�������.

    Transform ts_PlayerPos;

    public float fMoveSpeed = 2f;

    public void Init(Transform _playerpos)
    {
        ts_PlayerPos = _playerpos;
    }

    public void EnemyMove(Transform _playerpos)
    {
        ts_PlayerPos = _playerpos;

        this.transform.position = Vector2.MoveTowards(transform.position,ts_PlayerPos.position, fMoveSpeed * Time.deltaTime);
    }


}
