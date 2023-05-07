using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;

public class JoinSystem : MonoBehaviour
{
    public static void TrayJoin(Transform ToTest) 
    {

        List<OneBox> AllOtheres = FindObjectsOfType<OneBox>().ToList();
        List<OneBox> ToCheakBoxes = new List<OneBox>();
        ToTest.transform.Childs().ForEach(a => ToCheakBoxes.Add(a.GetComponent<OneBox>()));
        ToCheakBoxes.ForEach(a => AllOtheres.Remove(a));
        

        for (int i = 0; i < ToCheakBoxes.Count; i++)
        {

            TList<OneBox> Nears = new List<OneBox>();
            AllOtheres.ForEach(a => Nears.AddIf(a, Vector3.Distance(a.transform.position, ToCheakBoxes[i].transform.position) == 1f));


            if (Cheak(ToCheakBoxes[i], Nears, out OneBox NeedToMarge)) 
            {
                Marge(ToCheakBoxes[i].transform.parent, NeedToMarge.transform.parent);
                TrayJoin(ToCheakBoxes[i].transform.parent);
                return;
            }
        }


        




        bool Cheak(OneBox ToCheak, List <OneBox> Nears, out OneBox NeedToMarge) 
        {
            NeedToMarge = null;
            for (int i = 0; i < Nears.Count; i++)
                if (LevlData.CanBeJoined(ToCheak.MinInfo, Nears[i].MinInfo)) 
                {
                    NeedToMarge = Nears[i];
                    return true; 
                }
            return false;
        }

        void Marge(Transform First, Transform Second) 
        {
            Second.Childs().ForEach(a => a.SetParent(First));
            GameObject ss = Second.gameObject;
            Destroy(ss);
        }

    }
}
