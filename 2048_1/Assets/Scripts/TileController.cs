using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextNumber;
    [SerializeField] Image image;
    private PanelType type;
    public int CurentNumber => type.Value;
    public PanelType Type 
    { 
        get => type;
        set => SetPanelType(value);
    }

    void SetPanelType(PanelType panel)
    {
        type = panel;
        TextNumber.text = panel.Value.ToString();
        TextNumber.color = panel.TextColor;
        image.color = panel.BackColor;
    }
}
