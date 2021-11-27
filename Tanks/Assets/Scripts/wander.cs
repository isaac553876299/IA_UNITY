using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Actions/Wander")]
    public class Wander : GOAction
    {
        private UnityEngine.AI.NavMeshAgent navAgent;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
            //navAgent.SetDestination(target);

#if UNITY_5_6_OR_NEWER
                navAgent.isStopped = false;
#else
            navAgent.Resume();
#endif
        }

        /// <summary>Method of Update of MoveToPosition </summary>
        /// <remarks>Check the status of the task, if it has traveled the road or is close to the goal it is completed
        /// and otherwise it will remain in operation.</remarks>
        Vector3 wanderTarget = Vector3.zero;
        public override TaskStatus OnUpdate()
        {
            float wanderRadius = 10;
            float wanderDistance = 10;
            float wanderJitter = 1;

            wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                            0,
                                            Random.Range(-1.0f, 1.0f) * wanderJitter);
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

            navAgent.SetDestination(targetWorld); //Seek();

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