using UnityEngine;

public class GridPanelComponent : MonoBehaviour
{
    public TileController curentTile;
    public bool IsEmpty => curentTile == null;
    public int cellNumber;
}
