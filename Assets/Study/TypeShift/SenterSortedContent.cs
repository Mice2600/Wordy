using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class SenterSortedContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

                    VerCC.ForEach(x => {
                        if (Mathf.Abs(x.transform.position.y - transform.position.y) < Ds) 
                        {
                            Ds = Mathf.Abs(x.transform.position.y - transform.position.y);
                            near = x;
                        }
                    });
                    Answer += near.GetComponentInChildren<TMPro.TMP_Text>().text;
                }
            }
        }
    }
}
