using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Indecator : MonoBehaviour
{

    TextMeshProUGUI tt;
    private void Start()
    {
        tt = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        tt.text = "";
        Letter.Sellecting.ForEach(a => tt.text += a.GetComponentInChildren<TextMeshProUGUI>().text);   
    }
}
