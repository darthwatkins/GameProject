using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    [SerializeField]
    protected float m_hitPoints;
    public float HitPoints { get { return m_hitPoints; } }

    [SerializeField]
    private GameObject m_destructionEffectPrefab;

    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    private float m_blinkTime = .3f;
    private float m_currentBlinkLerp = 1f;

    public void TakeDamage(float damage) {
        //Start the blink
        m_currentBlinkLerp = 0f;

        //Lower hit points
        m_hitPoints -= damage;

        //Die if we're out of hit points
        if (m_hitPoints <= 0f)
            Die();
    }

    protected void Update() {
        //If we are not finished lerping
        if (m_currentBlinkLerp < 1f) {
            //Increment lerp
            m_currentBlinkLerp += Time.deltaTime * (1f / m_blinkTime);

            //Set lerp value to 1 if we've crossed 1
            if (m_currentBlinkLerp >= 1f) m_currentBlinkLerp = 1f;

            //Lerp the color of the sprite renderer from red to white
            m_spriteRenderer.color = Color.Lerp(Color.red, Color.white, m_currentBlinkLerp);
        }
    }

    public virtual void Die() {
        //Check if the prefab is null, if it's not: Spawn
        if (m_destructionEffectPrefab != null)
            Instantiate(m_destructionEffectPrefab, transform.position, Quaternion.identity);
    }
}
