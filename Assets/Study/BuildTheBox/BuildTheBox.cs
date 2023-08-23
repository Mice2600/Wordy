using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using TMPro;
using UnityEngine;

public class BuildTheBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    Dictionary<Vector2Int, char> Places;
    [Button]
    void Generate()
    {
        TList<Content> Contents = new TList<Content>(WordBase.Wordgs.GetContnetList(25));


        Places = new Dictionary<Vector2Int, char>();



        // mac cound = 7 item max count = 6
        TList<Content> ChosenContents = new TList<Content>();
        int YSize = Random.Range(4, 8);

        Contents.ForEach(c => {
            if (c.EnglishSource.Length < 7 && ChosenContents.Count < YSize) ChosenContents.Add(c);
        });
        ChosenContents.ForEach((C, i) => {
            List<(Vector2Int, char)> values = Acomadate(C.EnglishSource, i);
            values.ForEach(d => Places.Add(d.Item1, d.Item2));
        });
        CreatG();

        List<(Vector2Int, char)> Acomadate(string Word, int Y) 
        {
            List<(Vector2Int, char)> Places = new List<(Vector2Int, char)>();
            int BackGaps = 0;
            for (int i = 0; i < 6 - Word.Length; i++) BackGaps += (Random.Range(0, 100) > 50) ? 1 : 0;
            int OldLenght = Word.Length;
            for (int i = 0; i < OldLenght; i++) 
            {
                Places.Add((new Vector2Int(BackGaps + i, Y), Word[0]));
                Word = Word.Remove(0, 1);
            }
            return Places;
        }


        // bitta katem bopqolishi keremas
        bool Rule_1(Vector2Int ToChake) 
        {
            return false;
        }


    }
    public GameObject PPCC;
    public GameObject Parrent;
    public void CreatG() 
    {
        Parrent.ClearChilds();
        Places.Keys.ToList().ForEach(K => {
            GameObject dd = Instantiate(PPCC, Parrent.transform);
            dd.transform.localPosition = new Vector3(K.x, K.y, 0);
            dd.GetComponentInChildren<TMP_Text>().text = Places[K].ToString();
        });
    }

}
