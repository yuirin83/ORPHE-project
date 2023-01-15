using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool canTouch;
    float defaultScale;

    void Start()
    {
        canTouch = true;
        defaultScale = this.transform.localScale.x;
    }

    void Update()
    {
    }

    //UIはOnMouseEnterが効かないのでこちらで処理
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.canTouch)
        transform.DOScale(defaultScale * 1.2f, 0.24f).SetEase(Ease.OutCubic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(this.canTouch)
        transform.DOScale(defaultScale, 0.24f).SetEase(Ease.OutCubic);
    }

    //それ以外のオブジェクトについてはこちらでマウスオーバー処理
    void OnMouseEnter()
    {
        if(this.canTouch)
        transform.DOScale(defaultScale * 1.2f, 0.24f).SetEase(Ease.OutCubic);
    }

    void OnMouseExit()
    {
        if(this.canTouch)
        transform.DOScale(defaultScale, 0.24f).SetEase(Ease.OutCubic);
    }
}
