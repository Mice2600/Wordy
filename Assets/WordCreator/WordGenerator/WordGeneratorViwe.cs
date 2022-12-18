using Base;
using Base.Word;
using Servises;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using Traonsletor;
using UnityEngine;
namespace WordCreator.WordGenerator
{
    public class WordGeneratorViwe : BaseListViwe<Word>
    {
        public override List<Word> Contents => WordsGenereted;
        private protected override int IndexOf(IContent content) => Contents.IndexOf((content as Word?).Value);
        public List<Word> WordsGenereted;
        [SerializeField]
        private GameObject LodingObject;

        private protected override void Start()
        {
            LodingObject.SetActive(true);
            Translator.GetRandomWord(Resulrat);
            contentPattent.ClearChilds();
            void Resulrat(List<Word> words)
            {
                WordsGenereted = words;
                Lode(0);
                contentPattent.Childs().ForEach(child =>
                {
                    AddButton[] UIButtons = child.GetComponentsInChildren<AddButton>();
                    GameObject DD = child.gameObject;
                    for (int i = 0; i < UIButtons.Length; i++) UIButtons[i].onClick.AddListener(() => TryAdd(DD));
                });
                LodingObject.SetActive(false);
            }

        }

        public void TryAdd(GameObject Content)
        {
            WordBase.Wordgs.Add((Content.GetComponent<ContentObject>().Content as Word?).Value);
        }

    }
}