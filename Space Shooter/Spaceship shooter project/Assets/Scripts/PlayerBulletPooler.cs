using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPooler : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerBulletPrefab;

    // this is the stock of player bullet prefabs
    private List<GameObject> m_playerBulletPool = new List<GameObject>();

    // Debug function to check current list size.
    public void GetCount() {
        Debug.Log("Current list size: " + m_playerBulletPool.Count);
    }

    // either generate or retrieve a player bullet from the list and return it.
    public GameObject GetPlayerBullet() {

        if (m_playerBulletPool.Count > 0) {
            //Store bullet for a moment
            GameObject bulletToReturn = m_playerBulletPool[0];

            //Remove bullet from pool
            m_playerBulletPool.RemoveAt(0);

            // check for multiple run phantom bullets and ignore if so - run a recursion on this function,
            // not perfect but then I'm not entirely sure that this isn't being caused by the Unity build 
            // environment as the initial list size is always 0 on a fresh load in
            if (!bulletToReturn) return GetPlayerBullet();

            //Re-enable bullet
            bulletToReturn.SetActive(true);

            //Return bullet
            return bulletToReturn;
        }
        else {
            GameObject newBullet = Instantiate(m_playerBulletPrefab);
            newBullet.GetComponent<BulletScript>().Initialize(this);
            return newBullet;
        }
    }
    public void ReturnPlayerBulletToPool(GameObject bullet) {
        //Disable bullet
        bullet.SetActive(false);

        //Add to pool
        m_playerBulletPool.Add(bullet);
    }
}
