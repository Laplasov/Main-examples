using UnityEngine;
using UnityEngine.UIElements;

public class CameraSnap : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;
    [SerializeField]
    private float offset;
    private Vector3 offsetCamera;

    private void Awake()
    {
        offsetCamera = new Vector3(0, 0, offset);
    }
    private void Update()
    {
        m_Camera.transform.position = new Vector3(transform.position.x, m_Camera.transform.position.y, transform.position.z) + offsetCamera;
        if (Input.GetKeyDown(KeyCode.E))
            RotateCamera(-90f);
        if (Input.GetKeyDown(KeyCode.Q))
            RotateCamera(90f);

    }
    private void RotateCamera(float turn)
    {
        m_Camera.transform.RotateAround(transform.position, Vector3.up, turn);
        UpdateOffsetCamera();

    }

    private void UpdateOffsetCamera()
    {
        Vector3 eulerAngles = m_Camera.transform.rotation.eulerAngles;

        if (Mathf.Approximately(eulerAngles.y, 0f) || Mathf.Approximately(eulerAngles.y, 360f))
        {
            offsetCamera = new Vector3(0, offsetCamera.y, offset);
        }
        else if (Mathf.Approximately(eulerAngles.y, 90f))
        {
            offsetCamera = new Vector3(offset, offsetCamera.y, 0);
        }
        else if (Mathf.Approximately(eulerAngles.y, 180f))
        {
            offsetCamera = new Vector3(0, offsetCamera.y, -offset);
        }
        else if (Mathf.Approximately(eulerAngles.y, 270f))
        {
            offsetCamera = new Vector3(-offset, offsetCamera.y, 0);
        }
    }
}