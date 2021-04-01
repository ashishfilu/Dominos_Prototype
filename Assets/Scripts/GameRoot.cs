using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private RaycastHit mHitObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast(ray,out mHitObject))
            {
                Rigidbody rigidbody = mHitObject.rigidbody;
                rigidbody.AddForce(ray.direction * 12, ForceMode.Impulse);
            }
        }
    }
}
