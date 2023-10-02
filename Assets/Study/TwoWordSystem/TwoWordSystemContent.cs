using Base;
using Base.Word;
using System.Collections;
using SystemBox;
using TMPro;
using UnityEngine;
namespace Study.TwoWordSystem
{
    public class TwoWordSystemContent : ContentObject
    {
        [System.NonSerialized]
        public bool IsEnglishSide;
        public static TwoWordSystemContent EnglishSellected;
        public static TwoWordSystemContent RussianSellected;

        [SerializeField]
        private GameObject VoisOB;

        [System.NonSerialized]
        public bool Dead;

        private void Start()
        {
            if (Content == null) return;
            OnValueChanged += (s) => Refresh();
            Refresh();
        }

        public void Refresh()
        {
            TMP_Text InsideText = GetComponentInChildren<TMP_Text>(true);
            InsideText.gameObject.SetActive(true);
            if (IsEnglishSide)
            {
                InsideText.text = Content.EnglishSource;
                if (Random.Range(0, 10) > 5)
                {
                    VoisOB.SetActive(false);
                }
                else 
                {
                    VoisOB.SetActive(true);
                    InsideText.gameObject.SetActive(false);
                }
            }
            else 
            {
                
                InsideText.text = (Content as IMultiTranslation).Translations.RandomItem;
                VoisOB.SetActive(false);
            }
        }

        private TwoWordSystem TwoWordSystem => _TwoWordSystem ??= FindObjectOfType<TwoWordSystem>();
        private TwoWordSystem _TwoWordSystem;
        public void TrySellect()
        {
            if (Dead) return;

            if (IsEnglishSide) 
            {
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                    EasyTTSUtil.SpeechAdd((Content as ISpeeker).SpeekText);
                else Debug.Log(Content.EnglishSource + " Speeking");
            }
            
            if (IsEnglishSide)
            {
                if (EnglishSellected == this) EnglishSellected = null;
                else EnglishSellected = this;
            }
            else
            {
                if (RussianSellected == this) RussianSellected = null;
                else RussianSellected = this;
            }
            if (EnglishSellected != null && RussianSellected != null)
            {
                if (EnglishSellected.Content.Equals(RussianSellected.Content))
                {
                    TwoWordSystemContent Ones = EnglishSellected;
                    EnglishSellected = null;
                    Ones.Dead = true;

                    TwoWordSystemContent Twos = RussianSellected;
                    RussianSellected = null;
                    Twos.Dead = true;

                    StartCoroutine(DeadTime());

                    IEnumerator DeadTime()
                    {
                        string OldWord = Content.EnglishSource;
                        yield return new WaitForSeconds(1);
                        TwoWordSystem.TryChange(Ones, Twos);
                        if (Content.EnglishSource != OldWord) 
                        {
                            Twos.Dead = false;
                            Ones.Dead = false;
                        }
                    }
                }
                else
                {
                    TwoWordSystem.WrongChose(EnglishSellected.Content, RussianSellected.Content);
                    EnglishSellected = null;
                    RussianSellected = null;
                }
            }
        }
    }
}