using Base;
namespace Servises.SmartText
{
    public class IrregularText : ContentText
    {
        [UnityEngine.SerializeField]
        private string Ofset_1;
        [UnityEngine.SerializeField]
        private string Ofset_2;
        public override string GetValue(Content Object)
        {
            if (Object == null) return "";
            IIrregular s = Object as IIrregular;
            return s.BaseForm + Ofset_1 + s.SimplePast + Ofset_2 + s.PastParticiple;
        }
    }
}