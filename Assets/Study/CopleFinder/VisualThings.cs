using SystemBox.Simpls;
using UnityEngine;
namespace Study.CopleFinder
{
    public class VisualThings : MonoBehaviour
    {
        private CopleFinderContent twoWordSystemContent => _twoWordSystemContent ??= GetComponent<CopleFinderContent>();
        private CopleFinderContent _twoWordSystemContent;
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
            bool IsSellected() => (CopleFinderContent.FirstSellected == twoWordSystemContent || CopleFinderContent.SecondSellected == twoWordSystemContent);
        }
    }
}