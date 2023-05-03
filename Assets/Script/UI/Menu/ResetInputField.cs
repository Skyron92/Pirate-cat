using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetInputField : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;

    private void Awake() {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = "";
    }
}
