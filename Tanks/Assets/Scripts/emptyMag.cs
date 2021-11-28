using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/EmptyMag")]
    public class EmptyMag : ConditionBase
    {
        [InParam("tag")]
        public string tankTag;

        public override bool Check()
        {
            return (GameObject.FindWithTag(tankTag).GetComponent<mag>().shells == 0);
        }
    }
}