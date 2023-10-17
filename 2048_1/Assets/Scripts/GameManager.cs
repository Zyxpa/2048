using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GridController GridCntrl;
    [SerializeField] public GameObject GameOverPopUp;
    [SerializeField] public GameObject WinPopUp;

    private GameInput inputActions;

    void Start()
    {
        inputActions = new GameInput();
        inputActions.Enable();

        RestartGame();
    }

    void Update()
    {
        if (GridCntrl.State == States.Awaite)
        {
            var inputDirecton = inputActions.Main.Keyboard.ReadValue<Vector2>();
            if(inputDirecton != Vector2.zero)
                GridCntrl.DoMove(inputDirecton);
        }
    }
    public void GameOver(bool isWin)
    {
        if (isWin)
        {
            WinPopUp.SetActive(true);
            WinPopUp.transform.DOShakeScale((float)1, (float)0.5, 5);
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
        WinPopUp.SetActive(false);
        GridCntrl.Clear();
        GridCntrl.State = States.Awaite;
        GridCntrl.SpawnTile();
        GridCntrl.SpawnTile();
    }
}