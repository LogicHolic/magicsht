﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toMultiScene : MonoBehaviour {

		[SerializeField]
		Fade fade = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		private void OnTriggerEnter(Collider collision)
		{
				if(collision.gameObject.tag == "Player")
				{
						fade.FadeIn(3, () =>
						{
								SceneManager.LoadScene("NetworkTest");
								//fade.FadeOut(1);
						});
						
				}
		}
}
