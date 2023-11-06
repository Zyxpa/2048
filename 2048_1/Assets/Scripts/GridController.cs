﻿using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GridController : MonoBehaviour
{

    [SerializeField] TileController tilePrefub;
    [SerializeField] GridPanelComponent[] Grid;
    [SerializeField] EndGameEvent onEndGame;

    const float probabilityOfZeroElement = 0.8f;
    int countOfEmptyCell, gridDimension, countOfCell, winNumber;
    PanelTypes panelTypes;

    public UnityEvent OnGridControllerIsAwake;
    bool CanMakeMove => CheckMove();
    async void Awake()
    {
        panelTypes = await Addressables.LoadAssetAsync<PanelTypes>("PanelTypes").Task;

        //for (int i = 0; i < Grid.Length; i++)
        //    Grid[i].cellNumber = i;
        countOfCell = Grid.Length; 
        gridDimension = ((int)Math.Sqrt(countOfCell));
        winNumber = panelTypes.MaxValue();
        countOfEmptyCell = countOfCell;

        OnGridControllerIsAwake?.Invoke();
    }
    [ContextMenu("CheckMove")]
    private bool CheckMove()
    {
        for (int i = 0; i < countOfCell; i++)
            if (Grid[i].IsEmpty || CanMergeRight(i) || CanMergeLeft(i) || CanMergeUp(i) || CanMergeDown(i)) return true;
        return false;
    }

    private bool CanMergeRight(int pos) =>
        pos + 1 < countOfCell && (pos + 1) % gridDimension != 0 && !Grid[pos + 1].IsEmpty && Grid[pos].curentTile.CurentNumber == Grid[pos + 1].curentTile.CurentNumber;
    private bool CanMergeLeft(int pos) =>
        pos - 1 >= 0 && pos % gridDimension != 0 && Grid[pos].curentTile.CurentNumber == Grid[pos - 1].curentTile.CurentNumber;
    private bool CanMergeUp(int pos) =>
        pos - gridDimension >= 0 && Grid[pos].curentTile.CurentNumber == Grid[pos - gridDimension].curentTile.CurentNumber;
    private bool CanMergeDown(int pos) =>
        pos + gridDimension < countOfCell && !Grid[pos + gridDimension].IsEmpty && Grid[pos].curentTile.CurentNumber == Grid[pos + gridDimension].curentTile.CurentNumber;
    private void MoveAnimation(GameObject currentCell, GameObject targetCell, bool isMerge, UnityEvent animationEnd)
    {
        currentCell.transform.DOMove(targetCell.transform.position, 0.15f).OnComplete(
            () => 
            {
                if (isMerge)
                    DestroyCell(currentCell); 
                animationEnd.Invoke();
            });
    }

    private void DestroyCell(GameObject objectToDestroy)
    {
        countOfEmptyCell++;
        Destroy(objectToDestroy);
    }

    internal void DoMove(Vector2 direction, AnimationState.EndMovingEvent endMovingHandler, UnityEvent animationEnd)
    {
        int startPosition = 0, internalStep = 0, externalStep = 0, countOfAnimation = 0;
        if (direction == Vector2.up)
        {
            startPosition = 0;
            internalStep = gridDimension;
            externalStep = 1;
        }
        if (direction == Vector2.down)
        {
            startPosition = gridDimension*gridDimension -1;
            internalStep = -gridDimension;
            externalStep = -1;
        }
        if (direction == Vector2.left)
        {
            startPosition = 0;
            internalStep = 1;
            externalStep = gridDimension;
        }
        if (direction == Vector2.right)
        {
            startPosition = gridDimension*gridDimension -1;
            internalStep = -1;
            externalStep = -gridDimension;
        }
        if (internalStep == 0)
        {
            endMovingHandler.Invoke(countOfAnimation);
            return;
        }
            

        //Debug.Log(direction);
        for (int i = 0; i < gridDimension; i++, startPosition += externalStep)
        {
            int lastEmptyCell = -1;
            int lastFilledCell = -1;
            for (int j = 0, position = startPosition; j < gridDimension; j++, position += internalStep)
            {
                if (Grid[position].IsEmpty && lastEmptyCell < 0)
                    lastEmptyCell = position;
                if (Grid[position].IsEmpty) continue;
                
                if (lastFilledCell > -1 && Grid[lastFilledCell].curentTile.CurentNumber == Grid[position].curentTile.CurentNumber)
                {
                    //слияние 
                    MoveAnimation(Grid[position].curentTile.gameObject, Grid[lastFilledCell].gameObject, true, animationEnd);
                    countOfAnimation++;
                    Grid[lastFilledCell].curentTile.Type = panelTypes.GetPanel(Grid[lastFilledCell].curentTile.Type.Index + 1);
                    if (Grid[lastFilledCell].curentTile.Type.Value == winNumber) 
                        onEndGame?.Invoke(true);
                    lastEmptyCell = lastFilledCell + internalStep; 
                    continue;
                }
                lastFilledCell = lastEmptyCell < 0 ? position : lastEmptyCell;
                if (lastEmptyCell < 0) continue;
                MoveAnimation(Grid[position].curentTile.gameObject, Grid[lastEmptyCell].gameObject, false, animationEnd);
                countOfAnimation++;
                Grid[lastEmptyCell].curentTile = Grid[position].curentTile;
                Grid[position].curentTile = null;
                lastEmptyCell += internalStep;
                
            }
        }
        endMovingHandler.Invoke(countOfAnimation);
    }
    public void SpawnTile()
    {
        var randomVal = UnityEngine.Random.Range(0, countOfEmptyCell);
        int cellNumber = 0;
        for (int i = 0; i < Grid.Length; i++)
        {
            if (!Grid[i].IsEmpty) continue;
            randomVal--;
            if (randomVal < 1)
            {
                cellNumber = i;
                break;
            }
        }

        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        
        if (UnityEngine.Random.value < probabilityOfZeroElement)
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(0);
        else
            Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(1);

        countOfEmptyCell--;

        if (countOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);

    }
    public void SpawnTile(int cellNumber, int tileValueNumber = 0)
    {
        Grid[cellNumber].curentTile = Instantiate(tilePrefub, Grid[cellNumber].transform.position, Quaternion.identity, this.transform);
        Grid[cellNumber].curentTile.Type = panelTypes.GetPanel(tileValueNumber);
        countOfEmptyCell--;
        if (countOfEmptyCell <= 0 && !CanMakeMove)
            onEndGame.Invoke(false);
    }

    public void Clear()
    {
        Grid.Where(x => !x.IsEmpty).ToList().ForEach(x => { Destroy(x.curentTile.gameObject); x.curentTile = null; });
        countOfEmptyCell = countOfCell;
    }
    [Serializable]
    public class EndGameEvent : UnityEvent<bool> { }
}
