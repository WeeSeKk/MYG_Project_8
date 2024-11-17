using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class CustomRaycastCurvedUI : MonoBehaviour
{
    [SerializeField] InputActionReference trigger;
    [SerializeField] InputActionReference grab;
    [SerializeField] GameObject ui;
    public LayerMask uiLayerMask;
    Vector3 originPos;
    Vector3 newPos;
    bool orinPosSet = false;

    void Update()
    {
        CurvedUIInputModule.CustomControllerRay = new Ray(this.transform.position, this.transform.forward);
        CurvedUIInputModule.CustomControllerButtonState = trigger.action.triggered;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiLayerMask))
        {
            Debug.Log("Hit : " + hit.collider.name);

            if (Physics.Raycast(ray, out hit) && grab.action.inProgress)
            {
                Vector3 localPosition = hit.transform.InverseTransformPoint(hit.point);

                if (!orinPosSet)
                {
                    originPos = localPosition - ui.transform.localPosition;;
                    orinPosSet = true;
                }

                newPos = localPosition - originPos;
                ui.transform.localPosition = new Vector3(newPos.x, 0, 0);

                Debug.Log("Position : " + localPosition);
            }
            if (!grab.action.inProgress)
            {
                orinPosSet = false;
            }
        }
    }

}