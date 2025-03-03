using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState state {get; private set;}
    public TileCell cell {get; private set;}
    public int number {get; private set;}
    private Image background;
    private TextMeshProUGUI text;
    public bool locked { get; set; }

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();


    }

    public void Spawn(TileCell cell)
    {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        
        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell)
    {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        
        this.cell = cell;
        this.cell.tile = this;

        StartCoroutine(Animate(cell.transform.position, false)); //beginning of coroutine
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null) {
            this.cell.tile = null;
        }

        this.cell = null;
        cell.tile.locked = true;

        StartCoroutine(Animate(cell.transform.position, true));
    }

    private IEnumerator Animate(Vector3 to, bool merging) //Animate(Vector3 direction, duration)
    {
        float elapsed = 0f; //beginning of a timer, calculate how much time has past
        float duration = 0.1f; //time of animation

        Vector3 from = transform.position; //saving start position of Object

        while (elapsed < duration) //while elapsed not reach duration
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration); //smooth shifting, elapsed/duration represents how far are we in animation (between 0 to 1)
            elapsed += Time.deltaTime; //sync animation with time
            yield return null; //pause untill next frame
        }

        transform.position = to; //setting exact end position 

        if(merging) {
            Destroy(gameObject);
        }
    }
    
}
