using Base;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.XR;

namespace Study.FindOns
{
    
    public class FindOnsSystem : ContentObject
    {
        public Quest Quest => _Quest ??= GetComponent<Quest>();
        private Quest _Quest;
        public GameObject PrefabEnglishText;
        public GameObject PrefabEnglishSound;
        public GameObject PrefabRusianText;
        public Transform ContentParrent;
        public Transform QueatinContentParrent;

        private void Start()
        {
            Rondomize();
        }
        public void Rondomize()
        {
            int MaxCount = ContentParrent.childCount;
            ContentParrent.ClearChilds();
            int nicoint = 3;
            if ((Content as IPersanalData).ScoreConculeated > 35) nicoint = MaxCount - 2;
            int countToCreat = Random.Range(nicoint, MaxCount);
            string ContentLangvich = (Quest is not QuestFindOns) ? "ru" : "en";


            GameObject ToCreat = null;
            
            TList<Word> WordList = (Quest is QuestFindOns)? (Quest as QuestFindOns).NeedWords : (Quest as QuestFindOns_T2E).NeedWords;

            for (int i = 0; i < countToCreat; i++)
            {
                if (ContentLangvich == "en") ToCreat = PrefabRusianText;
                else ToCreat = (Random.Range(0, 100) > 50) ? PrefabEnglishText : PrefabEnglishSound;
                Instantiate(ToCreat, ContentParrent).GetComponentInChildren<ContentObject>().Content = WordList.RemoveRandomItem();
            }
            Content = ContentParrent.GetChild(Random.Range(0, ContentParrent.childCount)).GetComponentInChildren<ContentObject>().Content;

            ToCreat = PrefabRusianText;
            if (ContentLangvich == "en") ToCreat = (Random.Range(0, 100) > 50) ? PrefabEnglishText : PrefabEnglishSound;
            QueatinContentParrent.ClearChilds();
            Instantiate(ToCreat, QueatinContentParrent).GetComponentInChildren<ContentObject>().Content = Content;
            QueatinContentParrent.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public bool OnApplayButton(Content content) 
        {
            if (content.EnglishSource == Content.EnglishSource) 
            {
                StartCoroutine(WaitANdTrayAgane());
                return true;
                IEnumerator WaitANdTrayAgane()
                {
                    yield return new WaitForSeconds(1.5f);
                    Quest.OnWordWin?.Invoke(Content as Word);
                    Quest.OnGameWin?.Invoke();
                    DiscretionCorrectContent A = DiscretionCorrectContent.ShowCorrectContent(WordBase.Wordgs[Content as Word], Quest.AddScoreWord, OnFinsht);
                }
            }
            Quest.OnWordLost?.Invoke(Content as Word);
            Quest.OnWordLost?.Invoke(content as Word);
            Quest.OnGameLost?.Invoke();
            DiscretionIncorrectContent D = DiscretionIncorrectContent.ShowIncorrectContent(WordBase.Wordgs[Content as Word], WordBase.Wordgs[content as Word], Quest.RemoveScoreWord, OnFinsht);
            D.AddChangin(WordBase.Wordgs[content as Word], Quest.RemoveScoreWord);

            return false;
            void OnFinsht() 
            {
                Destroy(gameObject);
                Quest.OnFineshed();
            }
        }

    }
}