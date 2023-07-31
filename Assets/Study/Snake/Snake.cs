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
    public void Creat(Map map)
    {

        List<Word> contents = WordBase.Wordgs.GetContnetList(25).Mix();
        string MapString = "";

        contents.ForEach(x => MapString += x.EnglishSource);

        Instalate(map, MapString);
    }

    public void Instalate(Map map, string MapString)
    {
        List<BoxData> Places = map.boxes;
        Dictionary<Side, string> SidesSimbol = new Dictionary<Side, string>();
        SidesSimbol.Add(Side.None, "O");
        SidesSimbol.Add(Side.Left, "←");
        SidesSimbol.Add(Side.Right, "→");
        SidesSimbol.Add(Side.Up, "↑");
        SidesSimbol.Add(Side.Down, "↓");

        GameObject dd = new GameObject("Way");
        dd.transform.SetParent(Parrent);
        dd.AddComponent<RectTransform>();
        Places.ForEach((YLine, I) => {
            if (YLine.Qoundition == Qoundition.Way)
            {
                GameObject FFA = Instantiate(Prefab, dd.transform);
                FFA.transform.transform.localPosition = new Vector3Int(YLine.X * 200, YLine.Y * 200, 0);
                FFA.GetComponentsInChildren<TextMeshProUGUI>()[0].text = SidesSimbol[YLine.Side];
                FFA.GetComponentsInChildren<TextMeshProUGUI>()[1].text = MapString[I].ToString();
                //if (YLine.X == map.boxes[map.StartPosition].X && YLine.Y == map.boxes[map.StartPosition].Y)
                 //   Instantiate(PlayerPrefab, dd.transform).transform.localPosition = new Vector3Int(YLine.X * 200, YLine.Y * 200, 0);
            }
        });
    }

    void Update()
    {
        
    }
}
