using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GridController GridCntrl;
    [SerializeField] public GameObject GameOverPopUp;

    void Start()
    {
        RestartGame();
    }

    void Update()
    {
        if (GridCntrl.State == States.Awaite)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                GridCntrl.DoMove(Vector2.up);
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                GridCntrl.DoMove(Vector2.down);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                GridCntrl.DoMove(Vector2.left);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                GridCntrl.DoMove(Vector2.right);
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