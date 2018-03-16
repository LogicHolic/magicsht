using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countdownStart : MonoBehaviour {
		[SerializeField]
	 GameObject canvas;
		[SerializeField]
		GameObject text;

	// Use this for initialization
	void Start () {
				canvas.SetActive(false);
				text.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
