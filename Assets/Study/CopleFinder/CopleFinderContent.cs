using Base.Synonym;
using Base;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Study.CopleFinder
{
    public abstract class CopleFinderContent : ContentObject
    {
        [System.NonSerialized]
        public bool SideType;
        public static CopleFinderContent FirstSellected;
        public static CopleFinderContent SecondSellected;

        protected abstract string Text { get; }
        protected abstract bool CanUseVoiceToBothSids { get; }
        //protected abstract bool CanUseVoiceToBothSids { get; }

        [SerializeField]
        private GameObject VoisOB;
        [System.NonSerialized]
        public bool Dead;

        private void Start()
        {
            GetComponentInChildren<Button>().onClick.AddListener(TrySellect);
            if (Content == null) return;
            OnValueChanged += (s) => Refresh();
            Refresh();
        }

        public void Refresh()
        {
            TMP_Text InsideText = GetComponentInChildren<TMP_Text>(true);
            InsideText.gameObject.SetActive(true);
            InsideText.text = Text;

            if (Random.Range(0, 10) > 5 && CanUseVoiceToBothSids)
            {
                VoisOB.SetActive(true);
                InsideText.gameObject.SetActive(false);
            }
            else
            {
                VoisOB.SetActive(false);
            }
        }

        private CopleFinder CopleFinder => _CopleFinder ??= FindObjectOfType<CopleFinder>();
        private CopleFinder _CopleFinder;
        public void TrySellect()
        {
            if (Dead) return;

            if (SideType || CanUseVoiceToBothSids)
            {
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                    EasyTTSUtil.SpeechAdd(GetComponentInChildren<TMP_Text>(true).text);
                else Debug.Log(GetComponentInChildren<TMP_Text>(true).text + " Speeking");
            }

            if (SideType)
            {
                if (FirstSellected == this) FirstSellected = null;
                else FirstSellected = this;
            }
            else
            {
                if (SecondSellected == this) SecondSellected = null;
                else SecondSellected = this;
            }
            if (FirstSellected != null && SecondSellected != null)
            {
                if (IsThereEqualnest())
                {
                    CopleFinderContent Ones = FirstSellected;
                    FirstSellected = null;
                    Ones.Dead = true;

                    CopleFinderContent Twos = SecondSellected;
                    SecondSellected = null;
                    Twos.Dead = true;

                    StartCoroutine(DeadTime());

                    IEnumerator DeadTime()
                    {
                        string OldWord = Content.EnglishSource;
                        yield return new WaitForSeconds(1);
                        CopleFinder.TryChange(Ones, Twos);
                        if (Content.EnglishSource != OldWord)
                        {
                            Twos.Dead = false;
                            Ones.Dead = false;
                        }
                    }
                }
                else
                {
                    CopleFinder.WrongChose(FirstSellected.Content, SecondSellected.Content);
                    FirstSellected = null;
                    SecondSellected = null;
                }
            }
        }

        public virtual bool IsThereEqualnest() => FirstSellected.Content.Equals(SecondSellected.Content);

    }
}