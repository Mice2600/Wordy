using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Study.TypeShift
{
    public class Indecator : MonoBehaviour
    {
        private TextMeshProUGUI MText => _MText ??= GetComponent<TextMeshProUGUI>();
        private TextMeshProUGUI _MText;
        private TypeShift MTypeShift => _TypeShift ??= FindObjectOfType<TypeShift>();
        private TypeShift _TypeShift;
        public void Update()
        {
            MText.text = $"Guss {MTypeShift.UsingContent.Count - MTypeShift.FoundedContent.Count} words \n";
            for (int i = 0; i < MTypeShift.FoundedContent.Count; i++)
                MText.text += TextUtulity.SenterLine(MTypeShift.FoundedContent[i].EnglishSource) + "\n";
        }

    }
}