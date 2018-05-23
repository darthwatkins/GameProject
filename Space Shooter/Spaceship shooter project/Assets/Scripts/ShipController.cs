using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

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
    private float m_shotCoolDown;

    [SerializeField]
    private float m_shotSpeed;

    [SerializeField]
    private int m_weaponType; // 0 = standard, 1 = supershot, 2 = megashot

	// Use this for initialization
	void Start () {
        m_trailParticles.Stop();
	}

    // take damage 
    public void takeDamage(int damage) {


    }
	
	// Update is called once per frame
	void Update () {
        // handle input and movement
        HandleMovement();
        // handle firing
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

            // get speed
            float mag = Mathf.Abs(m_rigidBody.velocity.magnitude) / m_maxThrustSpeed;

            // ammount of acceleration to add on a curve limiting acceleration as we hit top speed
            float addtionalVelocity = (vSpeed * m_linearMultiplier) * (1f - Mathf.Pow(mag, 3));

            Debug.Log(1f - Mathf.Pow(mag, 3));

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
