using Base.Word;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using SystemBox.Simpls;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Generaton : MonoBehaviour
{
    private static Dictionary<Vector3Int, Box> AllCreated => _AllCreated ??= new Dictionary<Vector3Int, Box>();
    private static Dictionary<Vector3Int, Box> _AllCreated;
    private static (Vector2Int Max, Vector2Int Min) Limits;
    [Button]
    public static LevlData tryGnereat(List<string> Contents, (Vector2Int Max, Vector2Int Min) Limits)
    {
        Generaton.Limits = Limits;
        GameObject.FindObjectsOfType<TextMesh>().ToList().ForEach(t =>  { GameObject g = t.gameObject; DestroyImmediate(g); });

        TList<string> vs = Contents;
        

        _AllCreated = new Dictionary<Vector3Int, Box>();
        vs.Mix();


        while (vs.Count > 0)
        {
            int RandomIndex = UnityEngine.Random.Range(0, vs.First.Length);
            char NeedID = vs.First[RandomIndex];
            TList<Box> boxesToTest = new TList<Box>(AllCreated.Values);
            TList<Vector3Int> PosToCheak = new TList<Vector3Int>();
            for (int i = 0; i < boxesToTest.Count; i++)
                if (boxesToTest[i].Word == NeedID)
                    PosToCheak += boxesToTest[i].Pos;
            if (PosToCheak.Count == 0) PosToCheak += Vector3Int.zero;
            PosToCheak.Mix();

            for (int i = 0; i < PosToCheak.Count; i++) 
                if (TryCreaGrupe(vs.First, PosToCheak[i])) 
                    break;


            
            vs.RemoveFirst();

        }

        Dictionary<Box, List<Box>> Gropeer() 
        {
            List<Box> allboxes = new List<Box>();

            List<Vector3Int> keyssss = new List<Vector3Int>(_AllCreated.Keys);
            for (int i = 0; i < _AllCreated.Count; i++) allboxes.Add(_AllCreated[keyssss[i]]);

            List<Box> AllEnds = new List<Box>();
            for (int i = 0; i < allboxes.Count; i++) if (allboxes[i].IsEnd || allboxes[i].IsDubleEnd) AllEnds.Add(allboxes[i]);




            List<Box> TTAllEnds = new List<Box>(AllEnds);


            Dictionary<Box, List<Box>> AllGropes = new Dictionary<Box, List<Box>>();
            List<Box> Usenboxes = new List<Box>();

            while (AllEnds.Count > 0)
            {
                for (int i = 0; i < AllEnds.Count; i++)
                {
                    if (!_Update(AllEnds[i]))
                    {
                        AllEnds.RemoveAt(i); i--;
                    }
                }
            }
            bool _Update(Box Lider) //return true if some newws
            {
                if (!AllGropes.ContainsKey(Lider))
                {
                    AllGropes.Add(Lider, new List<Box>() { Lider });
                    Usenboxes.Add(Lider);
                }
                Box lastt = AllGropes[Lider][AllGropes[Lider].Count - 1];

                List<Box> toSellect = new List<Box>();

                if (_AllCreated.ContainsKey(lastt.Pos + Vector3Int.up))
                    if (!Usenboxes.Contains(_AllCreated[lastt.Pos + Vector3Int.up]))
                        toSellect.Add(_AllCreated[lastt.Pos + Vector3Int.up]);


                if (_AllCreated.ContainsKey(lastt.Pos + Vector3Int.down))
                    if (!Usenboxes.Contains(_AllCreated[lastt.Pos + Vector3Int.down]))
                        toSellect.Add(_AllCreated[lastt.Pos + Vector3Int.down]);

                if (_AllCreated.ContainsKey(lastt.Pos + Vector3Int.left))
                    if (!Usenboxes.Contains(_AllCreated[lastt.Pos + Vector3Int.left]))
                        toSellect.Add(_AllCreated[lastt.Pos + Vector3Int.left]);

                if (_AllCreated.ContainsKey(lastt.Pos + Vector3Int.right))
                    if (!Usenboxes.Contains(_AllCreated[lastt.Pos + Vector3Int.right]))
                        toSellect.Add(_AllCreated[lastt.Pos + Vector3Int.right]);

                if (toSellect.Count == 0) return false; //done remove 
                Box sellected = toSellect[UnityEngine.Random.Range(0, toSellect.Count)];
                Usenboxes.Add(sellected);
                AllGropes[Lider].Add(sellected);
                return true;
            }




            return AllGropes;
        }
        Dictionary<Box, List<Box>> GropedDic = Gropeer();



        TList<(Vector3Int pos, string ID, char Word, string GropeID)> AllPoseIDWord = new TList<(Vector3Int pos, string ID, char Word, string GropeID)>();

        GropedDic.Values.ToList().ForEach((F, i) => {
            string GropeName = "Grope: " + i + " "+ UnityEngine.Random.Range(-99999f, 99999f);
            F.ForEach(b => AllPoseIDWord.Add((b.Pos, b.ID, b.Word, GropeName)));
        });

        LevlData Resolt = new LevlData(){AllPoseIDWord = AllPoseIDWord};





        if (AllPoseIDWord.Count != _AllCreated.Count) return tryGnereat(Contents, Limits);

        return Resolt;
    }

    public static bool TryCreaGrupe(string tt, Vector3Int Pos)
    {
        if(tt.Length  < 3)return false;
        TList<int> indexsToTry = new TList<int>();
        if (AllCreated.ContainsKey(Pos))
            for (int i = 0; i < tt.Length; i++)
                if (tt[i] == AllCreated[Pos].Word)
                    indexsToTry += i;
        if (AllCreated.ContainsKey(Pos) && indexsToTry.Count == 0) return false;
        if (indexsToTry.Count == 0) indexsToTry += 0;

        for (int i = 0; i < indexsToTry.Count; i++)
        {
            int needStartCount = 0;
            int needEndCount = 0;
            for (int h = 0; h < tt.Length; h++)
            {
                if (h < indexsToTry[i]) needStartCount++;
                if (h > indexsToTry[i]) needEndCount++;
            }

            
            if (LeftEnptyCount(Pos) > needStartCount && RightEnptyCount(Pos) > needEndCount)
            {
                if (needStartCount == 0 && LeftEnptyCount(Pos) == 0
                    || needEndCount == 0 && RightEnptyCount(Pos) == 0)
                {
                    // not work becous  horizontal 1234 + 4562 is wrong
                }
                else 
                {
                    CreaGrupe(tt, tt.Length - needEndCount - 1, true, Pos);
                    return true;
                }
                    
                
            }
            if (UpEnptyCount(Pos) > needStartCount && DownEnptyCount(Pos) > needEndCount)
            {
                if (needStartCount == 0 && UpEnptyCount(Pos) == 0 
                    || needEndCount == 0 && DownEnptyCount(Pos) == 0)
                {
                    // not work becous  vertical 1234 + 4562 is wrong
                }
                else 
                {
                    CreaGrupe(tt, tt.Length - needEndCount - 1, false, Pos);
                    return true;
                }
                
            }
        }

        return false;
    }
    
    private static void CreaGrupe(string tt, int index, bool horizontal, Vector3Int IndexPos) // tested
    {
        for (int i = 0; i < tt.Length; i++)
        {
            Vector3 pos = new Vector3();
            if (i == index) { pos = IndexPos; }
            if (i < index)
            {
                pos = new Vector3(
   (horizontal) ? IndexPos.x - (index - i) : IndexPos.x,
   (!horizontal) ? IndexPos.y + (index - i) : IndexPos.y, 0);
            }
            if (i > index)
            {
                pos = new Vector3(
                    (horizontal) ? (IndexPos.x - index)  + i : IndexPos.x,
                    (!horizontal) ? IndexPos.y - (i - index) : IndexPos.y, 0);
            }
            if (AllCreated.ContainsKey(pos.ToInt())) 
            {
                AllCreated[pos.ToInt()].ID += $"<DubleID>Full_ID_<{tt}>_|_Count_<{i}>_|_Text_<{tt[i]}>_End";
                continue; 
            }
            Box dd = new Box();
            dd.Word = tt[i];

            dd.ID = $"Full_ID_<{tt}>_|_Count_<{i}>_|_Text_<{tt[i]}>_End";
            dd.Pos = pos.ToInt();
            AllCreated.Add(dd.Pos, dd);
        }
    }

    private static int UpEnptyCount(Vector3Int PosToTest)
    {
        if (PosToTest.y < Limits.Min.y) return 0;
        if (PosToTest.y > Limits.Max.y) return 0;
        if (PosToTest.x < Limits.Min.x) return 0;
        if (PosToTest.x > Limits.Max.x) return 0;
        int count = 0;

        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.up * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.up))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    private static int DownEnptyCount(Vector3Int PosToTest)
    {
        if (PosToTest.y < Limits.Min.y) return 0;
        if (PosToTest.y > Limits.Max.y) return 0;
        if (PosToTest.x < Limits.Min.x) return 0;
        if (PosToTest.x > Limits.Max.x) return 0;
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.down * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.down))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;

    }

    private static int RightEnptyCount(Vector3Int PosToTest)
    {
        if (PosToTest.y < Limits.Min.y) return 0;
        if (PosToTest.y > Limits.Max.y) return 0;
        if (PosToTest.x < Limits.Min.x) return 0;
        if (PosToTest.x > Limits.Max.x) return 0;
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.right * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.right + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    private static int LeftEnptyCount(Vector3Int PosToTest)
    {
        if (PosToTest.y < Limits.Min.y) return 0;
        if (PosToTest.y > Limits.Max.y) return 0;
        if (PosToTest.x < Limits.Min.x) return 0;
        if (PosToTest.x > Limits.Max.x) return 0;
        int count = 0;
        for (int i = 1; i < 15; i++)
        {
            Vector3Int dd = Vector3Int.left * i;
            if (!AllCreated.ContainsKey(PosToTest + dd))
                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left))
                    if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.down))
                        if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.up))
                            if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.down))
                                if (!AllCreated.ContainsKey(PosToTest + dd + Vector3Int.left + Vector3Int.up))
                                    count++;
                                else break;
                            else break;
                        else break;
                    else break;
                else break;
            else break;
        }
        return count;
    }

    private class Box 
    {
        public char Word;
        public string ID;
        public Vector3Int Pos;
        
        [ShowInInspector]
        public int SaidCount { 
            get 
            {
                int SC = 0;
                if(Generaton.AllCreated.ContainsKey(Pos + Vector3Int.up)) SC++;
                if(Generaton.AllCreated.ContainsKey(Pos + Vector3Int.down)) SC++;
                if(Generaton.AllCreated.ContainsKey(Pos + Vector3Int.left)) SC++;
                if(Generaton.AllCreated.ContainsKey(Pos + Vector3Int.right)) SC++;
                return SC;
            } 
        }
        [ShowInInspector]
        public bool IsEnd => SaidCount <= 1;
        public bool IsDubleEnd 
        {
            get 
            {
                if (SaidCount != 2) return false;
                if (Generaton.AllCreated.ContainsKey(Pos + Vector3Int.up) && Generaton.AllCreated.ContainsKey(Pos + Vector3Int.down)) return false;
                if (Generaton.AllCreated.ContainsKey(Pos + Vector3Int.left) && Generaton.AllCreated.ContainsKey(Pos + Vector3Int.right)) return false;
                return true;
            }
        }
        
    }

}
public struct LevlData //transleted
{
    [ShowInInspector]
    public TList<(Vector3Int pos, string ID, char Word, string GropeID)> AllPoseIDWord;// pos id word - list shunga qarab tahlidi


    [ShowInInspector]
    public List<string> AllContentIDes 
    {
        get 
        {
            TList<string> Resolt = new List<string>();
            AllPoseIDWord.ForEach(One => {
                string Id = One.ID;
                Id = Id.Replace("Full_ID_<", "").Replace(">_|_Count_<", "|").Replace(">_|_Text_<", "|").Replace(">_End", "").Replace("<DubleID>","|");
                string RID = Id.Split("|")[0];
                Resolt.AddIfDirty(RID);
            });
            return Resolt;
        }
    }
    [ShowInInspector]
    public List<List<string>> AllTrueIDs//birbiriga qoshish kere bogan id la ketma ketligi
    {
        get
        {
            List<List<string>> Resolt = new List<List<string>>();
            Dictionary<string, List<string>> Sorted = new Dictionary<string, List<string>>();

            AllPoseIDWord.ForEach(One => {


                new List<string>(One.ID.Split("<DubleID>")).ForEach(NID => {

                    string Id = NID;
                    Id = Id.Replace("Full_ID_<", "").Replace(">_|_Count_<", "|").Replace(">_|_Text_<", "|").Replace(">_End", "");
                    string RID = Id.Split("|")[0];
                    if (!Sorted.ContainsKey(RID)) Sorted.Add(RID, new List<string>() { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });

                    int Innn = int.Parse(Id.Split("|")[1]);
                    Sorted[RID][Innn] = One.ID;
                });
                
            });

            List<string> kees = new List<string>(Sorted.Keys);
            for (int i = 0; i < kees.Count; i++)
            {
                for (int ss = 0; ss < Sorted[kees[i]].Count; ss++)
                {
                    if (string.IsNullOrEmpty(Sorted[kees[i]][ss]))
                    {
                        Sorted[kees[i]].RemoveAt(ss);
                        ss--;
                    }
                }

                Resolt.Add(Sorted[kees[i]]);
            }
            return Resolt;
        }
    }
    [ShowInInspector]
    public List<List<string>> GropesFromID//id grupala birbiriga qoshish ga
    {
        get
        {
            List<List<string>> Resolt = new List<List<string>>();
            Dictionary<string, List<string>> Sorted = new Dictionary<string, List<string>>();

            AllPoseIDWord.ForEach(One => {
                if (!Sorted.ContainsKey(One.GropeID)) Sorted.Add(One.GropeID, new List<string>() { One.ID });
                else Sorted[One.GropeID].Add(One.ID);
            });

            Sorted.Values.ToList().ForEach(l => Resolt.Add(l));
            return Resolt;
        }
    }
    [ShowInInspector]
    public List<string> GropesID//id grupala
    {
        get 
        {
            List<string> Sorted = new List<string>();
            AllPoseIDWord.ForEach(One => {
                if (!Sorted.Contains(One.GropeID)) Sorted.Add(One.GropeID);
            });
            return Sorted;
        }
    }


    public static bool CanBeJoined((Vector3Int pos, string ID, char Word, string GropeID) Firs, (Vector3Int pos, string ID, char Word, string GropeID) Second, bool isHorizontal)
    {
        if(isHorizontal && Second.pos.x != Firs.pos.x) return false;
        
        bool IsThereId = false;


        new List<string>(Firs.ID.Split("<DubleID>")).ForEach(NID =>
        {
            

            string Id = NID;
            Id = Id.Replace("Full_ID_<", "").Replace(">_|_Count_<", "|").Replace(">_|_Text_<", "|").Replace(">_End", "");
            string RID = Id.Split("|")[0];

            new List<string>(Second.ID.Split("<DubleID>")).ForEach(Sedd => {
                string IdNexx = Sedd;
                IdNexx = IdNexx.Replace("Full_ID_<", "").Replace(">_|_Count_<", "|").Replace(">_|_Text_<", "|").Replace(">_End", "");
                string NexrRRR = IdNexx.Split("|")[0];
                if (RID == NexrRRR) 
                {
                    int NexxNumber = int.Parse(IdNexx.Split("|")[1]);
                    int MineNumber = int.Parse(Id.Split("|")[1]);
                    
                    if (Mathf.Abs(NexxNumber - MineNumber) == 1) 
                    {
                        IsThereId = true;
                    }
                }
            });
        });
        return IsThereId;
    }

}

