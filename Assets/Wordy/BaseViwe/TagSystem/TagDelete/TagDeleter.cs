using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required, HorizontalGroup("TagCreat")]
        public TagDeleter TagDeletViwe;
    }
}

public class TagDeleter : MonoBehaviour
{
    public static void Delet(System.Action OnNewTagDeleted, string tag) 
    {
        TagDeleter D = Instantiate(ProjectSettings.ProjectSettings.Mine.TagDeletViwe); 
        D.OnNewTagDaleted = OnNewTagDeleted; 
        D.CrrentTag = tag; 
    }

    private TMPro.TextMeshProUGUI textContainer => _textContainer ??= GetComponentInChildren<TMPro.TextMeshProUGUI>();
    private TMPro.TextMeshProUGUI _textContainer;

    public System.Action OnNewTagDaleted;
    private string CurrentValue;
    [System.NonSerialized]
    public string CrrentTag;
    private void Start()
    {
        textContainer.text = CrrentTag;
    }

    public void Delete()
    {
        Tagable.DestroyTag(CrrentTag);
        Destroy(gameObject); 
        OnNewTagDaleted?.Invoke();
    }
    public void OnUrself()
    {
        Destroy(gameObject);
    }
}
