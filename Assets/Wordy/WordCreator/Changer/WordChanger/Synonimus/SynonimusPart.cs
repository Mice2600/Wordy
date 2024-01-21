using Base.Word;
using Base;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
using Base.Synonym;

namespace WordCreator.WordCretor
{
    public class SynonimusPart : MonoBehaviour, IApplyers
    {
        [SerializeField, Required]
        private Transform TranslatorsParent;

        [SerializeField, Required]
        private GameObject PerfabField;
        ContentObject contentObject => _contentObject ??= GetComponentInParent<ContentObject>();
        ContentObject _contentObject;
        public void Start() 
        {
            MineWord dd = transform.root.GetComponentInChildren<MineWord>();
            if(dd != null) dd.OnEnglishValueChanged += (a) => ManualUpdate();
            ManualUpdate();
        }
        
        public void ManualUpdate()
        {
            TranslatorsParent.ClearChilds();
            
            if (SynonymBase.Synonyms.Contains(contentObject.Content)) 
            {
                SynonymBase.Synonyms[contentObject.Content].attachments.ForEach((Atach)=>
                {
                    Instantiate(PerfabField, TranslatorsParent).GetComponent<SynonymField>().Set(Atach);
                });
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
            LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);
        }
        public void OnAIFixButton() 
        {
            GPTAI.GenerateSinonim(contentObject.Content.EnglishSource, OnAiResponded);
            void OnAiResponded(List<string> Words) 
            {
                TranslatorsParent.ClearChilds();
                Words.ForEach(WI => {
                    Instantiate(PerfabField, TranslatorsParent).GetComponent<SynonymField>().Set(WI.ToUpper());
                });
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                LayoutRebuilder.MarkLayoutForRebuild(transform.parent as RectTransform);
            }
        }
        public void OnAddButton()
        {
            Instantiate(PerfabField, TranslatorsParent).GetComponent<SynonymField>().Set("Other One");
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        
        public void TryApply(Content content) 
        {
            List<string> strings = new List<string>();
            GetComponentsInChildren<SynonymField>().ToList().ForEach(x => {strings.Add(x.Text.ToUpper());});
            if (WordBase.Wordgs.Contains(content)) 
            {
                SynonymBase.RemoveSynonym(content.EnglishSource);
            }
            SynonymBase.AddSynonym(content, strings);
        }
    }
}