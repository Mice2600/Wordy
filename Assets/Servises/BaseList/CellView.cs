using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using Base;
using TMPro;
using SystemBox;
using System.Collections.Generic;
using System.Linq;

public class CellView : EnhancedScrollerCellView
    {
        private protected ContentObject ContentObject => _ContentObject ??= GetComponentInChildren<ContentObject>();
        private protected ContentObject _ContentObject;
        public TMP_Text NText;
        protected List<IQueueUpdate> queueUpdates => _queueUpdates ??= GetComponentsInChildren<IQueueUpdate>(true).ToList();
        protected List<IQueueUpdate> _queueUpdates;
        public virtual void SetData(Content data)
        {
            ContentObject.Content = data;
            queueUpdates.ForEach(action => action.TurnUpdate());
        }
    }