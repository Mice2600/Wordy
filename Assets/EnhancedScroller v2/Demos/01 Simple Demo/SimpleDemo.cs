using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections.Generic;
using Base.Dialog;
using Base;
using Servises.SmartText;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
    public class SimpleDemo : MonoBehaviour, IEnhancedScrollerDelegate
    {
        private List<Content> _data;

        public EnhancedScroller scroller;

        public EnhancedScrollerCellView cellViewPrefab;

        void Start()
        {
            Application.targetFrameRate = 60;

            scroller.Delegate = this;

            LoadLargeData();
        }

        private void LoadLargeData()
        {
            _data = new List<Content>(DialogBase.Dialogs);
            scroller.ReloadData();
        }

        
        
        public int GetNumberOfCells(EnhancedScroller scroller)=>_data.Count;

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => (cellViewPrefab as CellView).NText.GetPreferredValues(_data[dataIndex].EnglishSource, Screen.width, 100).y + 10f;

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;
            cellView.SetData(_data[dataIndex]);
            return cellView;
        }

    }
}
