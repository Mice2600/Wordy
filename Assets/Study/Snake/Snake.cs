using Base;
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
    public GameObject OutPrefab;
    public GameObject PlayerPrefab;
    public Transform Parrent;
    [System.NonSerialized]
    public TList<Content> UsedContents;
    public List<string> strings= new List<string>();
    public Map map;
    [SerializeField]
    private MyContentGropper ContentViwerParrent;
    [SerializeField]
    private GameObject ContentViwerPrefab;
    public void Creat(Map map)
    {
        this.map = map;
        List<Word> contents = WordBase.Wordgs.GetContnetList(25).Mix();
        string MapString = " ";
        strings = new List<string>();
        List<BoxData> Places = map.WayToWin;
        contents.ForEach(x => 
        {
            if (MapString.Length + x.EnglishSource.Length > Places.Count -1) return;
            strings.Add(x.EnglishSource); 
            MapString += x.EnglishSource + ""; 
        
        });
        MapString += "                    ";
        InstalateMap(map, MapString);
        UsedContents = new List<Content>();
        strings.ForEach(S => { 
            
            UsedContents.Add(WordBase.Wordgs.GetContent(S));
            GameObject LLD = Instantiate(ContentViwerPrefab);
            LLD.GetComponentInChildren<TMP_Text>().text = S;
            ContentViwerParrent.AddNewContent(LLD.transform);
            LLD.GetComponent<ContentObject>().Content = UsedContents.Last;
        });

    }

    public void InstalateMap(Map map, string MapString)
    {

        List<Vector2Int> OutSideAdder = new List<Vector2Int>();
        for (int y = -1; y < map.Size.y + 1; y++)
            for (int x = -1; x < map.Size.x + 1; x++) OutSideAdder.Add(new Vector2Int(x, y));

        List<BoxData> Places = map.boxes;
        List<BoxData> WayToWin = map.WayToWin;

        
        BoxData StartPosition = map.StartPosition;

        List<GameObject> Boxes = new List<GameObject>();
        
        Places.ForEach((YLine, I) => {
            if (YLine.Qoundition == Qoundition.Way)
            {
                GameObject FFA = Instantiate(Prefab, Parrent);
                Boxes.Add(FFA);
                FFA.transform.transform.localPosition = new Vector3Int(YLine.X, YLine.Y, 0);
                FFA.GetComponent<Letter>().boxData = YLine;
                
                if (WayToWin.Contains(YLine))
                {
                    FFA.GetComponentInChildren<TMP_Text>().text = MapString[WayToWin.IndexOf(YLine)].ToString();
                    if (WayToWin.IndexOf(YLine) == 0) Instantiate(PlayerPrefab, Parrent).transform.localPosition = new Vector3Int(YLine.X, YLine.Y, 0);
                }
            }
            else 
            {
                CreatOut(YLine);
            }
            OutSideAdder.Remove(new Vector2Int(YLine.X, YLine.Y));
        });
        OutSideAdder.ForEach(d => CreatOut(new BoxData() {  Qoundition = Qoundition.None, Side = Side.None, X = d.x, Y = d.y}));


        void CreatOut(BoxData _Data)
        {
            GameObject FFA = Instantiate(OutPrefab, Parrent);
            Boxes.Add(FFA);
            FFA.transform.transform.localPosition = new Vector3Int(_Data.X, _Data.Y, 0);
            FFA.GetComponent<OutSide>().boxData = _Data;
        }




        Vector3 Ofset = new Vector3();
        Parrent.localScale *= .75f;
        Boxes.ForEach(l => Ofset += l.transform.position);
        Ofset = Ofset / Boxes.Count;
        Ofset.y = 0;
        Parrent.transform.position -= Ofset;
    }

    void Update()
    {
        
    }
}
