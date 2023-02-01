using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteIrregularSystem : MonoBehaviour
{
    [Required]
    public QuestWriteIrregular questWriteWord;
    public GameObject ContentPrefab;
    

    private void Start()
    {
    
    }
    public bool TrayToComplate(string MadenWord)
    {
        return false;
        /*
         
        if (Content.EnglishSource == MadenWord)
        {
            StartCoroutine(WaitANdTrayAgane());
            return true;
            IEnumerator WaitANdTrayAgane()
            {
                yield return new WaitForSeconds(1.5f);
                questWriteWord.OnWordWin?.Invoke(Content as Word);
                questWriteWord.OnGameWin?.Invoke();
                DiscretionCorrectContent A = DiscretionCorrectContent.ShowCorrectContent(WordBase.Wordgs[Content as Word], questWriteWord.AddScoreWord, OnFinsht);
            }
        }


        questWriteWord.OnWordLost?.Invoke(Content as Word);
        questWriteWord.OnGameLost?.Invoke();
        DiscretionIncorrectContent D = DiscretionIncorrectContent.ShowIncorrectContent(WordBase.Wordgs[Content as Word], new Word(MadenWord, "", 0, false, "", ""), questWriteWord.RemoveScoreWord, OnFinsht);
        D.AddChangin(WordBase.Wordgs[Content as Word], questWriteWord.RemoveScoreWord);

        return false;


        void OnFinsht()
        {
            Destroy(gameObject);
            questWriteWord.OnFineshed();
        } 
         */

    }
}
