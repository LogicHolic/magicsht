using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toSingle : MonoBehaviour {
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
				if (collision.tag == "Player")
				{
						fade.FadeIn(1, () =>
						{
								fade.FadeOut(1);
								SceneManager.LoadScene("offlineStage1");
						});
				}
		}
}
