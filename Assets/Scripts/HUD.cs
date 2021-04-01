using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Button GreenButton;
    public Button RedButton;

    // Start is called before the first frame update
    void Start()
    {
        GreenButton.onClick.AddListener(OnGreenButtonClicked);
        RedButton.onClick.AddListener(OnRedButtonClicked);
    }

    void OnGreenButtonClicked()
    {
        GameEventManager.Instance.TriggerEvent(EventId.GreenButtonPressed , EventId.GreenButtonPressed);
    }

    void OnRedButtonClicked()
    {
        GameEventManager.Instance.TriggerEvent(EventId.RedButtonPressed,EventId.RedButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public partial class EventId
{
    public static string GreenButtonPressed = "GreenButtonPressed";
    public static string RedButtonPressed = "RedButtonPressed";
}
