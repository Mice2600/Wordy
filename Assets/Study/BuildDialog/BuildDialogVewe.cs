using Base;
using Base.Dialog;
using Base.Word;
using Servises.SmartText;
using Sirenix.OdinInspector;
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
                return new Dialog(Sours, "", 0);
            }
        }
        public bool isTrueGrope => Groped.EnglishSource == Content.EnglishSource;
        public bool isAnyGrope => UpParrent.Contents.Count > 1;
        [SerializeField]
        private TMP_Text IDContentText;
        [SerializeField]
        private TMP_FontAsset PasswordFont;
        private void Start()
        {
            List<Dialog> Grr = DialogBase.Dialogs.GetContnetList(2);
            Content = Grr[0];
            TList<string> All = Grr[0].EnglishSource.Split(" ");
            All.AddRange(Grr[1].EnglishSource.Split(" "));
            All.Mix();

            IDContentText.text = Grr[0].RussianSource;
            if (Application.isMobilePlatform && Content.Score > 30 && Random.Range(0, 100) > 40) 
            {
                IDContentText.text = Grr[0].RussianSource;
                IDContentText.font = PasswordFont; 
            }
            IDContentText.font = PasswordFont;

            List<Transform> Gamesss = new List<Transform>();
            for (int i = 0; i < All.Count; i++)
            {
                GameObject N = Instantiate(ContentPrefab, new Vector3(999, 0, transform.root.position.z), Quaternion.identity, transform);
                N.GetComponent<ContentObject>().Content = new Word(All[i], "", 0, "", "");
                
                GameObject NShadowDown = Instantiate(ShadowContentPrefab, new Vector3(9999, 99999, transform.position.z), Quaternion.identity, transform);
                NShadowDown.AddComponent<ContentObject>().Content = new Word(All[i], "", 0, "", "");
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
        public void TryAddMeDown(BuildDialogContent buildDialog) 
        {
            buildDialog.CorrentTarget = buildDialog.ShadowDown;
            UpParrent.RemoveContent(buildDialog.ShadowUp);
            Destroy(buildDialog.ShadowUp.gameObject);
        }
        public void TryAddMeUp(BuildDialogContent buildDialog)
        {
            GameObject NShadowUp = Instantiate(ShadowContentPrefab, new Vector3(9999, 99999, transform.root.position.z), Quaternion.identity, transform);
            NShadowUp.AddComponent<ContentObject>().Content = new Word(buildDialog.Content.EnglishSource, "", 0, "", "");
            buildDialog.GetComponent<BuildDialogContent>().ShadowUp = NShadowUp.GetComponent<RectTransform>();
            UpParrent.AddNewContent(NShadowUp.GetComponent<RectTransform>());
            buildDialog.CorrentTarget = buildDialog.ShadowUp;
        }

        public bool TryApplay() 
        {
            if (isTrueGrope)
            {
                QuestBuildDialog.OnDialogWin.Invoke((Content as Dialog?).Value);
                QuestBuildDialog.OnGameWin?.Invoke();
                DiscretionCorrectContent N = DiscretionCorrectContent.ShowCorrectContent(
                    DialogBase.Dialogs[(Content as Dialog?).Value], 
                    QuestBuildDialog.AddScoreDialog, OnClose);
                WordBase.Wordgs.FindContentsFromString(Groped.EnglishSource, (a) => N.AddChangin(a, QuestBuildDialog.AddScoreWord));
                return true;
            }
            QuestBuildDialog.OnDialogLost?.Invoke((Content as Dialog?).Value);
            QuestBuildDialog.OnGameLost?.Invoke();
            DiscretionIncorrectContent K = DiscretionIncorrectContent.ShowIncorrectContent(
                DialogBase.Dialogs[(Content as Dialog?).Value], Groped, 
                QuestBuildDialog.RemoveScoreDialog, OnClose);
            WordBase.Wordgs.FindContentsFromString(Groped.EnglishSource, (a) => K.AddChangin(a, QuestBuildDialog.RemoveScoreWord));
            return false;

            void OnClose() 
            {
                QuestBuildDialog.OnFineshed?.Invoke();
            }

        }

    }
}