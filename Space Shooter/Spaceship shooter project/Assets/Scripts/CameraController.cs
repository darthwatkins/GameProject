using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject m_playerShip;
	
	// Update is called once per frame
	void Update () {

        // lock the camera to the player ship position but not depth or rotation
        transform.position = new Vector3(m_playerShip.transform.position.x, m_playerShip.transform.position.y, -1f);

        //Debug.Log("X=" + transform.position.x + ": Y=" + transform.position.y);
	}
}
