using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/EmptyMag")]
    public class EmptyMag : ConditionBase
    {


        public override bool Check()
        {
            return false;
        }
    }
}