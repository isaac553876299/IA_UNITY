using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Actions/Shoot")]
    public class Shoot : GOAction
    {
        [InParam("target")]
        public Transform target;
        [InParam("prefab")]
        public GameObject prefab;
        [InParam("transform")]
        public Transform m_FireTransform;

        public AudioSource m_ShootingAudio;
        public AudioClip m_ChargingClip;
        public AudioClip m_FireClip;

        [InParam("force")]
        public float m_LaunchForce;
        [InParam("mag")]
        public int shells;
        [OutParam("mago")]
        public int shellso;

        public override void OnStart()
        {
            Debug.Log("fire!");
            Fire();
        }

        public override TaskStatus OnUpdate()
        {
            //Fire();
            //return TaskStatus.RUNNING;
            return TaskStatus.COMPLETED;
        }

        public override void OnAbort()
        {

        }

        private void Fire()
        {
            const float g = 10f;
            float v = m_LaunchForce;
            float x = target.position.x;
            float y = target.position.y;
            float angle = Mathf.Atan(Mathf.Pow(v, 2) + Mathf.Sqrt(Mathf.Pow(v, 4) - g * (g * Mathf.Pow(x, 2) + 2 * y * Mathf.Pow(v, 2))) / (g * x));
            Debug.Log(angle);

            GameObject.Instantiate(prefab, m_FireTransform.position, Quaternion.identity);
            prefab.GetComponent<Rigidbody>().velocity = m_LaunchForce * m_FireTransform.forward;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            --shells;
            shellso = shells;
        }
    }
}