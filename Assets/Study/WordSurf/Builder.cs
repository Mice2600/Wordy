using Base.Word;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private GameObject LatterPrefab;
    [SerializeField]
    public List<string> WordsSS;
    [SerializeField, Required]
    private Transform ContentParrent;
    private void Start()
    {
        Build(WordBase.Wordgs.GetContnetList(25));
        StartCoroutine(enumerator());
        IEnumerator enumerator() 
        {
            yield return new WaitForSeconds(10);
            while (true) 
            {
                yield return new WaitForSeconds(.5f);
                if (Letter.ReadyToDo != null && Letter.ReadyToDo.IsEnpty()) break;
            }
            if (WordsSS.IsEnpty()) { }

            ReBuild();
        }
    }

    [Button]
    public void ReBuild() 
    {
        List<Word> words = new List<Word>();
        WordsSS.ForEach(p => words.Add(WordBase.Wordgs[new Word(p, "", 0, false, "", "")]));
        Build(words);
    }

    [Button]
    public void Build(TList<Word> Words) 
    {
        Letter.Place = new Dictionary<Vector3Int, GameObject>();
        ContentParrent.ClearChilds();
        TList<TList<char>> Created = Creat(Words);
        TList<Transform> Items = new List<Transform>();
        Vector3 Ofset = (new Vector3(Created[0].Count * 100f, Created.Count * 100f) / 2f) - new Vector3(50, 100);
        Created.ForEach((L, Y) =>
        {
            L.ForEach((Leter, X) =>
            {
                Vector3 NPos = new Vector3(X * 100, Y * 100);
                if (Leter != ' ') 
                {
                    Items.Add(Instantiate(LatterPrefab, ContentParrent).transform);
                    Items.Last.transform.localPosition =   NPos - Ofset;
                    Items.Last.GetComponentInChildren<TextMeshProUGUI>().text = Leter.ToString();
                }
            });
        });
    }
    TList<TList<char>> Creat(TList<Word> Words)
    {
        TList<TList<char>> Place = new TList<TList<char>>();
        int XCount = (int)((GetComponent<Canvas>().pixelRect.width / 100f)) -2;
        int YCount = (int)((GetComponent<Canvas>().pixelRect.height / 100f)) - 4;
        for (int i = 0; i < YCount; i++)
        {
            TList<char> N = new TList<char>();
            for (int s = 0; s < XCount; s++)
                N.Add(' ');
            Place.Add(N);
        }
        
        bool Horizontal = Random.Range(0, 100) > 50;
         

        WordsSS = new List<string>();
        for (int i = 0; i < Words.Count; i++)
        {
            string w = Words[i].EnglishSource;
            FindSoitablePlace(w, true, out int HorizontalPos);
            FindSoitablePlace(w, true, out int VerticalPos);
            if (HorizontalPos != -1 || VerticalPos != -1) WordsSS.Add(w);
            if (HorizontalPos == -1 && VerticalPos != -1) PlaceIt(w, false, VerticalPos, Random.Range(0, 100) > 50);
            else if (HorizontalPos != -1 && VerticalPos == -1) PlaceIt(w, true, HorizontalPos, Random.Range(0, 100) > 50);
            else if (HorizontalPos != -1 && VerticalPos != -1)
            {
                if (Random.Range(0, 100) > 50) PlaceIt(w, true, HorizontalPos, Random.Range(0, 100) > 50);
                else PlaceIt(w, false, VerticalPos, Random.Range(0, 100) > 50);
            }
        }

        return Place;

        bool FindSoitablePlace(string Word, bool IsHorizontal, out int Index) 
        {





            TList<int> SuitblePlace = new List<int>();
            Index = -1;
            if (IsHorizontal)
            {
                for (int i = 0; i < Place[0].Count; i++)
                {
                    if (CheakExactPosHorizontal(i, Word.Length)) SuitblePlace.Add(i);
                }
                
            }
            else 
            {
                Index = -1;
                for (int i = 0; i < Place.Count; i++)
                {
                    if (CheakExactPosVertical(i, Word.Length)) SuitblePlace.Add(i);
                }
                
            }
            if (!SuitblePlace.IsEnpty())
            {
                Index = SuitblePlace.RandomItem;
                return true;
            }
            return false;



            bool CheakExactPosHorizontal(int index, int Count) 
            {
                if (index + (Count - 1) >= Place[0].Count) return false;
                for (int i = 0; i < Count; i++)
                {
                    try
                    {
                        if (Place[Place.Count - 1][index + i] != ' ') return false;
                    }
                    catch (System.Exception)
                    {

                        Debug.LogError((Place.Count,
                            Place[0].Count, 
                            index,
                            index + Count,
                            index + i
                            ));
                        throw;
                    }
                    
                }
                    
                return true;
            }
            bool CheakExactPosVertical(int index, int Count)
            {
                if (index + (Count - 1) >= Place.Count) return false;
                for (int i = 0; i < Place[0].Count; i++)
                    if (Place[(Place.Count - 1) - i][index] != ' ') return false;
                return true;
            }
        }


        void PlaceIt(string Word, bool Horisoantal, int StartIndex, bool Rotteted)
        {

            for (int i = 0; i < Word.Length; i++)
            {
                
                UpIt(StartIndex);
                
                
                try
                {
                    if (Rotteted) Place[0][StartIndex] = Word[(Word.Length - 1) - i];
                    else Place[0][StartIndex] = Word[i];
                }
                catch (System.Exception)
                {
                    Debug.Log((Place[0].Count, StartIndex, (Word.Length - 1) - i));
                    throw;
                }
                if (Horisoantal) StartIndex++;

            }
            void UpIt(int Index) 
            {
                for (int i = 0; i < Place.Count; i++)
                {
                    if ((Place.Count - 2) - i == -1) return;
                    try
                    {
                        char Old = Place[(Place.Count - 2) - i][Index];
                        Place[(Place.Count - 1) - i][Index] = Old;
                    }
                    catch (System.Exception)
                    {
                        Debug.Log((Place.Count, (Place.Count - 2) - i, Index));
                        throw;
                    }
                }
            }

        }

    }





    

}
