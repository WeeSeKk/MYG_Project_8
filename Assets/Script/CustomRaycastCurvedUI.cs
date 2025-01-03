using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class CustomRaycastCurvedUI : MonoBehaviour
{
    [SerializeField] IHMManager iHMManager;
    [SerializeField] InputActionReference trigger;
    [SerializeField] InputActionReference grab;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject carTest;
    public LayerMask uiLayerMask;
    Vector3 originPos;
    Vector3 newPos;
    float startingRotation = 0;
    bool orinPosSet = false;

    void Update()
    {
        CurvedUIInputModule.CustomControllerRay = new Ray(this.transform.position, this.transform.forward);
        CurvedUIInputModule.CustomControllerButtonState = trigger.action.triggered;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayerMask))
        {
            if (Physics.Raycast(ray, out hit) && grab.action.inProgress && !iHMManager.mainMenu && !iHMManager.selected)
            {
                Vector3 localPosition = hit.transform.InverseTransformPoint(hit.point);

                if (!orinPosSet)
                {
                    originPos = localPosition - ui.transform.localPosition;
                    orinPosSet = true;
                }

                newPos = localPosition - originPos;
                if (newPos.x < 20 && newPos.x > - 22) {
                    ui.transform.localPosition = new Vector3(newPos.x, 0, 0);
                }
            }
            else if (Physics.Raycast(ray, out hit) && grab.action.inProgress && !iHMManager.mainMenu && iHMManager.selected)
            {
                Vector3 localPosition = hit.transform.InverseTransformPoint(hit.point);

                if (!orinPosSet)
                {
                    originPos = localPosition;
                    startingRotation = carTest.transform.eulerAngles.y;
                    orinPosSet = true;
                }

                float rotationDelta = (localPosition.x - originPos.x) * 10;
                Vector3 newRotation = carTest.transform.eulerAngles;
                newRotation.y = startingRotation + rotationDelta; 
                carTest.transform.eulerAngles = newRotation;
            }
            if (!grab.action.inProgress)
            {
                orinPosSet = false;
            }
        }
    }
}