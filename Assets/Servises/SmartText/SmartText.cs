using UnityEngine; 
using Sirenix.OdinInspector;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Servises.BaseList;
using System;

namespace Servises.SmartText
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class SmartText : OptimizedBehaver, IQueueUpdate
    {
        private ISearchList SearChSystem;
        private protected override void Start()
        {
            base.Start();
            List<ISearchList> Lis = new List<ISearchList>(GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISearchList>());
            if (Lis.Count > 0) SearChSystem = Lis[0];
        }
        public abstract string MyText { get; }
        public virtual string MargeText => BiforText + MyText + AfterTextText;
        [HideLabel]
        [HorizontalGroup("t")]
        public string BiforText, AfterTextText;
        private protected TMP_Text textMesh => _textMesh ??= GetComponent<TMP_Text>();
        private protected TMP_Text _textMesh;

        public virtual void TurnUpdate() 
        {
            
            string DS = MargeText;
            if (SearChSystem != null) 
            {
                if (!string.IsNullOrEmpty(SearChSystem.SearchingString)) 
                {
                    if (!string.IsNullOrWhiteSpace(SearChSystem.SearchingString))
                    {
                        if (DS.Contains(SearChSystem.SearchingString, StringComparison.OrdinalIgnoreCase)) 
                        {
                            DS = DS.Replace(SearChSystem.SearchingString,
                                TextUtulity.UnderLine(TextUtulity.Color(SearChSystem.SearchingString, textMesh.color * 1.6f)));
                            DS = TextUtulity.Color(DS, Color.Lerp(textMesh.color, Color.black, .2f));
                        }
                    }
                }
            }
            textMesh.text = DS;
        } 
        

    }
}