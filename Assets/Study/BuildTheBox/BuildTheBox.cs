using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using SystemBox.Simpls;
using TMPro;
using UnityEngine;

public class BuildTheBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Generator.Generate();


        CreatG();


    }

    public GameObject BackBoxPrefab;
    public GameObject BoxPrefab;
    public GameObject Parrent;
    public void CreatG() 
    {
        Parrent.ClearChilds();
        Generator.Gropes.ForEach((Grope, GropeIndex) => {
            GameObject GropeObject = new GameObject("Grope " + GropeIndex);
            GropeObject.transform.SetParent(Parrent.transform);
            Grope.ForEach(item => {
                GameObject dd = Instantiate(BoxPrefab, Parrent.transform);
                dd.name = item.Item2.ToString();
                dd.transform.localPosition = new Vector3(item.Item1.x, item.Item1.y, 0);
                dd.GetComponentInChildren<TMP_Text>().text = item.Item2.ToString();
                dd.transform.SetParent(GropeObject.transform);


                GameObject Bass = Instantiate(BackBoxPrefab, Parrent.transform);
                Bass.name = item.Item2.ToString();
                Bass.transform.localPosition = new Vector3(item.Item1.x, item.Item1.y, 1);

            });
        });

        

    }

    /*
    private void OnDrawGizmos()
    {
        Giz.ForEach((g, i) => 
        {
            Gizmos.color = new TList<Color>() 
            {Color.red, Color.white, Color.blue, Color.green, Color.gray, Color.black, Color.cyan, Color.magenta}[i, ListGetType.Loop]; 
            g.ForEach(d => Gizmos.DrawCube(Parrent.transform.position + new Vector3(d.x, d.y, 0) * .6f, new Vector3(.5f, .5f, .5f))); });
    }*/

}
