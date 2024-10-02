using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GG_IdleGame : MonoBehaviour
{

    public GameObject obj_EnemySpawnPos;
    public GameObject pref_Enemy;
    List<GG_EvasionGameEnemy> list_Enemy = new List<GG_EvasionGameEnemy>();

    public GameObject obj_Player;

    // Start is called before the first frame update
    void Start()
    {
        //enemy spawn
        for (int i = 0; i < 10; ++i)
        {
            GG_EvasionGameEnemy enemy = Instantiate(pref_Enemy, obj_EnemySpawnPos.transform).GetComponent<GG_EvasionGameEnemy>();
            enemy.gameObject.transform.position = new Vector2(Random.Range(0f,6.5f), Random.Range(-4.5f, 4.5f)); 
            enemy.Init(obj_Player.transform);
            list_Enemy.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var enemy in list_Enemy)
        {
            enemy.EnemyMove(obj_Player.transform);
        }
    }
}
