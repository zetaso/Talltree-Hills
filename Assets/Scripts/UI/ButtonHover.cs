using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class ButtonHoverClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    public float scale;
    public float off_alpha;
    [SerializeField] public UnityEvent onClick;

    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * scale;
        if (text)
        {
            Color new_color = text.color;
            new_color.a = 1;
            text.color = new_color;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        if (text)
        {
            Color new_color = text.color;
            new_color.a = off_alpha;
            text.color = new_color;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
        if (text)
        {
            Color new_color = text.color;
            new_color.a = off_alpha;
            text.color = new_color;
        }

        onClick.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * scale;
        if (text)
        {
            Color new_color = text.color;
            new_color.a = 1;
            text.color = new_color;
        }
    }
}
