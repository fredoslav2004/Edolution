using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float zoomSensitivity = 15f;
    private float getCurrentZoomSensitivity => targetCamera.orthographicSize * zoomSensitivity;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    [SerializeField] private Camera targetCamera;

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // Calculate zoom and clamp it
            float newSize = Mathf.Clamp(targetCamera.orthographicSize - scroll * getCurrentZoomSensitivity, minZoom, maxZoom);
            
            // Calculate position offset to keep the mouse position steady
            Vector3 mouseWorldPos = targetCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (targetCamera.transform.position - mouseWorldPos).normalized;
            float zoomDelta = targetCamera.orthographicSize - newSize;
            Vector3 newPosition = targetCamera.transform.position - direction * zoomDelta;

            // Apply new zoom and position
            targetCamera.orthographicSize = newSize;
            targetCamera.transform.position = new Vector3(newPosition.x, newPosition.y, targetCamera.transform.position.z);
        }

        // Camera movement
        targetCamera.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * targetCamera.orthographicSize / 30;
    }

}
