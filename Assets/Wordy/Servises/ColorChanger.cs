using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
namespace Servises
{
    public class ColorChanger : OptimizedBehaver
    {
        public List<Image> Images;
        public List<TMP_Text> Texts;
        public void SetColor(Color color)
        {
            for (int i = 0; i < Images.Count; i++)
                if (Images[i] != null) Images[i].color = color;
            for (int i = 0; i < Texts.Count; i++)
                if (Texts[i] != null) Texts[i].color = color;
        }


    }
}