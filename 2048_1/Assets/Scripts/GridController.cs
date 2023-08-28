using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public enum States
{
    Awaite,
    Animation,
    Merge,
    Spawn,
    ButtonPress
}
public class GridController : MonoBehaviour
{

    [SerializeField] public TileController tilePrefub;
    [SerializeField] public GridPanelComponent[] Grid;
    [SerializeField] private EndGameEvent onEndGame;

    bool CanMakeMove => CheckMove();

    int CountOfEmptyCell;
    int CountOfCell;
    PanelTypes panelTypes;
    public States state;
    
    public void OnStart()
    {
        CountOfEmptyCell = Grid.Length;
        state = States.Awaite;
    }
    private void Update()
    {
        if (state == States.Spawn)
            SpawnTile();
    }

    void Awake()
    {
        panelTypes = Resources.Load<PanelTypes>("PanelTypes");
        for (int i = 0; i < Grid.Length; i++)
            Grid[i].cellNumber = i;
        CountOfCell = Grid.Length;
        OnStart();
    }
    [ContextMenu("Test")]
    private bool CheckMove()
    {
        for(int i = 0; i < CountOfCell; i++)
        {
            if (Grid[i].IsEmpty) return true;
            if (i + 1< CountOfCell && (i+1) % 4 != 0 && !Grid[i + 1].IsEmpty && Grid[i].curentTile.curentNumber == Grid[i + 1].curentTile.curentNumber) return true;
            if (i - 1 >= 0 &&  i%4 != 0 && Grid[i].curentTile.curentNumber == Grid[i - 1].curentTile.curentNumber) return true;
            if (i + 4 < CountOfCell && !Grid[i + 4].IsEmpty && Grid[i].curentTile.curentNumber == Grid[i + 4].curentTile.curentNumber) return true;
            if (i - 4 >= 0  && Grid[i].curentTile.curentNumber == Grid[i - 4].curentTile.curentNumber) return true;
        }
        return false;
    }

    private void MoveAnimation(GameObject currentCell, GameObject targetCell, bool isMerge)//TweenCallback func)
    {
        state = States.Animation;
        //Debug.Log("Move Tile " + currentCell.ToString() + " to " + targetCell.ToString());
        currentCell.transform.DOMove(targetCell.transform.position, (float)0.01).OnComplete(() => f(isMerge ? currentCell : null));
    }

    private void f(GameObject objectToDestroy)
    {
        state = state = States.Spawn;
        if (objectToDestroy != null)
        {
            CountOfEmptyCell++;
            Destroy(objectToDestroy);
        }
    }

    public void DoVerticalMove(bool isUp)
    {
        state = States.ButtonPress;
        int startPosition, direction;
        bool makeMovement = false;
        if (isUp)
        {
            startPosition = 0;
            direction = 1;
        }
        else
        {
            startPosition = 15;
            direction = -1;
        }
        for (int j = startPosition; j < 4 || j > 11; j += direction)
        {
            int lastEmptyCell = Grid[j].IsEmpty ? j : -1;
            int lastFilledCell = !Grid[j].IsEmpty ? j : -1;
            int val = j + 4 * direction;
            for (int i = val; i < 16 && i > -1; i += 4 * direction)
            {
                if (Grid[i].IsEmpty && lastEmptyCell < 0)
                    lastEmptyCell = i;
                if (!Grid[i].IsEmpty)
                {
                    if (lastFilledCell > -1 && Grid[lastFilledCell].curentTile.curentNumber == Grid[i].curentTile.curentNumber)
                    {

                        MoveAnimation(Grid[i].curentTile.gameObject, Grid[lastFilledCell].gameObject, true);
                        Grid[lastFilledCell].curentTile.Type = panelTypes.GetPanel(Grid[lastFilledCell].curentTile.Type.Index + 1);
                        if (Grid[lastFilledCell].curentTile.Type.Value == 2048)
                           onEndGame.Invoke(true);
                        makeMovement = true;
                        lastEmptyCell = lastFilledCell + 4 * direction;
                        lastFilledCell = -1;
                        continue;
                    }
                    lastFilledCell = lastEmptyCell < 0 ? i : lastEmptyCell;
                    if (lastEmptyCell < 0) continue;
                    MoveAnimation(Grid[i].curentTile.gameObject, Grid[lastEmptyCell].gameObject, false);
                    Grid[lastEmptyCell].curentTile = Grid[i].curentTile;
                    Grid[i].curentTile = null;
                    makeMovement = true;
                    lastEmptyCell = lastEmptyCell + 4 * direction;
                }
            }
        }
        if (state == States.ButtonPress)
            state = States.Awaite;
    }
    public void DoHorizontalMove(bool isLeft)
    {

        state = States.ButtonPress;

        int startPosition, direction;
        bool makeMovement = false;
        if (isLeft)
        {
            startPosition = 0;
            direction = 1;
        }
        else
        {
            startPosition = 3;
            direction = -1;
        }
        for (int j = startPosition; j < 16; j += 4)
        {
            int lastFilledCell = !Grid[j].IsEmpty ? j : -1;
            int lastEmptyCell = Grid[j].IsEmpty ? j : -1;
            int val = j + direction;
            for (int i = val; i < j + 4 && i > j - 4; i += direction)
            {
                if (Grid[i].IsEmpty && lastEmptyCell < 0)
                    lastEmptyCell = i;
                if (!Grid[i].IsEmpty)
                {
                    if (lastFilledCell > -1 && Grid[lastFilledCell].curentTile.curentNumber == Grid[i].curentTile.curentNumber)
                    {
                        makeMovement = true;
                        MoveAnimation(Grid[i].curentTile.gameObject, Grid[lastFilledCell].gameObject, true);
                        Grid[lastFilledCell].curentTile.Type = panelTypes.GetPanel(Grid[lastFilledCell].curentTile.Type.Index + 1);
                        if (Grid[lastFilledCell].curentTile.Type.Value == 2048)
                            onEndGame.Invoke(true);
                        
                        lastEmptyCell = lastFilledCell + direction;
                        lastFilledCell = -1;
                        continue;
                    }
                    lastFilledCell = lastEmptyCell < 0 ? i : lastEmptyCell;
                    if (lastEmptyCell < 0) continue;
                    MoveAnimation(Grid[i].curentTile.gameObject, Grid[lastEmptyCell].gameObject, false);
                    Grid[lastEmptyCell].curentTile = Grid[i].curentTile;
                    makeMovement = true;
                    Grid[i].curentTile = null;
                    lastEmptyCell = lastEmptyCell + direction;
                }
            }
        }
        if (state == States.ButtonPress)
            state = States.Awaite;
    }

    public void SpawnTile()
    {
        var randomVal = UnityEngine.Random.Range(0, CountOfEmptyCell);
        int cellNumber = 0;
        for (int i = 0; i < Grid.Length; i++)
        {
            if (Grid[i].IsEmpty)
            {
                randomVal--;
                if (randomVal < 1)
                {
                    cellNumber = i;
                    break;
                }
            }
        }
        Debug.Log("Spawn new Tile in " + cellNumber.ToString() + " position "+ Grid[cellNumber].transform.position.ToString());
        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        if (UnityEngine.Random.value < 0.8)
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(0);
        else
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(1);
        CountOfEmptyCell--;
        state = States.Awaite;
        Debug.Log(CountOfEmptyCell.ToString() + " " + CanMakeMove.ToString());
        if (CountOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);
    }
    public void SpawnTile(int cellNumber, int tileValueNumber = 0)
    {
        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(tileValueNumber);
        CountOfEmptyCell--;
        state = States.Awaite;
        Debug.Log(CountOfEmptyCell.ToString() + " " + CanMakeMove.ToString());
        if (CountOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);
    }

    public void Clear()
    {

        foreach (var cell in Grid)
        {
            if (!cell.IsEmpty)
                Destroy(cell.curentTile.gameObject);
            cell.curentTile = null;
        }
    }

    [Serializable]
    public class EndGameEvent : UnityEvent<bool> { }
}
