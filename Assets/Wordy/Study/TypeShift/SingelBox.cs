using Base.Word;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using SystemBox.Simpls;
using UnityEngine;
namespace Study.TypeShift
{
    public class SingelBox : MonoBehaviour
    {
        [System.NonSerialized]
        public char MyLetter;
        public int MyIndex = 0;
        void Start()
        {

            TypeShift typeShift = FindObjectOfType<TypeShift>();

            if (typeShift.UsingContent == null || typeShift.UsingContent.Count == 0) return;
            MyIndex = 0;
            typeShift.Horizontal.transform.Childs().ForEach((x, i) => { if (x.Childs().Contains(transform)) MyIndex = i; });


            InfnityList<Content> contents = new InfnityList<Content>();
            typeShift.UsingContent.ForEach(a => contents.AddIf(a, a.EnglishSource[MyIndex] == MyLetter));
            if (contents.Count == 0)
            {
                GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i) =>
                {
                    j.text = MyLetter.ToString();
                    j.color = Color.white;
                });
                return;
            }

            GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i) =>
            {
                j.text = MyLetter.ToString();
                j.color = GetColor(WordBase.Wordgs.IndexOf(contents[i] as Word));
            });


            StartCoroutine(UU());
            IEnumerator UU()
            {
                while (true)
                {
                    yield return new WaitForSeconds(.5f);

                    InfnityList<Content> ActualContents = new InfnityList<Content>();
                    contents.ForEach(a => ActualContents.AddIf(a, !typeShift.FoundedContent.Contains(a as Word)));

                    GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i) =>
                    {
                        if (ActualContents.Count == 0) j.color = Color.white;
                        else j.color = GetColor(WordBase.Wordgs.IndexOf(ActualContents[i] as Word));
                    });

                }
            }

        }

        Color GetColor(int index)
        {
            if (index == -1) return Color.white;
            index = Mathf.Abs(index);
            int levv = (index / 30);
            index -= (levv * 30);
            return ProjectSettings.ProjectSettings.Mine.ContentLoopColors.Evaluate((float)index / 30f);
        }

    }
}