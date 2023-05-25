using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;
    void Start()
    {
        buttonText = GetComponent<Button>().GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyle.Normal;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyle.Italic;
    }
    public void afterClik()
    {
        buttonText.fontStyle = FontStyle.Italic;
    }
}