using UnityEngine;

public class GridTile
{
    public Colour tileColour;
    public GameObject tileObject;

    public GridTile(Colour tileColour, GameObject tile)
    {
        Colour = tileColour;
        Tile = tile;
    }

    public Colour Colour
    {
        get => tileColour;
        private set => tileColour = value;
    }

    public GameObject Tile
    {
        get => tileObject;
        private set => tileObject = value;
    }
}
