using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/Ready2Shoot")]
    public class Ready2Shoot : ConditionBase
    {
        [InParam("force")]
        public float f;
        [InParam("transform")]
        public Transform ft;
        [InParam("target")]
        public Transform t;
        [InParam("tag")]
        public string tankTag;

        public override bool Check()
        {
            float maxDist = 30 * Mathf.Cos(0.79f);
            return ((GameObject.FindWithTag(tankTag).GetComponent<mag>().shells > 0)
                &&(maxDist > Vector3.Distance(ft.position, t.position)));
        }
    }
}