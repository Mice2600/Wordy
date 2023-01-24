using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Servises
{
    [RequireComponent(typeof(Button))]
    public class SceneLodeButton : MonoBehaviour
    {
        [Button]
        public void Replace()
        {
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            All = All.Replace("_", "");
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Words.txt", All);

            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Dialog.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt");
            All = All.Replace("_", "");
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt", All);//
        }

        [Button]
        public List<Word> das() 
        {
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            TList<Word> AllList = JsonHelper.FromJson<Word>(All);
            return AllList;
        }

        public string SceneName;
        private void Start() => GetComponent<Button>().onClick.AddListener(OpenScene);
        public void OpenScene() =>SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}