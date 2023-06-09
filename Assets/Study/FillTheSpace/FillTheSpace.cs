using Base.Word;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;
namespace FillTheSpace
{


    public class FillTheSpace : MonoBehaviour
    {

        QuestFillTheSpace Quest => _Quest ??= GetComponentInChildren<QuestFillTheSpace>();
        QuestFillTheSpace _Quest;

        [Required]
        public Transform MainContentParent;
        [Required, SerializeField]
        private ContentGropper OptionContentParent;
        [Required, SerializeField]
        private GameObject MainContentPrefab;
        [Required, SerializeField]
        private GameObject OptionContentPrefab;

        public Content MainConten;
        public TList<GameObject> Boxs;
        public List<Transform> PlaceBoxs;
        private void Start()
        {
            MainContentParent.transform.ClearChilds();
            OptionContentParent.transform.ClearChilds();

            Color MainColor = GetColor(Random.Range(0, 100));
            Color OptionColor = GetColor(Random.Range(0, 100));
            MainConten = Quest.NeedWord;
            int HidenCount = 1;
            if ((MainConten as IPersanalData).ScoreConculeated > 80)
            {
                HidenCount = ((float)MainConten.EnglishSource.Length * .7f).ToInt();
                OptionColor = MainColor;
            }
            else if ((MainConten as IPersanalData).ScoreConculeated > 60)
            {
                HidenCount = ((float)MainConten.EnglishSource.Length * .6f).ToInt();
                OptionColor = MainColor;
            }
            else if ((MainConten as IPersanalData).ScoreConculeated > 40)
            {
                HidenCount = ((float)MainConten.EnglishSource.Length * .5f).ToInt();
                OptionColor = MainColor;
            }
            else if ((MainConten as IPersanalData).ScoreConculeated > 20)
                HidenCount = ((float)MainConten.EnglishSource.Length * .4f).ToInt();
            else HidenCount = ((float)MainConten.EnglishSource.Length * .3f).ToInt();

            if (HidenCount < 2) HidenCount = 2;

            Boxs = new List<GameObject>();

            MainConten.EnglishSource.ToList().ForEach(B =>
            {
                GameObject Box = Instantiate(MainContentPrefab);
                Box.GetComponentInChildren<TMPro.TMP_Text>().text = B.ToString();
                Box.GetComponentInChildren<TMPro.TMP_Text>().color = MainColor;
                Box.transform.SetParent(MainContentParent);
                Boxs.Add(Box);
            });
            //-------------------------------------------------------------------------



            List<string> HidenLetter = new TList<string>();
            PlaceBoxs = new List<Transform>();

            TList<GameObject> BoxsMixed = Boxs.Mix(true);
            for (int i = 0; i < HidenCount; i++)
            {
                GameObject d = BoxsMixed.RemoveRandomItem();
                HidenLetter.Add(d.GetComponentInChildren<TMPro.TMP_Text>().text);
                d.GetComponentInChildren<TMPro.TMP_Text>().text = "_";
                PlaceBoxs.Add(d.transform);
            }

            List<Transform> PlaceBoxsSorted = MainContentParent.transform.Childs();
            for (int i = 0; i < PlaceBoxsSorted.Count; i++)
            {
                if (!PlaceBoxs.Contains(PlaceBoxsSorted[i]))
                {
                    PlaceBoxsSorted.RemoveAt(i);
                    i--;
                }
            }
            PlaceBoxs = PlaceBoxsSorted;


            //-------------------------------------------------------------------------
            TList<GameObject> OptionBoxes = new List<GameObject>();
            for (int i = 0; i < HidenLetter.Count; i++)
            {
                GameObject Box = Instantiate(OptionContentPrefab);
                Box.GetComponentInChildren<TMPro.TMP_Text>().text = HidenLetter[i].ToString();
                Box.GetComponentInChildren<TMPro.TMP_Text>().color = MainColor;
                //Box.GetComponentInChildren<TMPro.TMP_Text>().color = Boxs();
                OptionBoxes.Add(Box);
            }
            int OC = (HidenLetter.Count < 4) ? 4 : HidenLetter.Count;
            for (int i = 0; i < OC; i++)
            {
                GameObject Box = Instantiate(OptionContentPrefab);
                Box.GetComponentInChildren<TMPro.TMP_Text>().text = ((char)('a' + Random.Range(0, 26))).ToString();
                Box.GetComponentInChildren<TMPro.TMP_Text>().color = OptionColor;
                OptionBoxes.Add(Box);
            }
            OptionBoxes.Mix();
            OptionBoxes.ForEach(b => OptionContentParent.AddNewContent(b.transform));
        }
        public void TryComplete()
        {

            List<Transform> Ch = MainContentParent.Childs();
            string Ans = "";
            for (int i = 0; i < Ch.Count; i++)
                Ans += Ch[i].GetComponent<TMP_Text>().text;
            if (Ans.Contains("_")) return;

            if (MainConten.EnglishSource == Ans)
            {
                Quest.OnWordWin?.Invoke(MainConten as Word);
                Quest.OnGameWin?.Invoke();
                DiscretionCorrectContent.ShowCorrectContent(WordBase.Wordgs[MainConten as Word], 5, OnFinsht);
            }
            else 
            {
                Quest.OnWordLost?.Invoke(MainConten as Word);
                Quest.OnGameLost?.Invoke();
                DiscretionIncorrectContent.ShowIncorrectContent(WordBase.Wordgs[MainConten as Word], new Word(Ans, "", 0, false, "", ""), 5, OnFinsht); 
            }

            void OnFinsht()
            {
                Destroy(gameObject);
                Quest.OnFineshed();
            }

        }
        Color GetColor(int index)
        {
            index = Mathf.Abs(index);
            int levv = (index / 30);
            index -= (levv * 30);
            return ProjectSettings.ProjectSettings.Mine.ContentLoopColors.Evaluate((float)index / 30f);
        }
    }
}