using System.Collections;
using System.Collections.Generic;
using SystemBox.Simpls;
using UnityEngine;
namespace Study.TwoWordSystem
{
    public class VisualThings : MonoBehaviour
    {
        private TwoWordSystemContent twoWordSystemContent => _twoWordSystemContent ??= GetComponent<TwoWordSystemContent>();
        private TwoWordSystemContent _twoWordSystemContent;
        private Animator animator => _animator ??= GetComponent<Animator>();
        private Animator _animator;
        
        void Update()
        {
            animator.SetBool("Dead", twoWordSystemContent.Dead);
            if (twoWordSystemContent.Dead) 
            {
                animator.SetFloat("Sellected", 0);
                return; 
            }
            animator.SetFloat("Sellected", TMath.MoveTowards(animator.GetFloat("Sellected"), IsSellected() ? 1f : 0f, Time.deltaTime * 20f)); 
            bool IsSellected() => (TwoWordSystemContent.EnglishSellected == twoWordSystemContent || TwoWordSystemContent.RussianSellected == twoWordSystemContent);
        }
    }
}