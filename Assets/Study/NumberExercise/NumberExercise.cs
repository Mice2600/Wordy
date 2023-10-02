using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Study.NumberExercise
{
    public class NumberExercise : MonoBehaviour
    {
        private bool IsSingelNumber;
        string ContentNumber;
        private void Start()
        {
            WonViwe.SetActive(false);
            NumberViwe.text = "";
            IsSingelNumber = Random.Range(0, 100) > 50;
            if (IsSingelNumber)
            {
                ContentNumber += Random.Range(10, 9999);
            }
            else 
            {
                int Count = Random.Range(5, 20);
                for (int i = 0; i < Count; i++)
                {
                    ContentNumber += Random.Range(0, 10);
                }
            }
        }
        [SerializeField, Required]
        private TextMeshProUGUI NumberViwe;
        [SerializeField, Required]
        private GameObject WonViwe;

        public void OnSpeekButton(float Speed) 
        {
            EasyTTSUtil.StopSpeech();
            string toSs = ContentNumber;
            if (!IsSingelNumber)
            {
                toSs = "";
                for (int i = 0; i < ContentNumber.Length; i++)
                {
                    toSs += ContentNumber[i];
                    if (i + 1 != ContentNumber.Length) toSs += ",";
                }    
            }
            
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                EasyTTSUtil.SpeechAdd(toSs, 1, Speed, 1);
            else Debug.Log(toSs + " Speeking");
        }

        public void OnRemoveButton() 
        {
            if (NumberViwe.text.Length == 0) return;
            NumberViwe.text = NumberViwe.text.Remove(NumberViwe.text.Length -1);
        }
        public void OnApplyButton() 
        {
            WonViwe.SetActive(true);
            if (NumberViwe.text == ContentNumber)
            {
                WonViwe.GetComponentInChildren<TextMeshProUGUI>().text =
                    TextUtulity.Color("---------", Color.green) + "\n" +
                    TextUtulity.Color("Win", Color.green) + "\n" +
                    TextUtulity.Color(ContentNumber, Color.green) + "\n" +
                    TextUtulity.Color("---------", Color.green);
            }
            else 
            {
                WonViwe.GetComponentInChildren<TextMeshProUGUI>().text =
                    TextUtulity.Color("---------", Color.green) + "\n" +
                    TextUtulity.Color("Lose", Color.red) + "\n"
                    + TextUtulity.Color(ContentNumber, Color.green) + "\n" 
                    + TextUtulity.Color(NumberViwe.text, Color.red) + "\n"
                    + TextUtulity.Color("---------", Color.green);
            }
        }
        public void OnNummberButton(int Number) 
        {
            NumberViwe.text += Number;
        }
    }
}