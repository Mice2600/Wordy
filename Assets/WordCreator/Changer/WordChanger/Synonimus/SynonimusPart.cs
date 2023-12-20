using Base.Word;
using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using Traonsletor;
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
        [Button]
        public void Start()
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
        }
        public void OnAddButton()
        {
            Instantiate(PerfabField, TranslatorsParent).GetComponent<SynonymField>().Set("Other One");
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        
        public void TryApply(Content content) 
        {
            List<string> strings = new List<string>();
            GetComponentsInChildren<SynonymField>().ToList().ForEach(x => {strings.Add(x.Text);});
            SynonymBase.AddSynonym(content, strings);
        }
    }
}