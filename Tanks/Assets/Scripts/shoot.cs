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
        [InParam("tag")]
        public string tankTag;

        [InParam("m_ShootingAudio")]
        public AudioSource m_ShootingAudio;

        [InParam("m_FireClip")]
        public AudioClip m_FireClip;

        [InParam("force")]
        public float m_LaunchForce;

        public override void OnStart()
        {

        }

        public override TaskStatus OnUpdate()
        {
            Fire();
            GameObject.FindWithTag(tankTag).GetComponent<mag>().time = Time.time;
            return TaskStatus.COMPLETED;
        }

        public override void OnAbort()
        {

        }

        private void Fire()
        {
            float v = m_LaunchForce;

            float targetX = (target.position.x - m_FireTransform.position.x);
            float targetZ = (target.position.z - m_FireTransform.position.z);
            float targetY = (target.position.y - m_FireTransform.position.y);
            Vector2 targetXZPos = new Vector2(targetX, targetZ);

            float square = Mathf.Abs(Mathf.Pow(v, 4) - (Physics.gravity.y * (Physics.gravity.y * (Mathf.Pow(Mathf.Abs(targetXZPos.magnitude), 2)) + (2 * targetY * Mathf.Pow(v, 2)))));

            float angle = (Mathf.Pow(v, 2) + Mathf.Sqrt(square)) / (Physics.gravity.y * Mathf.Abs(targetXZPos.magnitude));

            float finalAngle = Mathf.Atan(Mathf.Abs(angle));

            GameObject bullet = GameObject.Instantiate(prefab, m_FireTransform.position, Quaternion.identity) as GameObject;

            float vz = Mathf.Cos(finalAngle) * v;
            float vy = Mathf.Sin(finalAngle) * v;
            Vector3 localv = new Vector3(0f, vy, vz);
            Vector3 velocity = m_FireTransform.TransformDirection(localv);
            bullet.GetComponent<Rigidbody>().velocity = velocity;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            GameObject.FindWithTag(tankTag).GetComponent<mag>().use();
        }
    }
}