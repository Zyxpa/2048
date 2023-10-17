using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Defs/PanelTypes", fileName = "PanelTypes")]
public class PanelTypes : ScriptableObject
{
    [SerializeField] PanelType[] panelTypes;

    public PanelType GetPanel(int index)
    {
        Debug.Log(index);
        return panelTypes[index];
    }

    public int MaxValue()
    {
        return 2 << (panelTypes.Length-1);
    }

}

[Serializable]
public struct PanelType
{
    [SerializeField] int index;
    [SerializeField] int value;
    [SerializeField] Color backColor;
    [SerializeField] Color textColor;

    public int Index => index;
    public int Value => value;
    public Color BackColor => backColor;
    public Color TextColor => textColor;
}