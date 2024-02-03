using Base.Synonym;
using Base;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace Study.CopleFinder
{
    public abstract class CopleFinderContent : ContentObject
    {
        [System.NonSerialized]
        public bool IsFirst;
        public static CopleFinderContent FirstSellected;
        public static CopleFinderContent SecondSellected;

        protected string TextSEllectedtext;
        protected abstract string Text { get; }
        protected abstract bool CanUseVoiceToFirst { get; }
        protected abstract bool CanUseVoiceToSecond { get; }
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

        public virtual void Refresh()
        {
            TMP_Text InsideText = GetComponentInChildren<TMP_Text>(true);
            InsideText.gameObject.SetActive(true);
            InsideText.text = Text;
            VoisOB.SetActive(false);
            if (Random.Range(0, 10) > 5)
            {
                    if ((IsFirst && CanUseVoiceToFirst) || (!IsFirst && CanUseVoiceToSecond))
                    {
                    VoisOB.SetActive(true);
                    InsideText.gameObject.SetActive(false);
                }
                
            }
            
        }

        private CopleFinder CopleFinder => _CopleFinder ??= FindObjectOfType<CopleFinder>();
        private CopleFinder _CopleFinder;
        public void TrySellect()
        {
            if (Dead) return;


            //if (IsFirst || CanUseVoiceToSecond)
                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) 
                {
                    if (!IsFirst && !Regex.IsMatch(TextSEllectedtext, "^[a-zA-Z0-9]*$"))
                    {
                        EasyTTSUtil.Initialize(EasyTTSUtil.Russia);
                        EasyTTSUtil.SpeechAdd(TextSEllectedtext);


                    }
                    else 
                    {
                        EasyTTSUtil.Initialize(EasyTTSUtil.UnitedStates);
                        EasyTTSUtil.SpeechAdd(TextSEllectedtext); 
                    }





                }
                //else Debug.Log(TextSEllectedtext + " Speeking");

            if (IsFirst)
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
                if (FirstSellected.IsThereEqualnest())
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