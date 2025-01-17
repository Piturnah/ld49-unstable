using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyButton : MonoBehaviour
{
    char key;
    public GameController gc;

    Animator anim;
    public TextMeshProUGUI text;

    public Color defaultColour;
    public Color depressedColour;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void UpdateKeyValue(char newVal) {
        key = newVal;
        text.text = ("" + key).ToUpper();
    }

    private void Update() {
        try {
            anim.SetBool("Wobbling", !Input.GetKey(KeyMap.charToKeycode[key]));
        } catch {
            Debug.LogWarning(key);
        }
    }
}
