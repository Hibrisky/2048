using UnityEngine;

public class GG_TileRaw : MonoBehaviour
{
    public GG_TileCell[] cells { get; private set; }

    private void Awake()
    {
        cells = GetComponentsInChildren<GG_TileCell>();
    }
}
