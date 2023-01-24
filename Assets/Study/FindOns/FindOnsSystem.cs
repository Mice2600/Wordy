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
            int countToCreat = Random.Range(3, 8);
            string ContentLangvich = (Random.Range(0, 100) > 50) ? "ru" : "en";


            GameObject ToCreat = null;
            TList<Word> WordList = QuestFindOns.NeedWords;

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
                    QuestFindOns.OnWordWin?.Invoke(Content as Word);
                    QuestFindOns.OnGameWin?.Invoke();
                    DiscretionCorrectContent A = DiscretionCorrectContent.ShowCorrectContent(WordBase.Wordgs[Content as Word], QuestFindOns.AddScoreWord, OnFinsht);
                }
            }
            QuestFindOns.OnWordLost?.Invoke(Content as Word);
            QuestFindOns.OnWordLost?.Invoke(content as Word);
            QuestFindOns.OnGameLost?.Invoke();
            DiscretionIncorrectContent D = DiscretionIncorrectContent.ShowIncorrectContent(WordBase.Wordgs[Content as Word], WordBase.Wordgs[content as Word], QuestFindOns.RemoveScoreWord, OnFinsht);
            D.AddChangin(WordBase.Wordgs[content as Word], QuestFindOns.RemoveScoreWord);

            return false;
            void OnFinsht() 
            {
                Destroy(gameObject);
                QuestFindOns.OnFineshed();
            }
        }

    }
}