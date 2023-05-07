using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;
using UnityEngine.XR;
using Base.Word;
using UnityEngine.UI;
using System.Globalization;

public class OneBox : MonoBehaviour
{
    public (Vector3Int pos, string ID, char Word, string GropeID) MinInfo;
    public string ID;
    public List<Content> contents = new List<Content>();
    private void Start()
    {
        ID = MinInfo.ID;
        



        new List<string>(ID.Split("<DubleID>")).ForEach(NID => {
            string Id = NID;
            Id = Id.Replace("Full_ID_<", "").Replace(">_|_Count_<", "|").Replace(">_|_Text_<", "|").Replace(">_End", "");
            string RID = Id.Split("|")[0];
            contents.Add(new Word(RID, "", 0, true, "", "").BaseCommander.GetContent(RID));
            new Word(RID, "", 0, true, "", "");
        });

        
        ContentLoopColors = ProjectSettings.ProjectSettings.Mine.ContentLoopColors;
        TurnUpdate();
    }

    [SerializeField]
    private List<MaskableGraphic> FirstColor;
    [SerializeField]
    private List<MaskableGraphic> SecondColor;
    private Gradient ContentLoopColors;
    
    public void TurnUpdate()
    {
        if (contents == null) return;
        if (contents.Count == 0) return;
        
        int Index = contents[0].BaseCommander.IndexOf(contents[0]);
        Color NC = GetColor(Index);
        FirstColor.ForEach(s => s.color = NC);
        SecondColor.ForEach(s => s.color = NC);
        if (contents.Count == 2)
        {
            int Indexss = contents[1].BaseCommander.IndexOf(contents[1]);
            Color NCss = GetColor(Indexss);
            SecondColor.ForEach(s => s.color = NCss);
        }
        Color GetColor(int index)
        {
            index = Mathf.Abs(index);
            int levv = (index / 30);
            index -= (levv * 30);
            Color color = ContentLoopColors.Evaluate((float)index / 30f);
            return color;
        }
    }

}