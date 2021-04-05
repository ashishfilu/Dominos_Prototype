using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyUp(KeyCode.Keypad1))
        {
            GameEventManager.Instance.TriggerEvent(EventId.KeyPad_1_Pressed, EventId.KeyPad_1_Pressed);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            GameEventManager.Instance.TriggerEvent(EventId.KeyPad_2_Pressed, EventId.KeyPad_2_Pressed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEventManager.Instance.TriggerEvent(EventId.Spacebar_Pressed, EventId.Spacebar_Pressed);
        }
    }

}

public partial class EventId
{
    public static string KeyPad_1_Pressed= "GreenButtonPressed";
    public static string KeyPad_2_Pressed = "RedButtonPressed";
    public static string Spacebar_Pressed = "BallRelease";
}
