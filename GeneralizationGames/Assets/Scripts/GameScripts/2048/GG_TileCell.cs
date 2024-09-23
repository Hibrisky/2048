using UnityEngine;

public class GG_TileCell : MonoBehaviour
{
    public Vector2Int coordinate { get; set; }
    public GG_Tile tile { get; set; }

    public bool empty => tile == null;
    public bool occupied => tile != null;
}
