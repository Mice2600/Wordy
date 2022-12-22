using Base;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.XR;

namespace Study.FindOns
{
    
    public class FindOnsSystem : ContentObject
    {
        [Required]
        public QuestFindOns QuestFindOns;
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
            ContentParrent.ClearChilds();
            int countToCreat = Random.Range(3, 6);
            string ContentLangvich = (Random.Range(0, 100) > 50) ? "ru" : "en";


            GameObject ToCreat = null;

            for (int i = 0; i < countToCreat; i++)
            {
                if (ContentLangvich == "en") ToCreat = PrefabRusianText;
                else ToCreat = (Random.Range(0, 100) > 50) ? PrefabEnglishText : PrefabEnglishSound;
                Instantiate(ToCreat, ContentParrent).GetComponentInChildren<ContentObject>().Content = WordBase.Wordgs.RandomItem();
            }
            Content = ContentParrent.GetChild(Random.Range(0, ContentParrent.childCount)).GetComponentInChildren<ContentObject>().Content;

            ToCreat = PrefabRusianText;
            if (ContentLangvich == "en") ToCreat = (Random.Range(0, 100) > 50) ? PrefabEnglishText : PrefabEnglishSound;
            QueatinContentParrent.ClearChilds();
            Instantiate(ToCreat, QueatinContentParrent).GetComponentInChildren<ContentObject>().Content = Content;
            QueatinContentParrent.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public bool OnApplayButton(IContent content) 
        {
            if (content.EnglishSource == Content.EnglishSource) 
            {
                StartCoroutine(WaitANdTrayAgane());
                return true;
                IEnumerator WaitANdTrayAgane()
                {
                    yield return new WaitForSeconds(1.5f);
                    QuestFindOns.OnWordWin?.Invoke((Content as Word?).Value);
                    QuestFindOns.OnGameWin?.Invoke();
                    DiscretionCorrectContent A = DiscretionCorrectContent.ShowCorrectContent(WordBase.Wordgs[Content], QuestFindOns.AddScoreWord, OnFinsht);
                }
            }
            QuestFindOns.OnWordLost?.Invoke((Content as Word?).Value);
            QuestFindOns.OnWordLost?.Invoke((content as Word?).Value);
            QuestFindOns.OnGameLost?.Invoke();
            DiscretionIncorrectContent D = DiscretionIncorrectContent.ShowIncorrectContent(WordBase.Wordgs[Content], WordBase.Wordgs[content], QuestFindOns.RemoveScoreWord, OnFinsht);
            D.AddChangin(WordBase.Wordgs[content], QuestFindOns.RemoveScoreWord);

            return false;
            void OnFinsht() 
            {
                Destroy(gameObject);
                QuestFindOns.OnFineshed();
            }
        }

    }
}