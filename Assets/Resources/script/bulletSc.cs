using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSc : MonoBehaviour
{

		public GameObject explosion;

		public int damage = 35;

		private RaycastHit hit;

		// Use this for initialization
		void Start()
		{
				Destroy(gameObject, 1.0f);
		}

		// Update is called once per frame
		void Update()
		{
				damage = (int)(damage - 0.5f);

				if (damage <= 1) damage = 1;
				if (Physics.Linecast(transform.position, transform.position + transform.forward * Time.deltaTime * 30, out hit))
				{
						transform.position = hit.point - transform.forward * Time.deltaTime * 5;
				}
				else
				{
						transform.position += transform.forward * Time.deltaTime * 30;
				}
		}

		private void OnTriggerEnter(Collider collision)
		{
				if (collision.gameObject.tag == "Enemy")
				{
						collision.GetComponent<EnemySc>().armorPoint -= 3;
						collision.GetComponent<MoveCon>().speed *= 0.95f;
				}
				Instantiate(explosion, transform.position, transform.rotation);
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