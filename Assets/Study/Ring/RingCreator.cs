using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;

namespace Study.Ring
{
    public class RingCreator : MonoBehaviour
    {


        public System.Action<Content> OncontentFound;


        public GameObject WordPrefab;
        public List<string> Used;
        [System.NonSerialized]
        public List<Content> Contents;
        [SerializeField, Required]
        private Transform RingParrent;
        [SerializeField, Required]
        private GameObject Win;
        [SerializeField, Required]
        private GameObject ContentPrefab;

        [SerializeField, Required]
        private ContentGropper ContentParrent;
        private void Start()
        {
            FindWords();
        }


        public bool CheakWord(string W) 
        {
            
            if (Used.Contains(W)) 
            {
                OncontentFound?.Invoke(WordBase.Wordgs.GetContent(W));
                
                Used.Remove(W);
                if (Used.IsEnpty()) 
                {
                    StartCoroutine(Ww());
                    IEnumerator Ww() 
                    {
                        yield return new WaitForSeconds(2);
                        Instantiate(Win).GetComponent<WinViwe>().contents = Contents;
                        QuestRing d = GetComponent<QuestRing>();
                        Contents.ForEach(ss => d.OnWordWin?.Invoke(ss as Word));
                        d.OnGameWin?.Invoke();
                    }
                    
                }
                return true;
            }
            return false;
        }

        public void FindWords()
        {

            TList<Word> contents = WordBase.Wordgs.GetContnetList(30);
            List<Word> Fixxed = new List<Word>();
            contents.ForEach(w =>
            {

                bool IsWrong = false;
                for (int i = 1; i < w.EnglishSource.Length - 1; i++)
                {
                    if (w.EnglishSource[i] == w.EnglishSource[i - 1] || w.EnglishSource[i] == w.EnglishSource[i + 1])
                    {
                        IsWrong = true;
                        break;
                    }
                }
                if (!IsWrong) Fixxed.Add(w);
            });
            contents = new List<Word>(Fixxed);
            Word Starter = contents.RemoveRandomItem();

            TList<string> UsingContents = new TList<string>(contents.RemoveRandomItem().EnglishSource);
            TList<string> Others = new TList<string>(contents.Select(W => W.EnglishSource));

            while (FindBestOptin(UsingContents, Others, out string Option))
            {
                UsingContents.Add(Option);
                Others.Remove(Option);

            }
            Used = UsingContents;
            Contents = Used.Select(W => WordBase.Wordgs.GetContent(W)).ToList();

            Contents.ForEach(
                C =>
                {
                    GameObject d = Instantiate(ContentPrefab, transform);
                    d.GetComponentInChildren<ContentObject>().Content = C;
                    ContentParrent.AddNewContent(d.transform);
                }
                );

            bool FindBestOptin(List<string> Sellected, List<string> OnQuawe, out string Option)
            {
                Option = "";
                TList<char> chars = new TList<char>();
                Sellected.ForEach(w => { w.Length.For(L => chars.AddIfDirty(w[L])); });
                if (chars.Count > 8) return false;


                int SmollestOne = 999;
                string Item = "";
                OnQuawe.ForEach(w =>
                {
                    int CC = 0;
                    w.Length.For(L => { if (!chars.Contains(w[L])) CC++; });
                    if (CC < SmollestOne) { SmollestOne = CC; Item = w; }
                });
                if (chars.Count + SmollestOne > 8) return false;
                Option = Item;
                return true;
            }

            Load(UsingContents);
        }
        public void Load(TList<string> Words)
        {
            RingParrent.ClearChilds();
            TList<char> NeedChars = new TList<char>();
            Words.ForEach(w =>
            {
                w.ToUpper();
                for (int i = 0; i < w.Length; i++)
                    if (!NeedChars.IsIndex(w[i]))
                        NeedChars += w[i];
            });
            TList<GameObject> CreatedWords = new TList<GameObject>();
            NeedChars.ForEach(w =>
            {
                CreatedWords += Instantiate(WordPrefab, RingParrent);
                CreatedWords.Last.GetComponentInChildren<TextMeshProUGUI>().text = w.ToString();
            });


            CreatedWords.Mix();
            GameObject go = new GameObject("Worker");
            for (int i = 0; i < CreatedWords.Count; i++)
            {
                go.transform.position = RingParrent.position;
                go.transform.Rotate(0, 0, 360f / CreatedWords.Count);
                go.transform.Translate(Vector3.up * RingParrent.GetComponent<RectTransform>().rect.width / 2);
                CreatedWords[i].transform.position = go.transform.position;
            }
            Destroy(go);
        }
    }
}