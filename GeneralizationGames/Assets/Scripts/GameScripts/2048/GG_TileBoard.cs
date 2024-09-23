using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GG_TileBoard : MonoBehaviour
{
    public GG_GameManager gameManager;

    public GameObject obj_GameOver;
    public GG_Tile tilePrefab;
    public GG_TileState[] tileStates;
    GG_TIleGrid grid;
    List<GG_Tile> tiles;

    bool waiting = false;

    Vector2 clickStartPos = Vector2.zero;
    Vector2 clickEndPos = Vector2.zero;
    Vector3 swipeDir = Vector3.zero;

    private void Awake()
    {
        grid = GetComponentInChildren<GG_TIleGrid>();
        tiles = new List<GG_Tile>(16);
    }

    public void ClearBoard()
    {
        obj_GameOver.SetActive(false);

        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach(var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    public void CreateTile()
    {
        GG_Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (!waiting) //
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickStartPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                clickEndPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                swipeDir = new Vector3(clickEndPos.x - clickStartPos.x, clickEndPos.y - clickStartPos.y);
                swipeDir.Normalize();

                if (swipeDir.y > 0f && swipeDir.x > -0.5f && swipeDir.x < 0.5f)
                {
                    MoveTiles(Vector2Int.up, 0, 1, 1, 1);
                }
                if (swipeDir.y < 0f && swipeDir.x > -0.5f && swipeDir.x < 0.5f)
                {
                    MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
                }
                if (swipeDir.x > 0f && swipeDir.y > -0.5f && swipeDir.y < 0.5f)
                {
                    MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
                }
                if (swipeDir.x < 0f && swipeDir.y > -0.5f && swipeDir.y < 0.5f)
                {
                    MoveTiles(Vector2Int.left, 1, 1, 0, 1);
                }
            }
        }
    }

    void MoveTiles(Vector2Int _dir, int _startX, int _incrementX, int _startY, int _incrementY)
    {
        bool changed = false;

        for (int i = _startX; i >= 0 && i < grid.width; i += _incrementX)
        {
            for (int j = _startY; j >= 0 && j < grid.height; j += _incrementY)
            {
                GG_TileCell cell = grid.GetCell(i, j);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, _dir);
                }
            }
        }

        if (changed)
            StartCoroutine(co_WaitForChanges());


    }

    bool MoveTile(GG_Tile _tile, Vector2Int _dir)
    {
        GG_TileCell newCell = null;
        GG_TileCell adjacent = grid.GetAdjacentCell(_tile.cell, _dir);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                //TODO : Merging
                if (CanMerge(_tile, adjacent.tile))
                {
                    Merge(_tile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, _dir);
        }

        if (newCell != null)
        {
            _tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    bool CanMerge(GG_Tile a, GG_Tile b)
    {
        return a.number == b.number && !b.locked;
    }

    void Merge(GG_Tile a, GG_Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int idx = Mathf.Clamp(Indexof(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;

        b.SetState(tileStates[idx], number);

        gameManager.IncreaseScore(number);
    }

    int Indexof(GG_TileState state)
    {
        for (int i = 0; i < tileStates.Length; ++i)
        {
            if (state == tileStates[i])
                return i;
        }

        return -1;
    }



    IEnumerator co_WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach(var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
            CreateTile();

        if(CheckGameOver())
        {
            obj_GameOver.SetActive(true);
            gameManager.GameOver_2048();
        }
    }

    bool CheckGameOver()
    {
        if(tiles.Count != grid.size)
            return false;

        foreach(var tile in tiles)
        {
            GG_TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            GG_TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            GG_TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            GG_TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if (up != null && CanMerge(tile, up.tile))
                return false;
            if (down != null && CanMerge(tile, down.tile))
                return false;
            if (left != null && CanMerge(tile, left.tile))
                return false;
            if (right != null && CanMerge(tile, right.tile))
                return false;
        }


        return true;
    }
}
