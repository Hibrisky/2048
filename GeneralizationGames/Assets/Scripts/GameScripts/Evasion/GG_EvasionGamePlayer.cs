using UnityEngine;

public class GG_EvasionGameManager : MonoBehaviour
{
    //�ش� ��ü�� ���Ͱ� �ִ�������.

    private void Update()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
