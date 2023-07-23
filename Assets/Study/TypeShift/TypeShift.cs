using Base.Word;
using Sirenix.OdinInspector;
using Study.TwoWordSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
namespace Study.TypeShift
{
    public class TypeShift : MonoBehaviour
    {
        QuestTypeShift Quest => _Quest ??= GetComponentInChildren<QuestTypeShift>();
        QuestTypeShift _Quest;
        [Required]
        public GameObject Horizontal;
        [Required, SerializeField]
        private GameObject SingelLetter;
        public TList<Word> UsingContent;
        public TList<Word> FoundedContent;
        void Start()
        {
            Horizontal.ClearChilds();


            TList<Word> ActiveItems = Quest.NeedWords;
            UsingContent = new TList<Word>(ActiveItems.RemoveRandomItem());
            FoundedContent = new TList<Word>();
            ActiveItems.ForEach(s => UsingContent.AddIf(s, s.EnglishSource.Length == UsingContent[0].EnglishSource.Length));

            TList<TList<char>> ds = new TList<TList<char>>();

            UsingContent[0].EnglishSource.ToList().ForEach(s => ds.Add(new TList<char>()));

            if (UsingContent.Count > 5)
            {
                UsingContent.RemoveRange(5, UsingContent.Count - 5);
            }
            int Length = UsingContent[0].EnglishSource.ToList().Count;
            for (int ss = 0; ss < Length; ss++)
            {
                for (int i = 0; i < UsingContent.Count; i++)
                    ds[ss].AddIfDirty(UsingContent[i].EnglishSource.ToList()[ss]);
            }

            for (int i = 0; i < ds.Count; i++)
            {
                if (UsingContent.Count < 3) ds[i].AddIfDirty((char)('a' + Random.Range(0, 26)));
                ds[i].Mix();
                GameObject v = Creatvertical();
                ds[i].ForEach((a) => Instantiate(SingelLetter, v.transform).GetComponentInChildren<SingelBox>().MyLetter = a);
            }
        }


        GameObject Creatvertical()
        {
            GameObject v = new GameObject("Vertical", typeof(MuseControll), typeof(VerGropeer));
            v.transform.SetParent(Horizontal.transform);
            v.AddComponent<RectTransform>().localPosition = Vector3.zero;
            return v;
        }
        [Required, SerializeField]
        private GameObject WinWindow;
        [Required, SerializeField]
        private GameObject SingelScorePrefab;

        public void onCorrectContentSorted(Content Word)
        {
            if (FoundedContent.Contains(Word as Word)) return;
            FoundedContent.Add(Word as Word);
            if (FoundedContent.Count == UsingContent.Count)
            {
                StartCoroutine(winTime());
                IEnumerator winTime()
                {
                    yield return new WaitForSeconds(1f);
                    WinWindow.SetActive(true);

                    ContentGropper gg = WinWindow.GetComponentInChildren<ContentGropper>();
                    UsingContent.ForEach(a =>
                    {
                        Quest.OnWordWin?.Invoke(a);
                        GameObject G = Instantiate(SingelScorePrefab);
                        G.GetComponent<ScoreChanginInfo>().Set(a, 5);
                        gg.AddNewContent(G.transform);
                    });
                    Quest.OnGameWin?.Invoke();
                }
            }
        }
        public void OnFinsht()
        {
            Destroy(gameObject);
            Quest.OnFineshed();
        }

    }
}