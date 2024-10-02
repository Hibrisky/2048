using UnityEngine;

public class GG_EvasionGameManager : MonoBehaviour
{
    //해당 객체가 몬스터가 있는쪽으로.

    private void Update()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
