using UnityEngine;
using UnityEngine.UI;
using System;

public class fire : MonoBehaviour
{
    public Transform target;
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_LaunchForce = 30f;
    public int shells = 3;

    private bool m_Fired = false;                       // Whether or not the shell has been launched with this button press.

    private void Start()
    {

    }

    private void Update()
    {
        if(!m_Fired) Fire();
    }

    private void Fire()
    {
        float maxDist = m_LaunchForce * Mathf.Cos(0.79f);

        if (maxDist > Vector3.Distance(m_FireTransform.position, target.position))
        {
            /*const float g = -10f;
            float v = m_LaunchForce;
            float x = Mathf.Abs(target.position.x - m_FireTransform.position.x);
            float y = Mathf.Abs(target.position.y - m_FireTransform.position.y);
            Vector2 v2 = new Vector2(x, y);
            float m = v2.magnitude;
            float angle = Mathf.Atan(Mathf.Pow(v, 2) + Mathf.Sqrt(Mathf.Pow(v, 4) - g * (g * Mathf.Pow(x, 2) + 2 * y * Mathf.Pow(v, 2))) / (g * x));*/

            float v = m_LaunchForce;
            float x = Mathf.Abs(target.position.x - m_FireTransform.position.x);
            float y = Mathf.Abs(target.position.y - m_FireTransform.position.y);
            float z = Mathf.Abs(target.position.z - m_FireTransform.position.z);
            Vector2 v2 = new Vector2(x, z);
            float m = v2.magnitude;
            float insideroot = Mathf.Abs(Mathf.Pow(v, 4) - (Physics.gravity.y * (Physics.gravity.y * Mathf.Pow(m, 2))));
            float tan = (Mathf.Pow(v, 2) + Mathf.Sqrt(insideroot)) / (Physics.gravity.y * m);
            float angle = Mathf.Atan(Mathf.Abs(tan));

            Debug.Log(angle);
            Debug.Log(Mathf.Sin(angle));
            Debug.Log(Mathf.Cos(angle));

            m_Fired = true;

            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position,
                m_FireTransform.rotation) as Rigidbody;

            Vector3 velocity = new Vector3(0f, m_LaunchForce * Mathf.Sin(angle), m_LaunchForce * Mathf.Cos(angle));
            velocity = shellInstance.transform.TransformDirection(velocity);
            shellInstance.AddForce(velocity, ForceMode.Impulse);

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            --shells;
        }
    }
}