using UnityEngine;
using Unity.Netcode;

[ExecuteInEditMode]
public class Zoom : NetworkBehaviour
{
    Camera myCamera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    [Range(0, 1)]
    public float currentZoom;
    public float sensitivity = 1;


    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        myCamera = GetComponent<Camera>();
        if (myCamera)
        {
            defaultFOV = myCamera.fieldOfView;
        }
    }

    void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        // Update the currentZoom and the camera's fieldOfView.
        currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
        currentZoom = Mathf.Clamp01(currentZoom);
        myCamera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
    }
}
