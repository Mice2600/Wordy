using EnhancedScrollerDemos.FlickSnap;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Servises.BaseList;
using System.Linq;
using SystemBox;
using Newtonsoft.Json.Linq;
using UnityEngine.UIElements;

namespace BaseViwe
{
    [RequireComponent(typeof(EnhancedScroller))]
    public class BaseViwe : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [SerializeField]
        private List<EnhancedScrollerCellView> PrefabContents;
        private EnhancedScroller scroller => _scroller ??= GetComponent<EnhancedScroller>();
        private EnhancedScroller _scroller;
        private FlickSnap flickSnap => _flickSnap ??= GetComponent<FlickSnap>();
        private FlickSnap _flickSnap;

        void Start()
        {
            Application.targetFrameRate = 60;
            scroller.Delegate = this;
            flickSnap.OnNewViweOpend += OnNewViweOpend;
            flickSnap.MaxDataElements = PrefabContents.Count;
            scroller.ReloadData();
        }

        private void OnNewViweOpend(GameObject Corrent)
        {
            new List<IBaseToolItem>(FindObjectsOfType<MonoBehaviour>(true).OfType<IBaseToolItem>()).ForEach((a) => a.OnNewViweOpend(Corrent));
        }

        public int GetNumberOfCells(EnhancedScroller scroller) => PrefabContents.Count;


        private float? cellViewSize;
        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) => (cellViewSize ??= scroller.GetComponent<RectTransform>().rect.width/* - (scroller.padding.left + scroller.padding.right)*/);

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            CellView cellView = scroller.GetCellView(PrefabContents[dataIndex]) as CellView;
            cellView.name = "Cell " + dataIndex.ToString();
            //cellView.SetData(PrefabContents[dataIndex]);
            return cellView;
        }

    }
}