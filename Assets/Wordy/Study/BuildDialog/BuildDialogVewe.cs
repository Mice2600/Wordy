using Base;
using Base.Dialog;
using Base.Word;
using Servises.SmartText;
using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
namespace Study.BuildDialog
{
    public class BuildDialogVewe : ContentObject
    {
        [Required]
        public QuestBuildDialog QuestBuildDialog;
        private ContentGropper StartParrent => _StartParrent ??= GetComponentInChildren<ContentGropper>();
        private ContentGropper _StartParrent;

        private ContentGropperTrueSorted UpParrent => _UpParrent ??= GetComponentInChildren<ContentGropperTrueSorted>();
        private ContentGropperTrueSorted _UpParrent;
        
        [Required]
        public GameObject ShadowContentPrefab;
        [Required]
        public GameObject ContentPrefab;
        public Dialog Groped 
        {
            get 
            {
                List<Transform> Listen = UpParrent.Contents;
                string Sours = "";
                for (int i = 0; i < Listen.Count; i++)
                {
                    ContentObject One = Listen[i].GetComponent<ContentObject>();
                    Sours += One.Content.EnglishSource;
                    if (i + 1 < Listen.Count) Sours += " ";
                }
                return new Dialog(Sours, "", 0, false);
            }
        }
        public bool isTrueGrope => Groped.EnglishSource.ToUpper() == Content.EnglishSource.ToUpper();
        public bool isAnyGrope => UpParrent.Contents.Count > 0;
        [SerializeField]
        private TMP_Text IDContentText;
        [SerializeField]
        private TMP_FontAsset PasswordFont;
        private void Start()
        {
            List<Dialog> Grr = new TList<Dialog>(QuestBuildDialog.NeedDialog, DialogBase.Dialogs.RandomItem());
            Debug.Log((Grr[0].EnglishSource, Grr[1].EnglishSource));
            Content = Grr[0];
            
            TList<string> All = Grr[0].EnglishSource.Split(" ");
            if (All.Count > 3)
            {
                if ((Content as IPersanalData) .ScoreConculeated < 10)
                {
                    MergeOnes(ref All);
                    MergeOnes(ref All);
                }
                else if ((Content as IPersanalData).ScoreConculeated < 25) MergeTreple(ref All);
                else if ((Content as IPersanalData).ScoreConculeated < 50) MergeOnes(ref All);
            }
            TList<string> Otherrr = Grr[1].EnglishSource.Split(" ");
            if (Otherrr.Count > 3) 
            {
                if ((Content as IPersanalData).ScoreConculeated < 20) MergeTreple(ref Otherrr);
                else if ((Content as IPersanalData).ScoreConculeated < 50) MergeOnes(ref Otherrr);
            }

            All.AddRange(Otherrr);
            All.Mix();

            IDContentText.text = Grr[0].RussianSource;
            if (Application.isMobilePlatform && (Content as IPersanalData).ScoreConculeated > 30 && Random.Range(0, 100) > 40) 
            {
                IDContentText.text = Grr[0].RussianSource;
                IDContentText.font = PasswordFont; 
            }
            //IDContentText.font = PasswordFont;

            List<Transform> Gamesss = new List<Transform>();
            for (int i = 0; i < All.Count; i++)
            {
                GameObject N = Instantiate(ContentPrefab, new Vector3(999, 0, transform.root.position.z), Quaternion.identity, transform);
                N.GetComponent<ContentObject>().Content = new Word(All[i], "", 0, false, "", "");
                
                GameObject NShadowDown = Instantiate(ShadowContentPrefab, new Vector3(9999, 99999, transform.position.z), Quaternion.identity, transform);
                NShadowDown.AddComponent<ContentObject>().Content = new Word(All[i], "", 0, false, "", "");
                N.GetComponent<BuildDialogContent>().BuildDialogVewe = this;
                N.GetComponent<BuildDialogContent>().ShadowDown = NShadowDown.GetComponent<RectTransform>();
                N.GetComponent<BuildDialogContent>().CorrentTarget = NShadowDown.GetComponent<RectTransform>();

                Gamesss.Add(NShadowDown.transform);
            }
            StartCoroutine(WaitEnddo());
            IEnumerator WaitEnddo() 
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                Gamesss.ForEach(StartParrent.AddNewContent);
            }
        }

        private void MergeOnes(ref TList<string> All) 
        {
            int Count = All.Count;
            for (int i = 0; i < Count; i+=2)
            {
                if (i >= Count) continue;
                if (i + 1 >= Count) continue;
                All[i] += " " + All[i + 1];
                All[i + 1] = "";
            }
            for (int i = 0; i < All.Count; i++)
            {
                if (string.IsNullOrEmpty(All[i])) 
                {
                    All.RemoveAt(i);
                    i--;
                }
            }
        }
        private void MergeTreple(ref TList<string> All) 
        {
            int Count = All.Count;
            for (int i = 0; i < Count; i+=3)
            {
                if (i >= Count) continue;
                if (i + 1 >= Count) continue;
                if (i + 2 >= Count) 
                {
                    All[i] += " " + All[i + 1];
                    All[i + 1] = "";
                    continue; 
                }

                All[i] += " " + All[i + 1];
                All[i] += " " + All[i + 2];
                All[i + 1] = "";
                All[i + 2] = "";
            }
            for (int i = 0; i < All.Count; i++)
            {
                if (string.IsNullOrEmpty(All[i])) 
                {
                    All.RemoveAt(i);
                    i--;
                }
            }
        }

        public void TryAddMeDown(BuildDialogContent buildDialog) 
        {
            buildDialog.CorrentTarget = buildDialog.ShadowDown;
            UpParrent.RemoveContent(buildDialog.ShadowUp);
            Destroy(buildDialog.ShadowUp.gameObject);
        }
        public void TryAddMeUp(BuildDialogContent buildDialog)
        {
            GameObject NShadowUp = Instantiate(ShadowContentPrefab, new Vector3(9999, 99999, transform.root.position.z), Quaternion.identity, transform);
            NShadowUp.AddComponent<ContentObject>().Content = new Word(buildDialog.Content.EnglishSource, "", 0, false, "", "");
            buildDialog.GetComponent<BuildDialogContent>().ShadowUp = NShadowUp.GetComponent<RectTransform>();
            UpParrent.AddNewContent(NShadowUp.GetComponent<RectTransform>());
            buildDialog.CorrentTarget = buildDialog.ShadowUp;
        }

        public bool TryApplay() 
        {
            if (isTrueGrope)
            {
                QuestBuildDialog.OnDialogWin.Invoke(Content as Dialog);
                QuestBuildDialog.OnGameWin?.Invoke();
                DiscretionCorrectContent N = DiscretionCorrectContent.ShowCorrectContent(Content as Dialog, (QuestBuildDialog.QuestData as IDialogScorer).AddScoreDialog, OnClose);
                WordBase.Wordgs.FindContentsFromString(Groped.EnglishSource, (a) => N.AddChangin(a, (QuestBuildDialog.QuestData as IWordScorer).AddScoreWord));
                return true;
            }
            QuestBuildDialog.OnDialogLost?.Invoke(Content as Dialog);
            QuestBuildDialog.OnGameLost?.Invoke();
            DiscretionIncorrectContent K = DiscretionIncorrectContent.ShowIncorrectContent(Content as Dialog, Groped, (QuestBuildDialog.QuestData as IDialogScorer).RemoveScoreDialog, OnClose);
            WordBase.Wordgs.FindContentsFromString(Groped.EnglishSource, (a) => K.AddChangin(a, (QuestBuildDialog.QuestData as IWordScorer).RemoveScoreWord));
            return false;

            void OnClose() 
            {
                QuestBuildDialog.OnFineshed?.Invoke();
            }

        }

    }
}