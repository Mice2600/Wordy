using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
namespace Study.TypeShift
{
    public class SenterSortedContent : MonoBehaviour
    {
        private TypeShift MTypeShift => _TypeShift ??= FindObjectOfType<TypeShift>();
        private TypeShift _TypeShift;
        public string Answer;
        void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                if (TInput.GetMouseButtonUp(i))
                {
                    Answer = "";
                    List<Transform> VerGrupes = transform.Childs();
                    for (int Vchildi = 0; Vchildi < VerGrupes.Count; Vchildi++)
                    {

                        List<Transform> VerCC = VerGrupes[Vchildi].Childs();
                        float Ds = 999;
                        Transform near = VerGrupes[0];

                        VerCC.ForEach(x =>
                        {
                            if (Mathf.Abs(x.transform.position.y - transform.position.y) < Ds)
                            {
                                Ds = Mathf.Abs(x.transform.position.y - transform.position.y);
                                near = x;
                            }
                        });
                        Answer += near.GetComponentInChildren<TMPro.TMP_Text>().text;

                    }
                    if (MTypeShift.UsingContent.Contains(new Base.Word.Word(Answer, "", 0, false, "", "")))
                    {
                        MTypeShift.onCorrectContentSorted(new Base.Word.Word(Answer, "", 0, false, "", ""));
                    }
                }
            }
        }
    }
}