using System.Collections.Generic;
using UnityEngine;

public class GG_JumpJumpManager : MonoBehaviour
{
    public GameObject obj_GameOver;

    public List<GG_JumpJumpBG> list_JumpJumpBG = new List<GG_JumpJumpBG>();

    private void Start()
    {
        obj_GameOver.SetActive(false);
        foreach(var bg in list_JumpJumpBG)
        {
            bg.Start = true;
        }
    }


    public void Show_GameOver()
    {
        obj_GameOver.SetActive(true);
        foreach (var bg in list_JumpJumpBG)
        {
            bg.Start = false;
        }
    }
}
