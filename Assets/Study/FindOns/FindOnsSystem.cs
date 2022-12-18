using Base;
using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace Study.FindOns
{
    public class FindOnsSystem : MonoBehaviour
    {
        public IContent GameContent;
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
            GameContent = ContentParrent.GetChild(Random.Range(0, ContentParrent.childCount)).GetComponentInChildren<ContentObject>().Content;

            ToCreat = PrefabRusianText;
            if (ContentLangvich == "en") ToCreat = (Random.Range(0, 100) > 50) ? PrefabEnglishText : PrefabEnglishSound;
            QueatinContentParrent.ClearChilds();
            Instantiate(ToCreat, QueatinContentParrent).GetComponentInChildren<ContentObject>().Content = GameContent;
            QueatinContentParrent.GetChild(0).SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }


    }
}