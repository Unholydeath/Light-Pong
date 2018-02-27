using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitle : MonoBehaviour {

    [SerializeField] Transform m_startPosition = null;
    [SerializeField] Transform m_endPosition = null;
    [SerializeField] [Range(0.0f, 5.0f)] float m_time = 1.0f;

    float m_timer = 0.0f;

    void Update()
    {
        m_timer = m_timer + Time.deltaTime;
        float t = m_timer / m_time;
        t = Mathf.Clamp01(t);
        float interp = Interpolation.BounceOut(t);
        transform.position = Vector3.LerpUnclamped(m_startPosition.position, m_endPosition.position, interp);
    }
}
