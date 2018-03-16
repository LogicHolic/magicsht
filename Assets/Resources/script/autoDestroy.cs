using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour {

		[SerializeField]
		float destroyFrame;

	// Use this for initialization
	void Start () {
    Destroy(this.gameObject, destroyFrame);
      }
	
	// Update is called once per frame
	void Update () {
		
	}
}
