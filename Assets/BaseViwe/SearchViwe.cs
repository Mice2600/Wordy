using Servises.BaseList;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchViwe : MonoBehaviour
{
    public System.Action OnSearchStarted;
    public System.Action OnSearchEnded;
    public System.Action<string> OnValueChanged;

    [Required, SerializeField]
    private TMPro.TMP_InputField InputField;
    public bool IsSearching =>  gameObject.activeSelf;
    public string SearchingString => InputField.text;
    public void OnShearchValueChanged(string Value)
    {
        InputField.text = Value.ToUpper();
        OnValueChanged?.Invoke(Value);
    }

    private void OnEnable()
    {
        OnSearchStarted?.Invoke();
    }
    private void OnDisable()
    {
        OnSearchEnded?.Invoke();
    }
}
