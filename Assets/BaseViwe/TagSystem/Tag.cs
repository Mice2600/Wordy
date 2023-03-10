using Servises;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class Tag 
{
    public Tag(string ID) => this._ID = ID;
    public string ID { get => _ID;
        set 
        {
            if (_ID != value) 
            {
                PlayerPrefs.SetString(_ID, PlayerPrefs.GetString(ID));
                PlayerPrefs.SetString(ID, "");
            }
            _ID = value;
        } 
    }
    private string _ID;
    public string this[int Index] 
    {
        get
        {
            try
            {
                Contents ??= JsonHelper.FromJsonList<string>(PlayerPrefs.GetString(ID));
                return Contents[Index];
            }
            catch (System.Exception XX) { throw Tools.ExceptionThrow(XX, 2); }
        }
        set
        {
            try
            {
                Contents[Index] = value;
                PlayerPrefs.SetString(ID, JsonHelper.ToJson(Contents));
            }
            catch (System.Exception XX) { throw Tools.ExceptionThrow(XX, 2); }
        }
    }
    public int Count => Contents.Count;
    public bool Contains(string ID) => Contents.Contains(ID);
    private List<string> Contents;
}
