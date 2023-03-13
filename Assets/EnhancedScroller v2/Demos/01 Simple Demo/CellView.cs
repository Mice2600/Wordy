using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using Base;
using TMPro;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
    /// <summary>
    /// This is the view of our cell which handles how the cell looks.
    /// </summary>
    public class CellView : EnhancedScrollerCellView
    {
        private ContentObject ContentObject => _ContentObject ??= GetComponent<ContentObject>();
        private ContentObject _ContentObject;

        public TMP_Text NText;
        public float GetSize() => NText.GetPreferredValues().y;
        public void SetData(Content data)
        {
            ContentObject.Content = data;
        }
    }
}