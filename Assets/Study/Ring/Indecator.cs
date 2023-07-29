using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Study.Ring
{
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
            if(Letter.Sellected != null)
            Letter.Sellected.ForEach(a => tt.text += a.GetComponentInChildren<TextMeshProUGUI>().text);
        }
    }
}