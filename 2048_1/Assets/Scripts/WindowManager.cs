using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public void Show()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.DOShakeScale((float)1, (float)0.5, 5);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
