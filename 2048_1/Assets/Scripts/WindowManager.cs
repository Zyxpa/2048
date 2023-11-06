using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public void Show()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.DOShakeScale(1f, 0.5f, 5);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
