using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Servises.SmartText
{
    public class SmartSizer : MonoBehaviour
    {
        public RectOffset Padding;
        
        [Required] public TMP_Text NText;
        
        private void LateUpdate()
        {
            FixtSize();
        }
        [Button]
        private protected void FixtSize()
        {


            
            #region Conculate Size and Pos


            Vector2 Min = Vector2.zero;

            Vector2 Max = Vector2.zero;

            if (!string.IsNullOrWhiteSpace(NText.text) && !string.IsNullOrEmpty(NText.text))
            {

                 Min = new
                (NText.textInfo.characterInfo[0].topLeft.x,
                NText.textInfo.characterInfo[NText.textInfo.characterInfo.Length - 1].bottomRight.y);

                 Max = new
                    (NText.textInfo.characterInfo[NText.textInfo.characterInfo.Length - 1].bottomRight.x,
                    NText.textInfo.characterInfo[0].topLeft.y);
                for (int i = 0; i < NText.textInfo.lineCount; i++)
                {
                    int NIndex = NText.textInfo.lineInfo[i].lastVisibleCharacterIndex;
                    var NMaxX = NText.textInfo.characterInfo[NIndex].bottomLeft.x;
                    if (NMaxX > Max.x) Max.x = NMaxX;
                }
                if (NText.textInfo.lineCount > 0) 
                {
                    int firstCharacterIndex = NText.textInfo.lineInfo[NText.textInfo.lineCount - 1].firstCharacterIndex;
                    for (int i = firstCharacterIndex; i < NText.textInfo.characterCount; i++)
                    {
                        var NMinY = NText.textInfo.characterInfo[i].bottomLeft.y;
                        if (NMinY > Min.y) Min.y = NMinY;
                    }
                }
                
                




                Vector2 NeedResalution = NText.GetRenderedValues(true);


                (transform as RectTransform).anchorMax = (NText.transform as RectTransform).anchorMax;
                (transform as RectTransform).anchorMin = (NText.transform as RectTransform).anchorMin;

                Vector2 CenterArchor =
                    ((transform as RectTransform).anchorMax + (transform as RectTransform).anchorMin) / 2;
                (transform as RectTransform).anchorMax = CenterArchor;
                (transform as RectTransform).anchorMin = CenterArchor;

                //(transform as RectTransform).anchoredPosition = (NText.transform as RectTransform).anchoredPosition;
                Max += (NText.transform as RectTransform).anchoredPosition;
                Min += (NText.transform as RectTransform).anchoredPosition;


            }

            #endregion


            Max.y += Padding.top;
            Min.y -= Padding.bottom;

            Max.x += Padding.right;
            Min.x -= Padding.left;


            (transform as RectTransform).offsetMax = Max;
            (transform as RectTransform).offsetMin = Min;
        }
    }
}