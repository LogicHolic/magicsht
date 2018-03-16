using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolerSc : MonoBehaviour
{
		public GameObject hole;

		Vector3 holePos;
		Vector3 wallVec;
		Quaternion rot;
		Vector3 angle;

		Rigidbody rb;

		void Start()
		{
				Destroy(gameObject, 4.0f);
		}

		void Update()
		{
				transform.position += transform.forward * Time.deltaTime * 5f;
		}

		private void OnCollisionEnter(Collision collision)
		{
				if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "floor")
				{
						if (collision.gameObject.GetComponent<wallInfo>().canHole)
						{
								foreach (ContactPoint point in collision.contacts)
								{
										//GameObject effect = Instantiate();
										//effect.transform.position = (Vector3)point.point;
										holePos = (Vector3)point.point;
										//collision.gameObject.GetComponent<wallInfo>().holePos;
								}

								wallVec = collision.gameObject.GetComponent<wallInfo>().wallVec;
								if (Vector3.Dot(wallVec, transform.position - collision.transform.position) > 0)
										wallVec = -wallVec;

								hole.GetComponent<holeSc>().parent = collision.gameObject;
								hole.GetComponent<holeSc>().holePos = holePos;

								angle = collision.gameObject.GetComponent<wallInfo>().angle;
								rot.eulerAngles = new Vector3(angle.x + 90, angle.y, 0);
							
								for (int i = 0; i < collision.gameObject.GetComponent<wallInfo>().needHole + 2; i++)
								{
										Instantiate(hole, holePos - wallVec / 5f + i * wallVec / 10f, rot);
								}

						}
				}
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