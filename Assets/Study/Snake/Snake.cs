using Base.Word;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject PlayerPrefab;
    public Transform Parrent;
    public List<string> strings= new List<string>();
    public Map map;
    public void Creat(Map map)
    {
        this.map = map;
        List<Word> contents = WordBase.Wordgs.GetContnetList(25).Mix();
        string MapString = "";
        strings = new List<string>();
        List<BoxData> Places = map.WayToWin;
        contents.ForEach(x => 
        {
            if (MapString.Length + x.EnglishSource.Length > Places.Count -1) return;
            strings.Add(x.EnglishSource); MapString += x.EnglishSource; 
        
        });
        Debug.Log("dd");
        MapString += "   ";
        Instalate(map, MapString);
    }

    public void Instalate(Map map, string MapString)
    {
        List<BoxData> Places = map.WayToWin;
        Dictionary<Side, string> SidesSimbol = new Dictionary<Side, string>();
        SidesSimbol.Add(Side.None, "O");
        SidesSimbol.Add(Side.Left, "←");
        SidesSimbol.Add(Side.Right, "→");
        SidesSimbol.Add(Side.Up, "↑");
        SidesSimbol.Add(Side.Down, "↓");

        
        BoxData StartPosition = map.StartPosition;

        List<GameObject> Boxes = new List<GameObject>();

        Places.ForEach((YLine, I) => {
            if (YLine.Qoundition == Qoundition.Way)
            {
                GameObject FFA = Instantiate(Prefab, Parrent);
                Boxes.Add(FFA);
                FFA.transform.transform.localPosition = new Vector3Int(YLine.X, YLine.Y, 0);
                FFA.GetComponent<Letter>().boxData = YLine;
                FFA.GetComponentsInChildren<TMP_Text>()[0].text = SidesSimbol[YLine.Side];
                if (I > 0) 
                {
                    FFA.GetComponentsInChildren<TMP_Text>()[1].text = MapString[0].ToString();
                    MapString = MapString.Remove(0, 1);
                }
                if (YLine == StartPosition) Instantiate(PlayerPrefab, Parrent).transform.localPosition = new Vector3Int(YLine.X, YLine.Y, 0);
            }
        });

        Vector3 Ofset = new Vector3();
        Parrent.localScale *= .85f;
        Boxes.ForEach(l => Ofset += l.transform.position);
        Ofset = Ofset / Boxes.Count;
        Parrent.transform.position -= Ofset;
    }

    void Update()
    {
        
    }
}
