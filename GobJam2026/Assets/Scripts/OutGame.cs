using UnityEngine;
using UnityEngine.EventSystems;

public class OutGame : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Application.Quit();
    }
}
