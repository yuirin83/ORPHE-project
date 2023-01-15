using DG.Tweening;  
using UnityEngine;  
using UnityEngine.UI;  
using UnityEngine.EventSystems;  

public class CustomButton : MonoBehaviour,  
    IPointerClickHandler,  
    IPointerDownHandler,  
    IPointerUpHandler  
{
    public System.Action onClickCallback; 
    public Image image;

    private void Start() {
        image = this.gameObject.GetComponent<Image>();    
    }

    public void OnPointerClick(PointerEventData eventData)  
    {
        onClickCallback?.Invoke();  
    }

    public void OnPointerDown(PointerEventData eventData)  
    {
        transform.DOScale(0.95f, 0.24f).SetEase(Ease.OutCubic);  
    }

    public void OnPointerUp(PointerEventData eventData)  
    {
        transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);  
    }
}