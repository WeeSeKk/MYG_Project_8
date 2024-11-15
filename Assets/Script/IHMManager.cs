using UnityEngine;
using DG.Tweening;
using CurvedUI;
using UnityEngine.UI;

public class IHMManager : MonoBehaviour
{
    [SerializeField] GameObject carDemo;
    [SerializeField] Canvas carDemoCanvas;
    [SerializeField] CurvedUISettings curvedUISettings;
    [SerializeField] Button returnButton;
    int uiPosition = 35;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carDemoCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnterHoverCar()
    {
        carDemoCanvas.enabled = true;
    }
    public void OnExitHoverCar()
    {
        carDemoCanvas.enabled = false;
    }

    public void OnLoading()
    {
        //Debug.Log(curvedUISettings.Angle);
        int startValue = curvedUISettings.Angle;
        int endValue = 360;

        DOTween.To(() => startValue, x =>
        {
            curvedUISettings.Angle = x;

        }, endValue, 2f).SetEase(Ease.OutCirc);

        //Debug.Log(curvedUISettings.Angle);
    }

    public void test(Button button)
    {
        button.interactable = false;
        Debug.Log("Pressed : " + button.name);
        button.interactable = true;
    }

    public void ScrollImage(GameObject image)
    {
        Vector3 startValue = image.transform.localPosition;

        if (startValue.x < uiPosition)
        {
            Vector3 endValue = new Vector3(uiPosition, image.transform.localPosition.y, image.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                image.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
            returnButton.gameObject.SetActive(false);
            
        }
        else
        {
            Vector3 endValue = new Vector3(15, image.transform.localPosition.y, image.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                image.transform.localPosition = x;
            }, endValue, 2f).SetEase(Ease.OutCirc);
            returnButton.gameObject.SetActive(true);
            
        }
    }

    public void HideShowMainButton(GameObject gameObject)
    {
        Vector3 startValue = gameObject.transform.localPosition;

        if (gameObject.transform.localPosition.x == 0)
        {
            Vector3 endValue = new Vector3(-20, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                gameObject.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
        }
        else
        {
            Vector3 endValue = new Vector3(0, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            DOTween.To(() => startValue, x =>
            {
                gameObject.transform.localPosition = x;

            }, endValue, 2f).SetEase(Ease.OutCirc);
        }
    }
}
