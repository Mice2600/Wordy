using Base;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;
namespace Study.WaterMelon
{
    public class WaterMelonContent : ContentObject
    {
        [SerializeField, Required]
        private TextMeshPro Letter => GetComponentInChildren<TextMeshPro>();

        public int MaxCount = 8;

        [Button, SerializeField]
        public void Sort()
        {
            TList<Content> contents = new TList<Content>(WordBase.Wordgs);
            Sort(contents.Mix().FindAll(d => d.EnglishSource.Length < MaxCount + 1 && (d as IMultiTranslation).Translations.FindAll(x => d.RussianSource.Length <= MaxCount).Count > 0).RandomItem());
        }

        public void Sort(Content content)
        {
            Content = content;
            Letter.text = content.EnglishSource;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<WaterMelonContent>(out WaterMelonContent contentObject))
            {
                if (contentObject.Content == Content)
                {
                    FindObjectOfType<Creator>().Creat(contentObject, this);
                }
            }
        }
        public void OnMouseUpAsButton()
        {
            EasyTTSUtil.SpeechAdd("");
            ContentObject wordContent = transform.GetComponentInParent<ContentObject>();
            if (wordContent == null) return;
            if (wordContent.Content == null) return;
            if (wordContent.Content is not ISpeeker) return;
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                EasyTTSUtil.SpeechAdd((wordContent.Content as ISpeeker).SpeekText, 1, .5f, 1);
            else Debug.Log(wordContent.Content.EnglishSource + " Speeking");
        }
    }
}