using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class OxygenUI : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();    
    }
    private void LateUpdate()
    {
        _text.text = (GameManager.Oxygen / GameManager.MaxOxygen).ToString();
    }
}
