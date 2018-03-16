using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour {
		[SerializeField]
		Fade fd = null;
	// Use this for initialization
	void Start () {
				fd.FadeIn(1f, () =>
				{
						fd.FadeOut(1);
				});
		}
	
	// Update is called once per frame
	void Update () {
		
	}
}
