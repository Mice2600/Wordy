using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class AplayButton : MonoBehaviour
{
    private TagCreator TagCreator => _TagCreator ??= GetComponentInParent<TagCreator>();
    private TagCreator _TagCreator;
    private Image Image => _Image ??= GetComponent<Image>();
    private Image _Image;
    private TextMeshProUGUI TextMeshProUGUI => _TextMeshProUGUI ??= GetComponentInChildren<TextMeshProUGUI>();
    private TextMeshProUGUI _TextMeshProUGUI;
    public Color Applay, Wrong;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    private void Update()
    {
        if (TagCreator.TestText(out string Massage))
        {
            Image.color = Applay;
            TextMeshProUGUI.text = "Aplay";
            return;
        }
        Image.color = Wrong;
        TextMeshProUGUI.text = Massage;
    }

    public void OnButton() 
    {
        TagCreator.Creat();
    }

}
