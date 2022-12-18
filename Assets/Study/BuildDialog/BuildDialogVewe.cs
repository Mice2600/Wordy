using Base;
using Base.Dialog;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace Study.BuildDialog
{
    public class BuildDialogVewe : ContentObject
    {
        public string Tesss;
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
                    BuildDialogContent One = Listen[i].GetComponent<BuildDialogContent>();
                    Sours += One;
                    if (i + 1 < Listen.Count) Sours += " ";
                }
                return new Dialog(Sours, "", 0);
            }
        }
        private void Start()
        {
            Content = new Dialog(Tesss, "", 0);
            TList<string> All = Tesss.Split(" ");
            All.Mix();
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
    }
}