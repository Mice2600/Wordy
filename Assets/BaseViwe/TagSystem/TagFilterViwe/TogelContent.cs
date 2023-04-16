using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class TogelContent : MonoBehaviour
{
    public System.Action<bool> OnBoolChanged;
    public System.Action OnDestroyButton;
    public bool isOn { get => GetComponent<Toggle>().isOn; set => GetComponent<Toggle>().isOn = value; }
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((GG) => { OnBoolChanged?.Invoke(GG); });
    }
    public void OnDestroyButtonF() => OnDestroyButton?.Invoke();
}
