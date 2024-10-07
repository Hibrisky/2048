using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GG_JumpJumpManager : MonoBehaviour
{
    public GameObject obj_GameOver;

    public List<GG_JumpJumpBG> list_JumpJumpBG = new List<GG_JumpJumpBG>();
    public GG_JumpJumpBG jumpjumpEnemyArea;

    public Transform enemySpawnPos;

    public GameObject[] EnemysType = new GameObject[3];

    public GG_JumpJumpPlayer player;

    float timer = 0f;
    float score = 0f;
    public Text txt_Score;

    private void Start()
    {
        obj_GameOver.SetActive(false);
        foreach(var bg in list_JumpJumpBG)
        {
            bg.MoveStart = true;
        }

        jumpjumpEnemyArea.MoveStart = true;
        txt_Score.text = "0";
    }

    public void ReStartGame()
    {
        obj_GameOver.SetActive(false);
        foreach (var bg in list_JumpJumpBG)
        {
            bg.MoveStart = true;
        }

        jumpjumpEnemyArea.MoveStart = true;
        txt_Score.text = "0";
        Time.timeScale = 1f;

        player.ResetPosition();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        score += Time.deltaTime;

        if (timer > 2.5f)
        {
            MonsterSpawn();
            timer = 0f;
        }

        txt_Score.text = ((int)score).ToString();
    }

    //몬스터 스폰.
    public void MonsterSpawn()
    {
        int rand = Random.Range(0, 3);
        GameObject obj = Instantiate(EnemysType[rand], enemySpawnPos);
        obj.transform.SetParent(jumpjumpEnemyArea.transform);
        obj.transform.position = enemySpawnPos.position;
    }


    public void Show_GameOver()
    {
        obj_GameOver.SetActive(true);
        foreach (var bg in list_JumpJumpBG)
        {
            bg.MoveStart = false;
        }
        jumpjumpEnemyArea.MoveStart = false;

        Time.timeScale = 0f;
    }
}
