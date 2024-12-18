using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using GameManagerNamespace;

public class ButtonStateController : MonoBehaviour
{
    IHMManager iHMManager;
    [SerializeField] RawImage raw;
    [SerializeField] GameObject optionButton;
    [SerializeField] TMP_Text carNameText;
    [SerializeField] TMP_Text carInfoText;
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
        iHMManager.OnCarButtonClick(button);
    }

    public void SetupButton(GameObject oldButton)
    {
        raw.texture = oldButton.transform.GetChild(1).GetComponent<RawImage>().texture;
        carNameText.text = oldButton.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text;
        EventManager.ChangeSelectedCar(GameManager.instance.SelectedCar(carNameText.text));
        CarSpecs carSpecs = GameManager.instance.GetCarSpecs(carNameText.text);

        carInfoText.text = $"Constructor : {carSpecs.modelManufacturer}\n\n" +
                   $"Model : {carSpecs.modelName}\n\n" +
                   $"Horse Power : {carSpecs.horsePower}\n\n" +
                   $"Weight : {carSpecs.modelWeight}";

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
