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
    public static void OpenSceneBaseSearch(string SearchID, string SceneName)
    {
        Engine.Get_Engine("Game").StartCoroutine(enumerator());
        IEnumerator enumerator()
        {

            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            List<ISearchList> Lis = new List<ISearchList>(GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISearchList>());
            Lis.ForEach(a => a.OnShearchValueChanged(SearchID));
            GameObject.FindObjectOfType<TMPro.TMP_InputField>().text = SearchID;


        }
    }
}
