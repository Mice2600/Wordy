using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Study.BuildDialog
{
    public class ApplayButton : MonoBehaviour
    {
        public BuildDialogVewe BuildDialogVewe;
        private void Start()
        {
            BuildDialogVewe = GetComponentInParent<BuildDialogVewe>();
            GetComponent<Button>().onClick.AddListener(OnButton);
        }
        private void OnButton() 
        {
            if(BuildDialogVewe)
        }
        void Update()
        {
            
        }
    }
}
