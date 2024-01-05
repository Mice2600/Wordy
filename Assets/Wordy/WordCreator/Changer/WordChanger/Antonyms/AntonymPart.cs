using Base.Word;
using Base;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
using UnityEngine.UI;
using Base.Synonym;
using Base.Antonym;

namespace WordCreator.WordCretor
{
    public class AntonymPart : MonoBehaviour, IApplyers
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
            if (dd != null) dd.OnEnglishValueChanged += (a) => ManualUpdate();
            ManualUpdate();
        }

        public void ManualUpdate()
        {
            TranslatorsParent.ClearChilds();

            if (AntonymBase.Antonyms.Contains(contentObject.Content))
            {
                AntonymBase.Antonyms[contentObject.Content].attachments.ForEach((Atach) =>
                {
                    Instantiate(PerfabField, TranslatorsParent).GetComponent<AntonymField>().Set(Atach);
                });
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }
        public void OnAddButton()
        {
            Instantiate(PerfabField, TranslatorsParent).GetComponent<AntonymField>().Set("Other One");
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        }

        public void TryApply(Content content)
        {
            List<string> strings = new List<string>();
            GetComponentsInChildren<AntonymField>().ToList().ForEach(x => { strings.Add(x.Text.ToUpper()); });
            if (WordBase.Wordgs.Contains(content))
            {
                AntonymBase.RemoveAntonym(content.EnglishSource);
            }
            AntonymBase.AddAntonym(content, strings);
        }
    }
}