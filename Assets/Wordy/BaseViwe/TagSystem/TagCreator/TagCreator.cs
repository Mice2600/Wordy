using Base;
using Sirenix.OdinInspector;


namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required, HorizontalGroup("TagCreat")]
        public TagCreator TagCreatorViwe;
    }
}

public class TagCreator : ContentObject
{
    public static void Open(Tagable content, System.Action OnNewTagCreated)
    {
        TagCreator Item = Instantiate(ProjectSettings.ProjectSettings.Mine.TagCreatorViwe);
        Item.Content = content as Content;
        Item.OnNewTagCreated = OnNewTagCreated;

    }

    private TMPro.TMP_InputField textContainer => _textContainer ??= GetComponentInChildren<TMPro.TMP_InputField>();
    private TMPro.TMP_InputField _textContainer;
    public System.Action OnNewTagCreated;
    private string CurrentValue;
    private void Start()
    {
        textContainer.onValueChanged.AddListener(OnValuechanged);
    }
    public void OnValuechanged(string Value)
    {
        Value = Value.Replace(" ", "").Replace("/", "").Replace(@"\", "").
            Replace(@"(", "").Replace(@")", "").Replace(@"{", "").Replace(@"}", "").
            Replace(@"[", "").Replace(@"]", "").Replace("@", "").Replace(@"#", "").
            Replace(@"!", "").Replace(@"$", "").Replace(@"%", "").Replace(@"^", "").
            Replace(@"&", "").Replace(@"*", "").Replace(@"-", "").Replace(@"_", "").
            Replace(@"=", "").Replace(@"+", "").Replace(@"`", "").Replace(@"~", "").
            Replace(@"|", "").Replace(@"'", "").Replace('"', ' ').Replace(@";", "").
            Replace(@":", "").Replace(@"?", "").Replace(@".", "").Replace(@",", "");

        if (Value.Length > 15) Value = Value.Remove(15);
        textContainer.text = Value;
        CurrentValue = Value;
    }


    public void Creat()
    {
        if (!TestText(out string Massage)) return;
        (Content as Tagable).AddTag(CurrentValue);
        Destroy(gameObject);
        OnNewTagCreated?.Invoke();

    }

    public bool TestText(out string Massage)
    {
        if (string.IsNullOrEmpty(this.CurrentValue)) { Massage = "Write"; return false; }
        string CurrentValue = "@" + this.CurrentValue;
        if (Tagable.GetListOfTags().Contains(CurrentValue)) { Massage = "Allredy Have"; return false; }
        Massage = "Applay";
        return true;
    }
    public void OnUrself()
    {
        Destroy(gameObject);
    }

}
