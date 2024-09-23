using UnityEngine;

public class GG_TIleGrid : MonoBehaviour
{
    public GG_TileRaw[] rows { get; private set; }
    public GG_TileCell[] cells { get; private set; }

    public int size => cells.Length;

    public int height => rows.Length;

    public int width  => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<GG_TileRaw>();
        cells = GetComponentsInChildren<GG_TileCell>();
    }

    private void Start()
    {
        for(int i=0;i<rows.Length;++i)
        {
            for(int j =0; j < rows[i].cells.Length;++j)
            {
                rows[i].cells[j].coordinate = new Vector2Int(j, i);
            }
        }
    }

    public GG_TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return rows[y].cells[x];
        else
            return null;
    }

    public GG_TileCell GetCell(Vector2Int _coordinates)
    {
        return GetCell(_coordinates.x, _coordinates.y);
    }


    public GG_TileCell GetAdjacentCell(GG_TileCell _cell,Vector2Int _dir)
    {
        Vector2Int coordinates = _cell.coordinate;
        coordinates.x += _dir.x;
        coordinates.y -= _dir.y;

        return GetCell(coordinates);
    }

    public GG_TileCell GetRandomEmptyCell()
    {
        int idx = Random.Range(0, cells.Length);
        int startingIdx = idx;

        while (cells[idx].occupied)
        {
            idx++;

            if(idx >= cells.Length)
                idx = 0;

            if(idx == startingIdx)
                return null; 
        }
        return cells[idx];
    }
}
