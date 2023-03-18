using Base;
using Base.Dialog;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Servises.BaseList
{
    public abstract class BaseListViwe : MonoBehaviour, IRemoveButtonUser, IEnhancedScrollerDelegate
    {
        
        [Required]
        public EnhancedScroller scroller;
        [Required]
        public CellView cellViewPrefab;
        public abstract List<Content> Contents { get; }
        private protected virtual int IndexOf(Content content) => Contents.IndexOf(content);
        private protected virtual void Start()
        {
            Lode(0);
        }

        public int GetNumberOfCells(EnhancedScroller scroller) => Contents.Count;

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => GetSizeOfContent(Contents[dataIndex]);


        private static Dictionary<GameObject, (TMP_Text T, float H)> SavedObjectTexts = new Dictionary<GameObject, (TMP_Text T, float H)>();
        public virtual float GetSizeOfContent(Content content) 
        {
            if (!SavedObjectTexts.ContainsKey(content.ContentObject)) 
            {
                TMP_Text t = content.ContentObject.GetComponent<CellView>().NText;
                if(t != null) SavedObjectTexts.Add(content.ContentObject, (t, 0));
                else SavedObjectTexts.Add(content.ContentObject, (null, content.ContentObject.GetComponent<RectTransform>().rect.height));
            }
            if (SavedObjectTexts[content.ContentObject].T !=null)
                return SavedObjectTexts[content.ContentObject].T.GetPreferredValues(content.EnglishSource, scroller.ScrollRect.content.rect.width, 100).y + 20f;
            return SavedObjectTexts[content.ContentObject].H;
        } 
            
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellView cellView = scroller.GetCellView(Contents[dataIndex].ContentObject.GetComponent<EnhancedScrollerCellView>()) as CellView;
            cellView.SetData(Contents[dataIndex]);
            return cellView;
        }

        public void GoToIndex(Content content) 
        {
            scroller.SetScrollPositionImmediately(scroller.GetScrollPositionForDataIndex(Contents.IndexOf(content), EnhancedScroller.CellViewPositionEnum.Before));
        }
        public virtual void Lode(int From)
        {
            Application.targetFrameRate = 60;
            scroller.Delegate = this;
            scroller.ReloadData(0);
            if(From > 0) scroller.SetScrollPositionImmediately(scroller.GetScrollPositionForDataIndex(From, EnhancedScroller.CellViewPositionEnum.Before));
        }
        public virtual void Refresh()
        {
            Application.targetFrameRate = 60;
            scroller.ReloadData(scroller.NormalizedScrollPosition);
            
        }
    
        public void OnRemoveButton(Content content)
        {
            content.BaseCommander.Remove(content);
            FindObjectOfType<DiscretionObject>()?.DestroyUrself();
            Refresh();
        }

    }
}