using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStateController : MonoBehaviour
{
    [SerializeField] Button button;
    bool isHovering;


    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log("Hover end");
        //anim true
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        Debug.Log("Hover end");
        //anim false
    }
}
