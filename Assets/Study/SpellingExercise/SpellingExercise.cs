using Base.Word;
using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Study.SpellingExercise
{
    public class SpellingExercise : MonoBehaviour
    {
        string ContentWord;
        private void Start()
        {
            WonViwe.SetActive(false);
            NumberViwe.text = "";
            ContentWord = GetComponent<Quest>().NeedWord.EnglishSource;

            group.cellSize = new Vector2 (Camera.main.scaledPixelWidth / 11, Camera.main.scaledPixelWidth / 11);

            //Debug.Log(Camera.main.scaledPixelWidth);


        }
        [SerializeField, Required]
        private TextMeshProUGUI NumberViwe;
        [SerializeField, Required]
        private GameObject WonViwe;
        [SerializeField, Required]
        private UnityEngine.UI.GridLayoutGroup group;
        public void OnSpeekButton(float Speed)
        {
            EasyTTSUtil.StopSpeech();


            string toSs = "";

            for (int i = 0; i < ContentWord.Length; i++)
            {
                toSs += ContentWord[i];
                if (i + 1 != ContentWord.Length) toSs += ",";
            }



            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                EasyTTSUtil.SpeechAdd(toSs, 1, Speed, 1);
            else Debug.Log(toSs + " Speeking");
        }

        public void OnRemoveButton()
        {
            if (NumberViwe.text.Length == 0) return;
            NumberViwe.text = NumberViwe.text.Remove(NumberViwe.text.Length - 1);
        }
        public void OnApplyButton()
        {
            WonViwe.SetActive(true);
            if (NumberViwe.text == ContentWord)
            {
                WonViwe.GetComponentInChildren<Button>().onClick.AddListener(() => GetComponent<QuestSpellingExercise>().Win());
                WonViwe.GetComponentInChildren<TextMeshProUGUI>().text =
                    TextUtulity.Color("---------", Color.green) + "\n" +
                    TextUtulity.Color("Win", Color.green) + "\n" +
                    TextUtulity.Color(ContentWord, Color.green) + "\n" +
                    TextUtulity.Color("---------", Color.green);
            }
            else
            {
                WonViwe.GetComponentInChildren<Button>().onClick.AddListener(() => GetComponent<QuestSpellingExercise>().Lost());
                WonViwe.GetComponentInChildren<TextMeshProUGUI>().text =
                    TextUtulity.Color("---------", Color.green) + "\n" +
                    TextUtulity.Color("Lose", Color.red) + "\n"
                    + TextUtulity.Color(ContentWord, Color.green) + "\n"
                    + TextUtulity.Color(NumberViwe.text, Color.red) + "\n"
                    + TextUtulity.Color("---------", Color.green);
            }
            
        }
        public void OnLetterButton(string Letter) => NumberViwe.text += Letter;

    }
}