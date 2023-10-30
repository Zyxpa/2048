using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GridController GridCntrl;
    [SerializeField] WindowManager GameOverPopUp;
    [SerializeField] WindowManager WinPopUp;

    private GameInput inputActions;

    void Start()
    {
        GridCntrl.OnGridControllerIsAwake.AddListener(RestartGame);
        inputActions = new GameInput();
        inputActions.Enable();
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

            WinPopUp.Show();
        else
            GameOverPopUp.Show();
       
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        GameOverPopUp.Hide();
        WinPopUp.Hide();
        GridCntrl.Clear();
        if (GridCntrl.State == States.Awaite)
        {
            GridCntrl.SpawnTile();
            GridCntrl.SpawnTile();
        }
    }
}