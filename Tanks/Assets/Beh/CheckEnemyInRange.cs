using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Beh/CheckEnemyInRange")]
    public class CheckEnemyInRange : ConditionBase
    {
        [InParam("target")]
        public Transform target;

        [InParam("fire_transf")]
        public Transform fire_transf;

        [InParam("maxDist")]
        public float maxDist;

        public override bool Check()
        {
            return (maxDist > Vector3.Distance(
                fire_transf.position,
                target.position));
        }
    }
}