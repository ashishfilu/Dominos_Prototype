using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Range(1.0f,100.0f)]
    public float MovementSpeed;
    [Range(1.0f, 100.0f)]
    public float RotationSpeed;

    private Vector2 mPreviousMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        mPreviousMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = gameObject.transform;
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;
        Vector3 position = transform.position;

        if(Input.GetKey(KeyCode.W))
        {
            position += forward * Time.deltaTime * MovementSpeed;
        }
        else if( Input.GetKey(KeyCode.S))
        {
            position -= forward * Time.deltaTime * MovementSpeed;
        }
        else if( Input.GetKey(KeyCode.D))
        {
            position += right * Time.deltaTime * MovementSpeed;
        }
        else if( Input.GetKey(KeyCode.A))
        {
            position -= right * Time.deltaTime * MovementSpeed;
        }

        if(Input.GetMouseButton(0))
        {
            Vector2 currentPosition = Input.mousePosition;
            Vector2 delta = currentPosition- mPreviousMousePosition  ;
            forward = Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, transform.up * (delta.x/Mathf.Abs(delta.x))) * forward;
            forward = Quaternion.AngleAxis(-RotationSpeed * Time.deltaTime, transform.right * (delta.y / Mathf.Abs(delta.y))) * forward;
            transform.forward = forward;
            mPreviousMousePosition = currentPosition;
        }

        transform.position = position;
    }
}
