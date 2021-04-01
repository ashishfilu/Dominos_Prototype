using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DominoType
{
    Red,
    Green
};

public class GameRoot : MonoBehaviour
{
    public GameObject DominosRoot;

    private RaycastHit mHitObject;
    private DominoType mDominoType = DominoType.Green;
    private GameObject mGreenDomino;
    private GameObject mRedDomino;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.Instance.SubscribeEventListener(EventId.GreenButtonPressed, ProcessButtonPress);
        GameEventManager.Instance.SubscribeEventListener(EventId.RedButtonPressed, ProcessButtonPress);

        mGreenDomino = Resources.Load<GameObject>("Prefabs/DominoCube_Green");
        mRedDomino = Resources.Load<GameObject>("Prefabs/DominoCube_Red");
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
                rigidbody?.AddForce(ray.direction * 12, ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out mHitObject))
            {
                if( mHitObject.transform.gameObject.CompareTag("Ground"))
                {
                    GameObject temp = null;
                    if ( mDominoType == DominoType.Green )
                    {
                        temp = GameObject.Instantiate(mGreenDomino);
                    }
                    else
                    {
                        temp = GameObject.Instantiate(mRedDomino);
                    }

                    temp.transform.parent = DominosRoot.transform;
                    temp.transform.position = mHitObject.point;
                }
            }
        }
    }

    void ProcessButtonPress(object data)
    {
        if(data.ToString() == EventId.GreenButtonPressed )
        {
            mDominoType = DominoType.Green;
        }
        else if( data.ToString() == EventId.RedButtonPressed )
        {
            mDominoType = DominoType.Red;
        }
    }
}
