using UnityEngine;
using UnityEngine.EventSystems;

public class BlockRaycast : MonoBehaviour
{
    public Camera allowedCamera;
    public LayerMask blockLayerMask;

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        /*
        if (allowedCamera != null && eventCamera != allowedCamera)
        {
            return false;
        }
        Ray ray = eventCamera.ScreenPointToRay(sp);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, blockLayerMask))
        {
            return false;
        }

        return true;
        */
        return false;
    }
}
