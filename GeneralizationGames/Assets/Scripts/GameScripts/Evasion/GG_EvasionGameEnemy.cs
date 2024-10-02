using UnityEngine;

public class GG_EvasionGameEnemy : MonoBehaviour
{
    //해당 객체가 플레이어가있는쪽으로.

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
