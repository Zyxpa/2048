using DG.Tweening;
using System;
using System.Linq;
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

    int countOfEmptyCell;
    int gridDimension;
    int countOfCell;
    PanelTypes panelTypes;
    public States State;
    
    public void OnStart()
    {
        countOfEmptyCell = Grid.Length;
        gridDimension = ((int)Math.Sqrt(Grid.Length));
        State = States.Awaite;
    }
    private void Update()
    {
        if (State == States.Spawn)
            SpawnTile();
    }

    void Awake()
    {
        panelTypes = Resources.Load<PanelTypes>("PanelTypes");
        for (int i = 0; i < Grid.Length; i++)
            Grid[i].cellNumber = i;
        countOfCell = Grid.Length;
        OnStart();
    }
    [ContextMenu("CheckMove")]
    private bool CheckMove()
    {
        for(int i = 0; i < countOfCell; i++)
        {
            if (Grid[i].IsEmpty) return true;
            if (i + 1< countOfCell && (i+1) % 4 != 0 && !Grid[i + 1].IsEmpty && Grid[i].curentTile.CurentNumber == Grid[i + 1].curentTile.CurentNumber) return true;
            if (i - 1 >= 0 &&  i%4 != 0 && Grid[i].curentTile.CurentNumber == Grid[i - 1].curentTile.CurentNumber) return true;
            if (i + 4 < countOfCell && !Grid[i + 4].IsEmpty && Grid[i].curentTile.CurentNumber == Grid[i + 4].curentTile.CurentNumber) return true;
            if (i - 4 >= 0  && Grid[i].curentTile.CurentNumber == Grid[i - 4].curentTile.CurentNumber) return true;
        }
        return false;
    }

    private void MoveAnimation(GameObject currentCell, GameObject targetCell, bool isMerge)
    {
        State = States.Animation;
        currentCell.transform.DOMove(targetCell.transform.position, (float)0.1).OnComplete(
            () => 
            {
                State = State = States.Spawn;
                if (isMerge)
                    DestroyCell(currentCell); 
            });
        
    }

    private void DestroyCell(GameObject objectToDestroy)
    {
        countOfEmptyCell++;
        Destroy(objectToDestroy);
    }

    public void DoMove(Vector2 direction)
    {
        int startPosition = 0, internalStep = 0, externalStep = 0;
        if (direction == Vector2.up)
        {
            startPosition = 0;
            internalStep = 4;
            externalStep = 1;
        }
        if (direction == Vector2.down)
        {
            startPosition = 12;
            internalStep = -4;
            externalStep = 1;
        }
        if (direction == Vector2.left)
        {
            startPosition = 0;
            internalStep = 1;
            externalStep = 4;
        }
        if (direction == Vector2.right)
        {
            startPosition = 3;
            internalStep = -1;
            externalStep = 4;
        }

        for (int i = 0; i < gridDimension; i++, startPosition += externalStep)
        {
            int lastEmptyCell = -1;
            int lastFilledCell = -1;
            for (int j = 0, position = startPosition; j < gridDimension; j++, position += internalStep)
            {
                if (Grid[position].IsEmpty && lastEmptyCell < 0)
                    lastEmptyCell = position;
                if (!Grid[position].IsEmpty)
                {
                    if (lastFilledCell > -1 && Grid[lastFilledCell].curentTile.CurentNumber == Grid[position].curentTile.CurentNumber)
                    {
                        //слияние 
                        MoveAnimation(Grid[position].curentTile.gameObject, Grid[lastFilledCell].gameObject, true);
                        Grid[lastFilledCell].curentTile.Type = panelTypes.GetPanel(Grid[lastFilledCell].curentTile.Type.Index + 1);
                        if (Grid[lastFilledCell].curentTile.Type.Value == 2048) // можно спрятать в сеттер
                            onEndGame.Invoke(true);
                        lastEmptyCell = lastFilledCell + internalStep; 
                        continue;
                    }
                    lastFilledCell = lastEmptyCell < 0 ? position : lastEmptyCell;
                    if (lastEmptyCell < 0) continue;
                    MoveAnimation(Grid[position].curentTile.gameObject, Grid[lastEmptyCell].gameObject, false);
                    Grid[lastEmptyCell].curentTile = Grid[position].curentTile;
                    Grid[position].curentTile = null;
                    lastEmptyCell = lastEmptyCell + internalStep;
                }
            }
        }

        if (State == States.ButtonPress)
            State = States.Awaite;

    }
    public void SpawnTile()
    {
        var randomVal = UnityEngine.Random.Range(0, countOfEmptyCell);
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
        
        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        
        if (UnityEngine.Random.value < 0.8)
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(0);
        else
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(1);

        countOfEmptyCell--;
        State = States.Awaite;

        if (countOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);
    }
    public void SpawnTile(int cellNumber, int tileValueNumber = 0)
    {
        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(tileValueNumber);
        countOfEmptyCell--;
        State = States.Awaite;
        Debug.Log(countOfEmptyCell.ToString() + " " + CanMakeMove.ToString());
        if (countOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);
    }

    public void Clear()
    {
        Grid.Where(x => !x.IsEmpty).ToList().ForEach(x => { Destroy(x.curentTile.gameObject); x.curentTile = null; });
        Debug.Log("After Clear " + Grid.Where(x => !x.IsEmpty).ToList().Count.ToString());
    }

    [Serializable]
    public class EndGameEvent : UnityEvent<bool> { }
}
