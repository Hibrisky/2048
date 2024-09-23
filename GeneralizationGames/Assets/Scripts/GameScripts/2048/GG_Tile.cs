using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GG_Tile : MonoBehaviour
{
    public GG_TileState state { get; private set; }
    public GG_TileCell cell { get; private set; }

    public int number { get; private set; }
    public bool locked { get; set; }


    Image background;
    Text text;

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }


    public void SetState(GG_TileState _state, int _number)
    {
        this.state = _state;
        this.number = _number;

        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = _number.ToString();
    }

    public void Spawn(GG_TileCell _cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = _cell;
        this.cell.tile = this;

        transform.position = _cell.transform.position;
    }

    public void MoveTo(GG_TileCell _cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = _cell;
        this.cell.tile = this;

        StartCoroutine(co_Animate(_cell.transform.position,false));
    }

    public void Merge(GG_TileCell _cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = null;
        _cell.tile.locked = true;
        StartCoroutine(co_Animate(_cell.transform.position,true));
    }


    IEnumerator co_Animate(Vector3 _to, bool _merging)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;

        while(elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, _to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _to;

        if (_merging)
            Destroy(gameObject);
    }
}
