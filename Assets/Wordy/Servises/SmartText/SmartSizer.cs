using Sirenix.OdinInspector;
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
            Vector3 Min = Vector2.one;
            Vector3 Max = -Vector2.one;


            for (int i = 0; i < NText.textInfo.characterInfo.Length; i++)
            {
                if (char.IsLetterOrDigit(NText.textInfo.characterInfo[i].character) || char.IsSeparator(NText.textInfo.characterInfo[i].character) || char.IsSymbol(NText.textInfo.characterInfo[i].character))
                {

                    var Y = NText.textInfo.characterInfo[i].topRight;
                    var Y2 = NText.textInfo.characterInfo[i].topLeft;


                    Max = Vector2.Max(Max, Y);
                    Max = Vector2.Max(Max, Y2);
                    Min = Vector2.Min(Min, Y);
                    Min = Vector2.Min(Min, Y2);


                    var X = NText.textInfo.characterInfo[i].bottomLeft;
                    var X2 = NText.textInfo.characterInfo[i].bottomRight;

                    Max = Vector2.Max(Max, X);
                    Max = Vector2.Max(Max, X2);
                    Min = Vector2.Min(Min, X);
                    Min = Vector2.Min(Min, X2);

                }
            }
            
            //Min = NText.transform.position + ((-NText.transform.right * Min.x) + (-NText.transform.up * Min.y));
            //Max = NText.transform.position + (NText.transform.right * Max.x + (NText.transform.up * Max.y));

            Max.y += Padding.top;
            Min.y -= Padding.bottom;

            Max.x += Padding.right;
            Min.x -= Padding.left;



            var maxAnchorOld = (transform as RectTransform).anchorMax;
            var mminAnchorOld = (transform as RectTransform).anchorMin;
            (transform as RectTransform).anchorMax = Vector2.one / 2f;
            (transform as RectTransform).anchorMin = Vector2.one / 2f;

            var OldTextTransform = NText.transform.position;
            (transform as RectTransform).offsetMax = Max;
            (transform as RectTransform).offsetMin = Min;
            NText.transform.position = OldTextTransform;
            var OldLPos = (transform as RectTransform).localPosition;
            (transform as RectTransform).position = NText.transform.position + OldLPos;
            NText.transform.position = OldTextTransform;

            maxAnchorOld = (transform as RectTransform).anchorMax = maxAnchorOld;
            mminAnchorOld = (transform as RectTransform).anchorMin = mminAnchorOld;

        }
    }
}


