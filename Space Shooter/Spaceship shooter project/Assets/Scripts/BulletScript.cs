using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // if time to live is too short then the bullet gets re-used too quickly and the trails glitch.  
    // 1.5s TTL seems okay at 10 bullets a second = simplest solution
    [SerializeField]
    private float m_timeToLive;
    private float m_timeLeftToLive;

    [SerializeField]
    private TrailRenderer m_bulletTrail;

    private PlayerBulletPooler m_bulletPooler;
    public PlayerBulletPooler Pooler { set { m_bulletPooler = value; } }

    public void Initialize(PlayerBulletPooler pooler) {
        m_bulletPooler = pooler;
    }

    /// <summary>
    /// Fire the bullet
    /// </summary>
    public void Fire() {
        m_timeLeftToLive = m_timeToLive;

        // set or disable trail based on prefs
        if (PlayerSettingsScript.Instance.Particles) m_bulletTrail.enabled = true;
        else m_bulletTrail.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        // after a short TTL the bullets are considered void and returned to the bullet pooler
        m_timeLeftToLive -= Time.deltaTime;
        if (m_timeLeftToLive < 0) m_bulletPooler.ReturnPlayerBulletToPool(gameObject);
    }
}
