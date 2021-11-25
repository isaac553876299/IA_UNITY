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
            const float g = 10f;
            float v = m_LaunchForce;
            float x = target.position.x;
            float y = target.position.y;
            float angle = Mathf.Atan(Mathf.Pow(v, 2) + Mathf.Sqrt(Mathf.Pow(v, 4) - g * (g * Mathf.Pow(x, 2) + 2 * y * Mathf.Pow(v, 2))) / (g * x));
            Debug.Log(angle);
            m_Fired = true;

            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position,
                m_FireTransform.rotation) as Rigidbody;

            shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            --shells;
        }
    }
}