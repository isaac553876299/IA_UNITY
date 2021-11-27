using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Actions/Patrol")]
    public class Patrol : GOAction
    {
        private UnityEngine.AI.NavMeshAgent navAgent;

        [InParam("wp1")]
        public Vector3 wp1;
        [InParam("wp2")]
        public Vector3 wp2;
        [InParam("wp3")]
        public Vector3 wp3;
        [InParam("wp4")]
        public Vector3 wp4;
        [InParam("wp5")]
        public Vector3 wp5;

        public Vector3[] waypoints;
        int patrolWP = -1;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }

            waypoints = new Vector3[5];
            waypoints[0] = wp1;
            waypoints[1] = wp2;
            waypoints[2] = wp3;
            waypoints[3] = wp4;
            waypoints[4] = wp5;
            

#if UNITY_5_6_OR_NEWER
                navAgent.isStopped = false;
#else
            navAgent.Resume();
#endif
        }

        public override TaskStatus OnUpdate()
        {
            if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
            {
                patrolWP = (patrolWP + 1) % waypoints.Length;
                navAgent.destination = (waypoints[patrolWP]);
            }

            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
                return TaskStatus.COMPLETED;

            return TaskStatus.RUNNING;
        }

        public override void OnAbort()
        {
#if UNITY_5_6_OR_NEWER
            if(navAgent!=null)
                navAgent.isStopped = true;
#else
            if (navAgent != null)
                navAgent.Stop();
#endif
        }
    }
}
