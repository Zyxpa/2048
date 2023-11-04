using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GridController GridCntrl;
    [SerializeField] WindowManager GameOverPopUp;
    [SerializeField] WindowManager WinPopUp;

    FSM fsm;
    void Start()
    {
        GridCntrl.OnGridControllerIsAwake.AddListener(RestartGame);
        fsm = this.gameObject.AddComponent<FSM>();

        fsm.AddState(new AwaiteState(fsm));
        fsm.AddState(new SpawnState(fsm, GridCntrl));
        fsm.AddState(new AnimationState(fsm, GridCntrl));
        
        fsm.SetState<AwaiteState>();
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