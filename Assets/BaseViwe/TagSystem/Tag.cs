using Servises;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
[System.Serializable]
public class Tag
{
    public Tag(string ID) 
    {

        this._ID = ID;
        Contents = JsonHelper.FromJsonList<string>(PlayerPrefs.GetString(ID));
    }
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
                return Contents[Index];
            }
            catch (System.Exception XX) { throw Tools.ExceptionThrow(XX, 2); }
        }
        set
        {
            try
            {
                Contents[Index] = value;
                Save();
            }
            catch (System.Exception XX) { throw Tools.ExceptionThrow(XX, 2); }
        }
    }
    public void Add(string ID) {Contents.AddIfDirty(ID); Save();}
public void Remove(string ID) { Contents.Remove(ID); Save(); }
    public int Count => Contents.Count;
    public bool Contains(string ID) => Contents.Contains(ID);
    private TList<string> Contents;
    public void Save() 
    {
        PlayerPrefs.SetString(ID, JsonHelper.ToJson(Contents.ToList()));
    }

}
