using Servises.SmartText;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;

namespace Servises
{
    public interface RePlaceController
    {
        public bool TrayUp();
        public bool TrayDown();
    }

    public class RePlaceContents : MonoBehaviour
    {
        private ScrollRect scrollRect => _scrollRect ??= transform.GetComponentInParent<ScrollRect>();
        private ScrollRect _scrollRect;
        private RePlaceController wordBaseViwe => _wordBaseViwe ??= transform.GetComponentInParent<RePlaceController>();
        private RePlaceController _wordBaseViwe;
        private void OnTransformChildrenChanged()
        {
            scrollRect.content.Childs().ForEach((a) => { if (a.TryGetComponent(out SmartSizer daa) && daa.OnSizeChanged != OnSizeChanged) { daa.OnSizeChanged += OnSizeChanged; } });
        }
        public VerticalLayoutGroup VerticalLayoutGroup;
        public float Dddd;
        private void Update()
        {
            Dddd = (int)(scrollRect.verticalNormalizedPosition * 100f);
            if (Input.GetMouseButton(0)) return;
            if (scrollRect.content.childCount < 20) return;
            if (Dddd > 70)
            {
                for (int i = 0; i < 1; i++)
                {
                    if (!wordBaseViwe.TrayUp()) break;
                    scrollRect.content.GetChild(scrollRect.content.childCount - 1).SetAsFirstSibling();
                    scrollRect.content.position += new Vector3(0, scrollRect.content.GetChild(0).GetComponent<RectTransform>().rect.height + (VerticalLayoutGroup.spacing), 0);
                    
                }
            }
            if (Dddd < 30)
            {
                for (int i = 0; i < 1; i++)
                {
                    if (!wordBaseViwe.TrayDown()) break;
                    scrollRect.content.GetChild(0).SetAsLastSibling();
                    scrollRect.content.position -= new Vector3(0, scrollRect.content.GetChild(scrollRect.content.childCount -1).GetComponent<RectTransform>().rect.height + (VerticalLayoutGroup.spacing), 0);
                }
            }
        }
        public void OnSizeChanged((RectTransform rectTransform, Vector2 OlldSize) Value)
        {
            if (Value.rectTransform.transform.GetSiblingIndex() < 4)
            {
                float result = Value.rectTransform.rect.height - Value.OlldSize.y;   
                scrollRect.content.position += new Vector3(0, result, 0);
            }
        }

    }
}