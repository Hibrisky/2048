using UnityEngine;

public class GG_JumpJumpEnemy : JumpEnemy
{
    private void OnBecameVisible()
    {
        isOut = true;
        Destroy(this.gameObject);
    }
}
