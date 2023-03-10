using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Servises.SmartText
{
    public class SmartSizer : MonoBehaviour
    {
        public System.Action<(RectTransform rectTransform, Vector2 OlldSize)> OnSizeChanged;
        [SerializeField]
        private bool ChangeX = true, ChangeY = true;
        [SerializeField]
        private float OfsetSize = 2;
        [Required]
        public TMP_Text NText;
        private RectTransform rectTransform => _rectTransform ??= GetComponent<RectTransform>();
        private RectTransform _rectTransform;
        public virtual void Update()
        {
            FixtSize();
        }

        private protected void FixtSize()
        {
            Vector2 OlldSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            Vector3 oldpos = transform.position;
            Vector2 ds = NText.GetRenderedValues(true);
            
            if (ds.y < 0) ds = new Vector2(0,0);
            ds *= 1.15f;
            Vector2 ResoltOfsetSize = ds / OfsetSize;
            if (!ChangeX) ResoltOfsetSize.x = rectTransform.offsetMax.x;
            if (!ChangeY) ResoltOfsetSize.y = rectTransform.offsetMax.y;
            rectTransform.offsetMax = ResoltOfsetSize;

            Vector2 ResoltoffsetMin = -ds / OfsetSize;
            if (!ChangeX) ResoltoffsetMin.x = rectTransform.offsetMin.x;
            if (!ChangeY) ResoltoffsetMin.y = rectTransform.offsetMin.y;
            rectTransform.offsetMin = ResoltoffsetMin;

            transform.position = oldpos;

            Vector2 NewdSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            if (NewdSize != OlldSize) OnSizeChanged?.Invoke((rectTransform, OlldSize));
        }
    }
}