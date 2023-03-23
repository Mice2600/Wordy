using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

namespace EnhancedScrollerDemos.FlickSnap
{
    /// <summary>
    /// This demo shows how you can use your own snapping component in place of the built-in snapping.
    /// All of the snapping is done in the script FlickSnap.cs.
    /// </summary>
    public class DemoController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        private SmallList<Data> _data;
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab;
        public FlickSnap flickSnap;
        public float cellViewSize;
        void Start()
        {
            Application.targetFrameRate = 60;
            scroller.Delegate = this;
            LoadData();
        }
        private void LoadData()
        {
            _data = new SmallList<Data>();
            for (var i = 0; i < 3; i++)
                _data.Add(new Data() { someText = "Cell Data Index " + i.ToString() });
            flickSnap.MaxDataElements = _data.Count;
            scroller.ReloadData();
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return cellViewSize;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;
            cellView.name = "Cell " + dataIndex.ToString();

            cellView.SetData(_data[dataIndex]);

            return cellView;
        }

    }
}
