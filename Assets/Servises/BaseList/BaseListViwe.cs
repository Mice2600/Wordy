using Base;
using Base.Dialog;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
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
        public virtual float GetSizeOfContent(Content content) => (cellViewPrefab).NText.GetPreferredValues(content.EnglishSource, scroller.ScrollRect.content.rect.width, 100).y + 20f;
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;
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