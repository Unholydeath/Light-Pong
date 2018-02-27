using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 10.0f)] float m_ballSpeed = 3.0f;
    [SerializeField] AudioClip m_hitSFX;
    [SerializeField] AudioClip m_destroySFX;

    Vector3 velocity = Vector3.zero;
    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void MoveBall()
    {
        velocity.y = -m_ballSpeed;
    }

    void Update()
    {
        transform.position = transform.position + (velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        velocity = velocity - (2 * (Vector3.Dot(velocity, contactPoint.normal)) * contactPoint.normal);
        m_audioSource.clip = m_hitSFX;
        m_audioSource.Play();
    }

    public void PlayDestroySound()
    {
        m_audioSource.clip = m_destroySFX;
        m_audioSource.Play();
    }
}
