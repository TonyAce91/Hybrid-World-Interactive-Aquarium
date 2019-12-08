using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleScript : MonoBehaviour {

    public bool castleTouched = false;
	[SerializeField] private float shrinkSpeed = 0.5f;
	[SerializeField] private float shrinkBuffer;
	[SerializeField] private float growSpeed = 0.5f;
	[SerializeField] private float growBuffer;
	[SerializeField] private float m_timer = 10.0f;
    public List<Transform> waypoints = new List<Transform>();

    private float m_time = 0;
	[SerializeField] private float scale = 0.7f;
	private new Vector3 originalScale;
	[SerializeField]private bool shrink = false;

	private void Start()
	{
		originalScale = transform.localScale;
	}

    private void Update()
    {

		//--------------------------------------shrinking stuff :)---------------------------------------------------------------------
		if (shrink && transform.localScale.x >= scale + shrinkBuffer) {
			transform.localScale = Vector3.Slerp (transform.localScale, new Vector3 (scale, scale, scale), shrinkSpeed * Time.deltaTime);
		} else if (shrink && transform.localScale.x <=scale + shrinkBuffer) 
		{
			shrink = false;
		}

		//---------------------------------------Growing Stuff :)-----------------------------------------------------------------------
		if (!shrink && transform.localScale.x <= originalScale.x - growBuffer) {
			transform.localScale = Vector3.Slerp (transform.localScale, originalScale, growSpeed * Time.deltaTime);
		} else if (!shrink && transform.localScale.x >= originalScale.x - growBuffer) 
		{
			transform.localScale = originalScale;
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger detected");
		if (other.tag == "Player Hand") {
				shrink = true;
				//start getting smaller
			castleTouched = true;
			m_time = m_timer;

		}
		
    }

}
