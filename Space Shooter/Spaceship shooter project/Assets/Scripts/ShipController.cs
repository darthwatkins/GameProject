using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : Damageable {

    // Thrust settings
    [SerializeField]
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private float m_angularMultiplier;

    [SerializeField]
    private float m_linearMultiplier;

    [SerializeField]
    private float m_maxThrustSpeed;

    [SerializeField]
    private ParticleSystem m_trailParticles;

    // Firing setting
    [SerializeField]
    private float m_shotsPerSecond;
    private float m_shotCoolDown = 0f;

    [SerializeField]
    private float m_shotSpeed;

    [SerializeField]
    private int m_weaponType; // 0 = standard, 1 = supershot, 2 = megashot

    [SerializeField]
    private PlayerBulletPooler m_playerBulletPooler;

    [SerializeField]
    private GameObject m_muzzleFlash;

    [SerializeField]
    private List<GameObject> m_firePoints;
    private int m_fireIndex = 0;

    [SerializeField]
    private int m_health;

    [SerializeField]
    private int m_lives;

    // Use this for initialization
    void Start () {
        // stop the engine particle system playing
        m_trailParticles.Stop();

        // debug bullet pooler list size
        //m_playerBulletPooler.GetCount();
    }

    // take damage 
    public void TakeDamage(int damage) {


    }
	
	// Update is called once per frame
	new void Update () {
        // derrive from base class
        base.Update();
        // handle input and movement
        HandleMovement();
        // handle firing
        HandleFiring();
    }

    private void HandleFiring() {

        // check for fire button press#
        if (m_shotCoolDown > 0f) {
            m_shotCoolDown -= Time.deltaTime;
        }
        if (m_shotCoolDown <= 0f && Input.GetKeyDown(KeyCode.Z)) {

            // spawn new bullet
            GameObject bullet = m_playerBulletPooler.GetPlayerBullet();
            bullet.GetComponent<BulletScript>().Fire();
            bullet.transform.position = m_firePoints[m_fireIndex].transform.position;
            bullet.transform.rotation = m_firePoints[m_fireIndex].transform.rotation;
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * m_shotSpeed;

            // set muzzle flash to fired cannon
            m_muzzleFlash.SetActive(true);
            m_muzzleFlash.transform.position = m_firePoints[m_fireIndex].transform.position;
            m_muzzleFlash.transform.rotation = m_firePoints[m_fireIndex].transform.rotation;

            // cycle fire points
            m_fireIndex++;
            if (m_fireIndex >= m_firePoints.Count) m_fireIndex = 0;

            // set cooldown
            m_shotCoolDown = 1f / m_shotsPerSecond;


        }
        else {
            // hide muzzle flash
            m_muzzleFlash.SetActive(false);
        }
    }

    // movement mechanics for the ship object.
    private void HandleMovement() {

        // keyboard controls == 0?
        if (PlayerSettingsScript.Instance.Controller == 0) {

            // turning speed
            float hSpeed = Input.GetAxis("Horizontal");
            m_rigidBody.AddTorque(-(hSpeed * m_angularMultiplier));

            // thrust
            // get input
            float vSpeed = Input.GetAxis("Vertical");
            // remove reverse
            if (vSpeed < 0) vSpeed = 0;

            // get speed
            float mag = Mathf.Abs(m_rigidBody.velocity.magnitude) / m_maxThrustSpeed;

            // ammount of acceleration to add on a curve limiting acceleration as we hit top speed
            float addtionalVelocity = (vSpeed * m_linearMultiplier) * (1f - Mathf.Pow(mag, 3));

           // Debug.Log(1f - Mathf.Pow(mag, 3));

            // add force to relative y axis of ship
            m_rigidBody.AddRelativeForce(new Vector2(0f, addtionalVelocity));

            // turn off/on engine trail particles if enabled in the PlayerSettignsScript
            if (PlayerSettingsScript.Instance.Particles && addtionalVelocity > 0) {
                m_trailParticles.Play();
            }
            else if (m_trailParticles.isPlaying)  m_trailParticles.Stop();
        }
    }
}
