using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

public class Indecator : MonoBehaviour
{
    TextMeshProUGUI tt;
    SnakePlayer PP;
    Snake SS;
    private void Start()
    {
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
    private bool CheakAndWayt() 
    {
        if (Waiting != null) return false;
        
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
