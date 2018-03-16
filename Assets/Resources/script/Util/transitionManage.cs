using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionManage : MonoBehaviour {
		[SerializeField]
		Fade fade = null;

	// Use this for initialization
	void Start () {
				fade.FadeIn(1f, () =>
				{
						fade.FadeOut(1);
				});
		}
	
	// Update is called once per frame
	void Update () {
		
	}
}
