using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgerSc : MonoBehaviour
{

		[SerializeField]
		public GameObject bridge;
		public GameObject manager;

		private int touchObj = 0;
		private Rigidbody rb;

		Quaternion rot;

		Vector3 wallVec;
		Vector3 bridgePos;

		private RaycastHit hit;

		// Use this for initialization
		void Start()
		{
				manager = GameObject.Find("globalManager");
				Destroy(gameObject, 1f);
		}

		// Update is called once per frame
		void Update()
		{
				if (touchObj == 0)
				{
						if (Physics.Linecast(transform.position, transform.position + transform.forward * Time.deltaTime * 30, out hit))
						{
								if (hit.collider.tag == "wall" || hit.collider.tag == "floor")
								{
										if (hit.collider.GetComponent<wallInfo>().canBridge)
										{
												transform.position = hit.point;

												bridgePos = (Vector3)hit.point;
												manager.GetComponent<bridgeInfo>().SendMessage("addPos", bridgePos);
												rot.eulerAngles = new Vector3(Random.value % 360, Random.value % 360, Random.value % 360);
												Instantiate(bridge, transform.position, rot);

												Destroy(gameObject);
										}
								}
						}
						else
						{
								transform.position += transform.forward * Time.deltaTime * 30;
						}
				}
				//else
				//{
				//		Destroy(gameObject);
				//}
		}

		//private void OnCollisionStay(Collision collision)
		//{
		//		if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "floor")
		//		{
		//				if (collision.gameObject.GetComponent<wallInfo>().canBridge)
		//				{
		//						foreach (ContactPoint point in collision.contacts)
		//						{
		//								bridgePos = (Vector3)point.point;
		//								manager.GetComponent<bridgeInfo>().SendMessage("addPos", bridgePos);
		//						}

		//						rot.eulerAngles = new Vector3(Random.value % 360, Random.value % 360, Random.value % 360);

		//						Instantiate(bridge, transform.position, rot);

		//						touchObj = 1;
		//				}
		//		}

		//		Destroy(gameObject);
		//}
}