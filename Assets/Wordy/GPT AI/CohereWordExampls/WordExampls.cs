using Base;
using Base.Dialog;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;


namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [Required]
        public GameObject CohereWordExamplsViwe;
    }
}

public class WordExampls : ContentObject
{
    public static void CreatSentencs(Content content)
    {
        WordExampls D = Instantiate(ProjectSettings.ProjectSettings.Mine.CohereWordExamplsViwe).GetComponent<WordExampls>();
        D.Content = content;
        GPTAI.GenerateSentences(content.EnglishSource, 
            (List) => { if (D != null) D.Set(List); D.Sentences = List; });
    }
    public List<string> Sentences = new List<string>();
    public Transform ContentParrent;
    private void Set(List<string> Sentences)
    {
        this.Sentences = Sentences;
        ContentParrent.ClearChilds();
        if (Sentences == null) return;
        for (int i = 0; i < Sentences.Count; i++)
        {
            DialogDefoult DI = new DialogDefoult(Sentences[i], "");
            Instantiate(DI.ContentObject, ContentParrent).GetComponent<ContentObject>().Content = DI;
        }
    }
    //
    [SerializeField]
    private GameObject LoadingViwe;
    
    
    void Start()
    {
        ContentParrent.ClearChilds();
        LoadingViwe.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Sentences == null || Sentences.Count == 0) 
        {
            LoadingViwe.SetActive(true);
        }
        else LoadingViwe.SetActive(false);

    }
    public void OnCloseButton() 
    {
        Destroy(transform.root.gameObject);
    }
}
