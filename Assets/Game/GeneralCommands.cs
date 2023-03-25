using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemBox.Engine;
using System.Linq;
using UnityEngine.SceneManagement;
using Servises.BaseList;
using Servises;
using Base.Dialog;
using Base.Word;
using EnhancedScrollerDemos.FlickSnap;

public class GeneralCommands : MonoBehaviour
{
    public string FirsSceneName;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(FirsSceneName, LoadSceneMode.Single);
        DialogBase.Dialogs.Avake();
        WordBase.Wordgs.Avake();
        IrregularBase.Irregulars.Avake();
    }
}
public static partial class SceneComands
{
    public static void OpenSceneBaseSearch(string SearchID, int SlideIndex)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            if(SceneManager.GetActiveScene().name != "TotleBaseViwe") SceneManager.LoadScene("TotleBaseViwe", LoadSceneMode.Single);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            GameObject.FindObjectOfType<FlickSnap>().JumpToDataIndex(SlideIndex);
            SearchViwe Lis = GameObject.FindObjectOfType<SearchViwe>(true);
            Lis.gameObject.SetActive(true);
            Lis.OnShearchValueChanged(SearchID);
        }
    }
}
