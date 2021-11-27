using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/Ready2Shoot")]
    public class Ready2Shoot : ConditionBase
    {
        [InParam("force")]
        public int f;
        [InParam("transform")]
        public Transform ft;
        [InParam("target")]
        public Transform t;

        public override bool Check()
        {
            return true;
            float maxDist = f * Mathf.Cos(0.79f);
            return (maxDist > Vector3.Distance(ft.position, t.position));
        }
    }
}