using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Range(1.0f,100.0f)]
    public float Sensitivity = 50;
    public Transform PlayerBody;

    private float m_XRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime ;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime ;

        m_XRotation -= mouseY;
        m_XRotation = Mathf.Clamp(m_XRotation, -90.0f, 45.0f);

        transform.localRotation = Quaternion.Euler(m_XRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
