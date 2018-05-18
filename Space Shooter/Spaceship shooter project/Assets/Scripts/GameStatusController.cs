using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusController : MonoBehaviour {

    [SerializeField]
    private int m_lives;
    public int Lives { get { return m_lives; } set { m_lives = value; } }

    [SerializeField]
    private int m_initialHitPoints;

    [SerializeField]
    private int m_hitPoints;
    public int CurrentHitPoints { get { return m_hitPoints; } set { m_hitPoints = value; } }

    [SerializeField]
    private int m_waveNumber;
    public int CurrentWave { get { return m_waveNumber; } set { m_waveNumber = value; } }

    [SerializeField]
    private int m_score;
    public int Score { get { return m_score; } set { m_lives = value; } }

    // reset hitpoints back to initial value
    public void ResetHitPoints() {
        m_hitPoints = m_initialHitPoints;
    }
}
