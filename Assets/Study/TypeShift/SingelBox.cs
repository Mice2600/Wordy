using Base.Word;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using SystemBox.Simpls;
using UnityEngine;

public class SingelBox : MonoBehaviour
{
    [System.NonSerialized]
    public char MyLetter;
    public int MyIndex = 0;
    void Start()
    {

        TypeShift typeShift =FindObjectOfType<TypeShift>();

        if (typeShift.UsingContent == null || typeShift.UsingContent.Count == 0) return;
        MyIndex = 0;
        typeShift.Horizontal.transform.Childs().ForEach((x, i) =>{if (x.Childs().Contains(transform)) MyIndex = i;});


        InfnityList<Content> contents = new InfnityList<Content>();
        typeShift.UsingContent.ForEach(a => contents.AddIf(a, a.EnglishSource[MyIndex] == MyLetter));
        if (contents.Count == 0) 
        {
            GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i) => {
                j.text = MyLetter.ToString();
                j.color = Color.white;
            });
            return; 
        }
        
        GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i)=> {
            j.text = MyLetter.ToString();
            j.color = GetColor(i);
        });


        StartCoroutine(UU());
        IEnumerator UU()    
        {
            while (true) 
            {
                yield return new WaitForSeconds(.5f);

                InfnityList<Content> ActualContents = new InfnityList<Content>();
                contents.ForEach(a => ActualContents.AddIf(a, !typeShift.FoundedContent.Contains(a as Word)));

                GetComponentsInChildren<TMPro.TMP_Text>().ToList().ForEach((j, i) => {
                    if (ActualContents.Count == 0) j.color = Color.white;
                    else j.color = GetColor(i);
                });

            }
        }

    }

    Color GetColor(int index)
    {
        return new LoopList<Color>() { Color.red, Color.blue, Color.green, Color.yellow, TColor.Pink, TColor.Mint, TColor.Light_green }[index];
    } 

}
