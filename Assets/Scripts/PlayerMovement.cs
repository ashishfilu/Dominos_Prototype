using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    private CharacterController m_CharaterController;

    private void Start()
    {
        m_CharaterController = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;
        m_CharaterController.Move(motion * Speed * Time.deltaTime);
    }
}
