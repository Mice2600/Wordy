using Base.Word;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelletAllFunction : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnButton()
    {
        WordBase.Wordgs.Clear();
		try
		{
            FindObjectOfType<Servises.BaseList.BaseListViwe>().Refresh();
        }
		catch (System.Exception)
		{

		}
        
    }

}
