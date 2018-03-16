using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookSc : MonoBehaviour
{
		[SerializeField]
		GameObject Lmanager;


		Vector3 endPos;

		private int touchObj = 0;
		private GameObject Player;
		private Rigidbody rb;

		int count = 0;
		MoveCon jump;

		private RaycastHit hit;

		float hookLen = 0;

		void Start()
		{
				Player = GameObject.FindGameObjectWithTag("Player");
				rb = Player.GetComponent<Rigidbody>();
				
				jump = Player.GetComponent<MoveCon>();

				Lmanager = GameObject.Find("LightningManager");
		}
		
		void Update()
		{
				Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().StartPosition = transform.position;
				Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().EndPosition = Player.transform.position;

				if (Input.GetMouseButtonDown(1)) count = 0;
				if (Input.GetMouseButton(1))
				{
						if (touchObj == 0)
						{
								count++;
								//transform.position += transform.forward * Time.deltaTime * 50;
								if (Physics.Linecast(transform.position, transform.position + transform.forward * Time.deltaTime * 50, out hit))
								{
										if (hit.collider.tag == "wall" || hit.collider.tag == "floor")
										{
												if (hit.collider.GetComponent<wallInfo>().canHook)
												{
														transform.position = hit.point - transform.forward * Time.deltaTime * 5;
												}
										}
								}
								else
								{
										transform.position += transform.forward * Time.deltaTime * 50;
								}
								
						}
						else
						{
								transform.localScale = new Vector3(0, 0, 0);

								Vector3 Vec = transform.position - (Player.transform.position + Vector3.up * 1.5f);

								//LightningStart.GetComponent<DigitalRuby.LightningBolt.lbSc>().StartPosition = Player.transform.position;
								//LightningStart.GetComponent<DigitalRuby.LightningBolt.lbSc>().EndPosition = transform.position;

								float dist = Vec.magnitude;
								Vec = Vec.normalized;
								
								jump.jpCnt = 0;
								if (dist < 0.1f)
								{
										rb.velocity = new Vector3(0,1f,0);
								} else{
										if(dist < 1) dist++;
										rb.AddForce(Vec * dist * Mathf.Log(dist) * 8f, ForceMode.Acceleration);
								}
								
						}
				}
				else
				{
						Destroy(gameObject);
						Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().StartPosition = Vector3.zero;
						Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().EndPosition = Vector3.zero;
				}

				if (count > 15)
				{
						Destroy(gameObject);
						Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().StartPosition = Vector3.zero;
						Lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().EndPosition = Vector3.zero;
				}

		}

		private void OnCollisionEnter(Collision collision)
		{
				if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "floor")
				{
						if (collision.gameObject.GetComponent<wallInfo>().canHook)
						{
								gameObject.transform.parent = collision.gameObject.transform;
								//transform.SetParent(collision.gameObject.transform, false);
								touchObj = 1;
						}
				}
				else { Destroy(gameObject); }
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