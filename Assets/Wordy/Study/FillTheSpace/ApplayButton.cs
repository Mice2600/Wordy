using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FillTheSpace
{
    public class ApplayButton : MonoBehaviour
    {

        FillTheSpace fillTheSpace => _fillTheSpace ??= FindObjectOfType<FillTheSpace>();
        FillTheSpace _fillTheSpace;

        [SerializeField]
        private Color Red;
        [SerializeField]
        private string RedTex;
        [SerializeField]
        private Color Green;
        [SerializeField]
        private string GreenTex;

        public void Start()
        {
            StartCoroutine(Upp());
            IEnumerator Upp() 
            {
                while (true) 
                {

                    List<Transform> Ch = fillTheSpace.MainContentParent.Childs();
                    string Ans = "";
                    for (int i = 0; i < Ch.Count; i++)
                        Ans += Ch[i].GetComponent<TMP_Text>().text;
                    if (Ans.Contains("_"))
                    {
                        GetComponent<Image>().color = Red;
                        GetComponentInChildren<TMP_Text>().text = RedTex;
                    }
                    else 
                    {
                        GetComponent<Image>().color = Green;
                        GetComponentInChildren<TMP_Text>().text = GreenTex;
                    }
                    yield return new WaitForSeconds(1);
                }
            }
        }


        public void OnButton() 
        {
            fillTheSpace.TryComplete();
        }
    }
}
