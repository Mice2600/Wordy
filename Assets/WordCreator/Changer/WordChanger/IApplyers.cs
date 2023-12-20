using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WordCreator.WordCretor
{
    public interface IApplyers
    {
        public void TryApply(Content content);
    }
}