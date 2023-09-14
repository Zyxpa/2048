using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI TextNumber;
    [SerializeField] public Image image;
    private PanelType type;
    public int CurentNumber => type.Value;
    public PanelType Type 
    { 
        get => type;
        set 
        { 
            type = value;
            TextNumber.text = value.Value.ToString();
            TextNumber.color = value.TextColor;
            image.color = value.BackColor; 
        }
    }
}
