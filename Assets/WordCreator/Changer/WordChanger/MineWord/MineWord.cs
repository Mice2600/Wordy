using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using Traonsletor;
using UnityEngine;
using UnityEngine.UI;

namespace WordCreator.WordCretor
{
    public class MineWord : MonoBehaviour, IApplyers
    {
        [SerializeField, Required]
        private TMP_InputField English;

        [SerializeField, Required]
        private Transform TranslatorsParent;

        
        [SerializeField, Required]
        private TMP_InputField MineTranslators;
        [SerializeField, Required]
        private List<TMP_InputField> Translators;
        [SerializeField, Required]
        private GameObject PerfabField;

        ContentObject contentObject => _contentObject ??= GetComponentInParent<ContentObject>();
        ContentObject _contentObject;

        void Start()
        {
            TranslatorsParent.ClearChilds();
            Translators = new List<TMP_InputField>();
            English.text = contentObject.Content.EnglishSource;
            English.onValueChanged.AddListener((T) => contentObject.Content.EnglishSource = T.ToUpper());
            /*if ((GetComponentInParent<ContentObject>().Content as IMultiTranslation).TranslationCount == 0) 
            {
                (GetComponentInParent<ContentObject>().Content as IMultiTranslation).AddTranslation("");
            }*/
            (contentObject.Content as IMultiTranslation).Translations.ForEach((Translation, i) => {
                if (i == 0) 
                {
                    MineTranslators.text = Translation;
                    AddToTranslationList(MineTranslators, Translation);   
                }
                else
                {
                    GameObject K = Instantiate(PerfabField, TranslatorsParent);
                    K.GetComponentInChildren<TMP_InputField>().text = Translation;
                    AddToTranslationList(K.GetComponentInChildren<TMP_InputField>(), Translation);
                    K.GetComponentInChildren<Button>().onClick.AddListener(() => OnRemoveButton(K));
                }
            });
        }
        public void OnRemoveButton(GameObject Parrent)
        {
            (contentObject.Content as IMultiTranslation).RemoveTranslationAt(Translators.IndexOf(Parrent.GetComponentInChildren<TMP_InputField>()));
            Translators.Remove(Parrent.GetComponentInChildren<TMP_InputField>());
            Destroy(Parrent);
        }
        public void OnAddButton()
        {
            GameObject K = Instantiate(PerfabField, TranslatorsParent);
            AddToTranslationList(K.GetComponentInChildren<TMP_InputField>(), "Other One");
            K.GetComponentInChildren<Button>().onClick.AddListener(() => OnRemoveButton(K));
            (contentObject.Content as IMultiTranslation).AddTranslation("Other One");
            transform.root.gameObject.GetComponentsInChildren<VerticalLayoutGroup>().ToList().ForEach(i => { i.spacing += Random.Range(-.5f, .5f); } );
            
        }
        public void OnChange(TMP_InputField Transttion) 
        {
            if (contentObject.Content.RussianSource == "") 
                contentObject.Content.RussianSource = Transttion.text.ToUpper();
            (contentObject.Content as IMultiTranslation).ChangeTranslationAt(Translators.IndexOf(Transttion), Transttion.text);
        }
        public void OnTranllateButton(string TronslateType)
        {
            if (TronslateType != "ru" && TronslateType != "uz") TronslateType = "ru";
            StartCoroutine(Translator.Process("en", TronslateType, contentObject.Content.EnglishSource, onWordTransleted));
            void onWordTransleted(string tt)
            {
                Debug.Log(tt);
                MineTranslators.text = tt;
                OnChange(MineTranslators);

            }
        }
        private void AddToTranslationList(TMP_InputField Item, string DefaultText) 
        {
            Item.text = DefaultText;
            TMP_InputField DD = Item;
            Item.onValueChanged.AddListener((T) => OnChange(DD));
            Translators.Add(Item);
        }

        public void TryApply(Content content) 
        {
            content.EnglishSource = English.text;
            content.RussianSource = MineTranslators.text;
        }

    }
}