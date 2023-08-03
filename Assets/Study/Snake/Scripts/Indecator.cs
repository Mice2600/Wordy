using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;

public class Indecator : MonoBehaviour
{
    TextMeshProUGUI tt;
    SnakePlayer PP;
    Snake SS;

    [SerializeField, Required]
    private GameObject Win;

    private void Start()
    {
        Founded = new List<string>();
        tt = GetComponentInChildren<TextMeshProUGUI>();
        DontShowLetter = new List<Letter>();
    }
    
    void Update()
    {
        tt.text = "";
        if (PP == null) 
        {
            PP = FindObjectOfType<SnakePlayer>();
            SS = FindObjectOfType<Snake>();
            PP.CanMove += CheakAndWayt;
            return;
        }

        
    }



    List<Letter> DontShowLetter;
    Coroutine Waiting;
    public System.Action<string> OnFound;
    public List<string> Founded = new List<string>();
    private bool IsWin;
    private bool CheakAndWayt() 
    {
        if (Waiting != null) return false;
        

        if (!IsWin && PP.Map.Count == PP.UsedLetters.Count && Founded.Count == SS.strings.Count)
        {
            IsWin = true;


            StartCoroutine(enumerator());
            IEnumerator enumerator()
            {
                yield return new WaitForSeconds(1);
                TList<Content> contents = new List<Content>();
                QuestSnake d = FindObjectOfType<QuestSnake>();
                Founded.ForEach(ss => { contents.Add(WordBase.Wordgs.GetContent(ss)); d.OnWordWin?.Invoke(contents.Last as Word); });
                d.OnGameWin?.Invoke();
                Instantiate(Win).GetComponent<WinViwe>().contents = contents;

                
            }

            
        }

        string Resolt = "";
        if (PP.ToShowLetters != null)
            PP.ToShowLetters.ForEach(a => 
            {
                if (!DontShowLetter.Contains(a)) 
                    Resolt += a.GetComponentInChildren<TMP_Text>().text;
                
            });

        Resolt = Resolt.Replace(" ", "");
        tt.text = Resolt;
        if (SS.strings.Contains(Resolt)) 
        {
            PP.ToShowLetters.ForEach(a => DontShowLetter.Add(a));
            Waiting = StartCoroutine(Waoting());
            OnFound?.Invoke(Resolt);
            Founded.Add(Resolt);
            


            //Debug.Break();
            return false;
        }
        return true;


        IEnumerator Waoting() 
        {
            yield return new WaitForSeconds( .5f);
            Waiting = null;
        }

    }




}
