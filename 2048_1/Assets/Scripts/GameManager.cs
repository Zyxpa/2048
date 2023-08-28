using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GridController GridCntrl;
    [SerializeField] public GameObject GameOverPopUp;

    void Start()
    {
        RestartGame();
        //GridCntrl.SpawnTile(0, 0);
        //GridCntrl.SpawnTile(1, 1);
        //GridCntrl.SpawnTile(2, 2);
        //GridCntrl.SpawnTile(3, 3);
        //GridCntrl.SpawnTile(4, 3);
        //GridCntrl.SpawnTile(5, 2);
        //GridCntrl.SpawnTile(6, 1);
        //GridCntrl.SpawnTile(7, 0);
        //GridCntrl.SpawnTile(8, 0);
        //GridCntrl.SpawnTile(9, 1);
        //GridCntrl.SpawnTile(10, 2);
        //GridCntrl.SpawnTile(11, 3);
        //GridCntrl.SpawnTile(12, 3);
        //GridCntrl.SpawnTile(13, 2);
        //GridCntrl.SpawnTile(14, 1);

    }

    void Update()
    {
        if (GridCntrl.state == States.Awaite)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                GridCntrl.DoVerticalMove(true);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                GridCntrl.DoVerticalMove(false);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                GridCntrl.DoHorizontalMove(true);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                GridCntrl.DoHorizontalMove(false);
        }
    }
    public void GameOver(bool isWin)
    {
        if (isWin)
        {
            Debug.Log("GAme Over" + isWin.ToString());
        }
        else 
        {
            GameOverPopUp.SetActive(true);
            GameOverPopUp.transform.DOShakeScale((float)1, (float)0.5, 5); 
        }
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        GameOverPopUp.SetActive(false);
        GridCntrl.Clear();
        GridCntrl.OnStart();
        GridCntrl.SpawnTile();
        GridCntrl.SpawnTile();
    }
}