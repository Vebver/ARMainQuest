using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector] public bool Pressed;
    private Image buttonImage;

    public Color normalColor = Color.white;
    public Color pressedColor = Color.gray;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        buttonImage.color = pressedColor; // change color on press
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        buttonImage.color = normalColor; // revert color
    }
}
