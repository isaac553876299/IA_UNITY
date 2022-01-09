using Pada1.BBCore;
using UnityEngine;
using System.Linq;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance.
    /// </summary>
    [Condition("Perception/IsTargetClose")]
    [Help("Checks whether a target is close depending on a given distance")]
    public class IsTargetClose : GOCondition
    {
        ///<value>Input Target Parameter to to check the distance.</value>
        [InParam("target")]
        [Help("Target to check the distance")]
        public GameObject target;

        ///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        public float closeDistance;

        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
		{
            if((gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cop").Where(x => x.GetComponent<Moves>().found == false).Count(); i++)
                {
                    GameObject.FindGameObjectsWithTag("Cop").Where(x => x.GetComponent<Moves>().found == false).ElementAt(i).GetComponent<Moves>().found = true;
                }
            }
            return (gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance;
		}
    }
}