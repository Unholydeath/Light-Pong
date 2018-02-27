using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public enum eColor
    {
        RED,
        ORANGE,
        GREEN,
        BLUE,
        PURPLE
    }

    [SerializeField] eColor m_color;
    [SerializeField] GameObject m_light;
    [SerializeField] AudioClip m_hitSFX;
    [SerializeField] AudioClip m_destroySFX;
    
    Light m_lightObject = null;
    AudioSource m_audioSource;

    int m_maxHitPoints;
    int m_hitPoints;
    int m_pointValue;
    float m_lightIntensity = 3.0f;
    bool m_hasLight = false;
    
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_lightObject = m_light.GetComponent<Light>();

        switch (m_color)
        {
            case eColor.RED:
                m_maxHitPoints = 1;
                m_pointValue = 100;
                break;
            case eColor.ORANGE:
                m_maxHitPoints = 2;
                m_pointValue = 200;
                break;
            case eColor.GREEN:
                m_maxHitPoints = 3;
                m_pointValue = 300;
                break;
            case eColor.BLUE:
                m_maxHitPoints = 4;
                m_pointValue = 400;
                break;
            case eColor.PURPLE:
                m_maxHitPoints = 5;
                m_pointValue = 500;
                break;
        }
        m_hitPoints = m_maxHitPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            m_audioSource.clip = m_hitSFX;
            m_audioSource.Play();

            m_hitPoints--;            

            if(m_hitPoints <= 0)
            {
                Game.Instance.playerScore += m_pointValue;
                Game.Instance.playerScoreHUD.text = "Score: " + Game.Instance.playerScore;

                GetComponent<MeshRenderer>().enabled = false;
                m_audioSource.clip = m_destroySFX;
                m_audioSource.Play();

                CameraController.Instance.Shake(.3f);

                Destroy(gameObject, 0.5f);
            }
            else
            {
                if (!m_hasLight)
                {
                    m_light.SetActive(true);
                    m_hasLight = true;
                    m_lightObject.intensity = m_lightIntensity / (m_maxHitPoints - 1);
                }
                else
                {
                    m_lightObject.intensity += m_lightIntensity / (m_maxHitPoints - 1);
                }                
            }
        }
    }
}
