using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;    //Creates a TMPro text element
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateText(string promptMessage)
    {
            promptText.text = promptMessage;    //Displays the message on the Screen
    }
}

