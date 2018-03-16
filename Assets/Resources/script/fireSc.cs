using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSc : MonoBehaviour
{
		int touchObj = 0;
		public GameObject manager;
		GameObject player;
		Vector3 firePos;

		Vector3 vec;

		public GameObject Explosion;
		public GameObject Burned;
		public GameObject Fire;

		int count;

		// Use this for initialization
		void Start()
		{
				manager = GameObject.Find("fireManager");
				player = GameObject.FindGameObjectWithTag("Player");

				vec = player.GetComponent<ShotScript>().throwTargetVec;

				count = 0;
		}

		// Update is called once per frame
		void Update()
		{
				if (touchObj == 0)
				{
						count++;
						transform.position += transform.forward * Time.deltaTime * 20;

						if (count > 30)
						{
								Destroy(gameObject);
						}
				}
				else if (touchObj == 1)
				{
						StartCoroutine("destroySelf");
						touchObj = 2;
				}

		}

		private void OnCollisionEnter(Collision collision)
		{
				touchObj = 1;

				if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "floor")
				{
						foreach (var p in collision.contacts)
						{
								firePos = p.point;
								manager.GetComponent<FireInfo>().SendMessage("addPos", firePos);
						}
				}
				if (collision.gameObject.tag == "Enemy")
				{
						collision.gameObject.GetComponent<EnemySc>().armorPoint -= 5;
				}
		}

		IEnumerator destroySelf()
		{
				Instantiate(Explosion, transform.position, transform.rotation);
				Instantiate(Burned, transform.position, transform.rotation);
				Destroy(Fire);

				gameObject.GetComponent<MeshRenderer>().enabled = false;
				yield return new WaitForSeconds(10);
				gameObject.GetComponent<SphereCollider>().enabled = false;
				manager.GetComponent<FireInfo>().SendMessage("popPos", firePos);
				Destroy(gameObject);
		}

		private void OnTriggerStay(Collider other)
		{
				if (other.gameObject.tag == "Hole")
				{
						if (other.GetComponent<holeSc>().parent.tag == "wall")
								gameObject.layer = 14;
						if (other.GetComponent<holeSc>().parent.tag == "floor")
								gameObject.layer = 15;
				}
		}
		private void OnTriggerExit(Collider other)
		{
				if (other.gameObject.tag == "Hole")
				{
						gameObject.layer = 9;
				}
		}
}