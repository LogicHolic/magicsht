using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

		public GameObject explosion;

		// Use this for initialization
		void Start()
		{
				Destroy(gameObject, 2.0f);
		}

		// Update is called once per frame
		void Update()
		{
				transform.position += transform.forward * Time.deltaTime * 10;
		}


		private void OnCollisionEnter(Collision collision)
		{
				if (collision.gameObject.tag == "Player")
				{
						Destroy(gameObject);
						Instantiate(explosion, transform.position, transform.rotation);
				}
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
						gameObject.layer = 11;
				}
		}
}