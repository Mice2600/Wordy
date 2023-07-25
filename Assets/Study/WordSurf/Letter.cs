using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Letter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public static TList<GameObject> Sellecting;

    public static Dictionary<Vector3Int, GameObject> Place;
    private Vector3 MovePos;
    [SerializeField, Required]
    private GameObject AvtiveObject;
    public void OnPointerDown(PointerEventData eventData)
    {
        Sellecting = new TList<GameObject>(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject == null) return;
        if (Sellecting.IsEnpty()) return;
        if (Sellecting.Contains(gameObject))
        {
            if(Sellecting.Count < 2) return;
            if(Sellecting.Last == gameObject) return;
            if (Sellecting[Sellecting.Count -2] != gameObject) return;
            Sellecting.RemoveLast();
            return; 
        }

        if (Vector3.Distance(transform.position, Sellecting.Last.transform.position) > 120) return;

        bool PseTrue = true;
        Sellecting.ForEach(s => { if (Mathf.Abs(s.transform.position.x - transform.position.x) > 10) PseTrue = false; });
        if (!PseTrue)
        {
            PseTrue = true;
            Sellecting.ForEach(s => { if (Mathf.Abs(s.transform.position.y - transform.position.y) > 10) PseTrue = false;});
        }
        if (!PseTrue) return;
        Sellecting.Add(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        string Text = "";
        Sellecting.ForEach(a => Text += a.GetComponentInChildren<TextMeshProUGUI>().text);
        
        if (Builder.WordsSS.Contains(Text)) Sellecting.DestroyAll();
        Builder.WordsSS.Remove(Text);
        Sellecting = new TList<GameObject>();
    }
    WordSurf Builder;
    [ShowInInspector]
    public static List<Letter> ReadyToDo;
    void Start()
    {
        ReadyToDo = new List<Letter>();
        Builder = FindObjectOfType<WordSurf>();
        Sellecting = new List<GameObject>();
        MovePos = transform.position;
        if (Place == null)
            Place = new Dictionary<Vector3Int, GameObject>();
        List<GameObject> Values = new List<GameObject>(Place.Values);
        bool IsthereBug = Values.Count == 0;
        for (int i = 0; i < Values.Count; i++)
            if (Values[i] == null) { IsthereBug = true; break; }
        if (IsthereBug) Place = new Dictionary<Vector3Int, GameObject>();
        Place.Add(transform.position.ToInt(), gameObject);

        StartCoroutine(CheakUpdate());
        IEnumerator CheakUpdate() 
        {
            while (true) 
            {
                yield return new WaitForSeconds(Random.Range(.5f, 2f));
                ChackWords();
            }
        }
    }



    void Update()
    {
        if (transform.position == MovePos) 
        {
            Vector3Int DownPos = transform.position.ToInt() - new Vector3Int(0, 100, 0);
            List<Vector3Int> Keys = new List<Vector3Int>(Place.Keys);
            Vector3Int? NewarOne = Keys.Find((p) => Vector3Int.Distance(DownPos, p) < 10);
            if (NewarOne != null && Place.ContainsKey(NewarOne.Value) && Place[NewarOne.Value] == null)
            {
                Vector3Int? MyPos = Keys.Find((p) => Vector3Int.Distance(transform.position.ToInt(), p) < 10);
                if (MyPos != null && Place.ContainsKey(MyPos.Value) && Place[MyPos.Value] == gameObject)
                {
                    Place[MyPos.Value] = null;
                    Place[NewarOne.Value] = gameObject;
                    MovePos = NewarOne.Value;
                }
            }
        }
        AvtiveObject.SetActive(Sellecting.Contains(gameObject));
        transform.position = Vector3.MoveTowards(transform.position, MovePos, Time.deltaTime * 2000f);
    }




    private void OnDestroy()
    {
        ReadyToDo.Remove(this);
    }

    public void ChackWords() 
    {
        ReadyToDo.Remove(this);
        string MyLetter = GetComponentInChildren<TextMeshProUGUI>().text;
        for (int i = 0; i < Builder.WordsSS.Count; i++)
        {
            if(Builder.WordsSS[i][0].ToString() == MyLetter)
            {
                if(ChackFor(Builder.WordsSS[i]))    
                {
                    ReadyToDo.Add(this);
                    
                }
            }
        }        
    }
    public bool ChackFor(string Word) 
    {
        if (Cheak(gameObject, Word, new Vector3Int(0, 100, 0)) || 
            Cheak(gameObject, Word, new Vector3Int(0, -100, 0)) || 
            Cheak(gameObject, Word, new Vector3Int(100, 0, 0)) || 
            Cheak(gameObject, Word, new Vector3Int(-100, 0, 0))) 
        {
            return true;
        }
        return false;

        bool Cheak(GameObject ToCheak, string WordCheaking, Vector3Int Side) 
        {
            if (ToCheak.GetComponentInChildren<TextMeshProUGUI>().text == WordCheaking[0].ToString())
            {
                if (WordCheaking.Length == 1) return true;
                Vector3Int Pos = ToCheak.transform.position.ToInt() + Side; 
                List<Vector3Int> Keys = new List<Vector3Int>(Place.Keys);
                Vector3Int? NewarOne = Keys.Find((p) => Vector3Int.Distance(Pos, p) < 10);
                if (NewarOne != null && Place.ContainsKey(NewarOne.Value) && Place[NewarOne.Value] != null) 
                {
                    return Cheak(Place[NewarOne.Value], WordCheaking.Remove(0, 1), Side);
                }
                else return false;
            }
            return false;
        }
    }
}
