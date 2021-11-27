using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/EmptyMag")]
    public class EmptyMag : ConditionBase
    {
        [InParam("mag")]
        public int mag;

        public override bool Check()
        {
            return (mag == 0);
        }
    }
}