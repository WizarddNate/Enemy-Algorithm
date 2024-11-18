using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    public bool walkable = true; //this tells the dfs algorithm if a tile is walkable or not
    public Vector3 coords;
    //changes the color of offset tiles and a walkable or blocked status
    public void Init(bool isOffset,bool walkable)
    {
        this.walkable = walkable;
        _renderer.color = walkable ? (isOffset ? _offsetColor : _baseColor) : Color.black; // black represents the blocked tiles 
    }
    
}
