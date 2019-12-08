using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {

    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private float m_maxDistance = 500;
    [SerializeField] private int m_interactableLayer;
    [SerializeField] private FishActor m_mainFish = null;
    [SerializeField] private Vector3 frontMiddlePoint;

    // Use this for initialization
    void Start () {
        m_mainFish = FindObjectOfType<FishActor>();
        if (m_mainCamera == null)
            m_mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        if (m_mainCamera == null)
        {
            Debug.Log("Main camera is null");
            return;
        }

        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 mousePos = Input.mousePosition;

        //    // Use the current camera to convert it to a ray
        //    Ray mouseRay = m_mainCamera.ScreenPointToRay(mousePos);

        //    // finds the distance along a ray
        //    RaycastHit hit;
        //    if (Physics.Raycast(mouseRay, out hit, m_maxDistance, 1 << m_interactableLayer))
        //    {
        //        m_mainFish.player = hit.collider.gameObject;
        //    }
        //    else
        //    {
        //        Plane playerPlane = new Plane(Vector3.forward, frontMiddlePoint);

        //        // finds the distance along a ray
        //        float rayDistance = 0;
        //        playerPlane.Raycast(mouseRay, out rayDistance);

        //        // find the collision point from the distance
        //        Vector3 collisionPoint = mouseRay.GetPoint(rayDistance);

        //        transform.position = collisionPoint;
        //        m_mainFish.player = gameObject;
        //    }
        //}
    }
}
