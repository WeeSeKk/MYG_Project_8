using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonStateController : MonoBehaviour
{
    IHMManager iHMManager;
    [SerializeField] RawImage raw;
    [SerializeField] GameObject optionButton;
    bool isHovering;

    void OnEnable()
    {
        iHMManager = GameObject.Find("IHMManager").GetComponent<IHMManager>();
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

    public void OnButtonClicked(Button button)
    {
        Debug.Log("IHM Call");
        iHMManager.OnCarButtonClick(button);
    }

    public void SetupButton(GameObject oldButton)
    {
        raw.texture = oldButton.transform.GetChild(1).GetComponent<RawImage>().texture;
        optionButton.SetActive(true);
    }

    public void OnSelectButtonClicked(Button button)
    {
        iHMManager.OnCarSelectedButtonClick(button);
    }

    public void OnColorButtonClicked(Button button)
    {
        iHMManager.OnCarColorButtonClick(button);
    }

    public void OnInfoButtonClicked(Button button)
    {
        iHMManager.OnCarInfoButtonClick(button);
    }
}
