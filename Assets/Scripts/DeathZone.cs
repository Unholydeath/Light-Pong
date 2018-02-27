using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] GameObject m_ballDeathParticles;
    [SerializeField] GameObject m_particleContainer;

    Vector3 rotation = new Vector3(270.0f, 0.0f, 0.0f);

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            GameObject particles = Instantiate(m_ballDeathParticles, collision.transform.position, Quaternion.Euler(rotation), m_particleContainer.transform);
            Destroy(particles, .5f);
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            collision.gameObject.GetComponent<Ball>().PlayDestroySound();
            Destroy(collision.gameObject, .5f);
        }
    }
}
