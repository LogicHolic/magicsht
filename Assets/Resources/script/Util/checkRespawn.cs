using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRespawn : MonoBehaviour
{
		[SerializeField]
		Fade fade;

		Vector3 checkPos;
		Animator ani;
		Rigidbody rb;
		// Use this for initialization
		void Start()
		{

				ani = GetComponent<Animator>();
				rb = GetComponent<Rigidbody>();
		}

		// Update is called once per frame
		void Update()
		{

		}
		private void OnTriggerEnter(Collider other)
		{
				if (other.gameObject.tag == "Ereki")
				{
						StartCoroutine("respawn", checkPos);
				}
		}

		IEnumerator respawn(Vector3 pos)
		{
				GetComponent<MoveCon>().canMove = false;
				ani.SetBool("isDead", true);
				rb.velocity = new Vector3(0, 0, 0);
				fade.FadeIn(1f, () =>
				{
						fade.FadeOut(1);
				});
				yield return new WaitForSeconds(1f);

				transform.position = pos;
				yield return new WaitForSeconds(1f);

				ani.SetBool("isDead", false);
				GetComponent<MoveCon>().canMove = true;
		}
}