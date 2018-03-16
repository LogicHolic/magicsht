using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveCon : MonoBehaviour
{
		[SerializeField]
		public GameObject fmanager;
		[SerializeField]
		public GameObject gmanager;
		[SerializeField]
		public Camera Cam;
		[SerializeField]
		public int MP = 100000;

		[SerializeField]
		Fade fade;

		int MPhealing = 10;
		int MPCnt = 0;

		int warpCost = 40;
		int ravenCost = 10;

		public GameObject ravenEff;

		Vector3 checkPos;


		int ravenCnt = 70;
		int ravenCntMax = 70;

		int warpCnt = 180;
		int warpCntMax = 180;

		List<Vector3> fList;
		List<Vector3> bList;
		Rigidbody rb;
		Animator ani;
		public float speed = 0.1f;
		float defaultSpeed = 0.1f;
		private Vector3 input = Vector3.zero;
		public bool isGround;
		float jump;
		int mask;
		RaycastHit hit;
		public int jpCnt;
		bool isWall;
		Vector3 wallVec;
		bool isSkate;
		bool wSkate;
		bool isDead;
		int wSkateCnt = 20;

		public bool canMove;

		void Start()
		{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;

				rb = GetComponent<Rigidbody>();
				mask = LayerMask.GetMask(new string[] { "Floor", "Enemy", "Wall" });
				ani = GetComponent<Animator>();
				isGround = true;
				isWall = false;
				wSkate = true;

				canMove = true;
		}

		void Update()
		{
				ravenCnt++;
				warpCnt++;

				if(defaultSpeed > speed)
				{
						speed *= 1.005f;
				}

				if(MPCnt > MPhealing && MP < 100)
				{
						MP++;
						MPCnt = 0;
				}
				MPCnt++;

				if (Input.GetKeyDown(KeyCode.C))
				{
						if (Cursor.lockState == CursorLockMode.Locked)
						{
								Cursor.lockState = CursorLockMode.None;
						}
						else if (Cursor.lockState == CursorLockMode.None)
						{
								Cursor.lockState = CursorLockMode.Locked;
						}
						Cursor.visible = !Cursor.visible;

						GetComponent<ShotScript>().enabled = !GetComponent<ShotScript>().enabled;
						canMove = !canMove;
						Cam.GetComponent<CameraControllerUnity>().canCam = !Cam.GetComponent<CameraControllerUnity>().canCam;
				}



				Vector3 cameraForward = Vector3.Scale(Cam.transform.forward, new Vector3(1, 0, 1)).normalized;

				if (canMove)
				{
						input = cameraForward * Input.GetAxis("Vertical") + Cam.transform.right * Input.GetAxis("Horizontal");
						input *= speed;
				}
				else
				{
						input = Vector3.zero;
				}

				if (input.x != 0 || input.z != 0)
				{
						transform.rotation = Quaternion.LookRotation(input);
						ani.SetFloat("Forward", 1.0f);
				}
				else
				{
						transform.rotation = Quaternion.LookRotation(Vector3.Scale(Cam.transform.forward, new Vector3(1, 0, 1)).normalized);
						ani.SetFloat("Forward", 0.0f);
				}


				jump = rb.velocity.y;
				if (jump > -9) jump = -9;
				if (jump < 5) jump = 5;
				ani.SetFloat("Jump", jump);

				if (isGround && canMove)
				{
						rb.velocity = new Vector3(0, rb.velocity.y, 0);
						isWall = false;
				}

				jpCnt++;
				wSkateCnt++;

				bool forRay1 = Physics.SphereCast(transform.position + new Vector3(0, 0.3f, 0), 0.1f, input, out hit, 0.1f, mask);
				bool forRay2 = Physics.SphereCast(transform.position + new Vector3(0, 1.3f, 0), 0.1f, input, out hit, 0.1f, mask);

				if (isGround && !forRay1 && !forRay2 && !isSkate && canMove)
				{
						rb.position += input;
				}

				if (!isWall && !isGround && !forRay1 && !forRay2 && canMove)
				{
						rb.position += input * 0.9f;
				}

				if (forRay1 && forRay2 && canMove && gameObject.layer == 14)
				{
						rb.position += input;
				}

				if (isWall && !isSkate)
				{
						if (wSkate && wSkateCnt > 10)
						{
								rb.AddForce(new Vector3(0, 5, 0) + wallVec * 3, ForceMode.Impulse);
								wSkate = false;
								isWall = false;
						}

						transform.LookAt(transform.position + wallVec);

						if (wSkateCnt > 10)
								rb.velocity = new Vector3(0, -2, 0);

						if (Input.GetKeyDown(KeyCode.Space) && canMove)
						{
								rb.AddForce(new Vector3(0, 20, 0) + wallVec * 8, ForceMode.Impulse);
								jpCnt = 0;
								isWall = false;
						}

						if (jpCnt < 20)
						{
								isGround = false;
						}
				}

				if (isSkate && canMove)
				{
						if (isGround)
						{
								rb.position += input * 2.5f;
						}

						if (isWall)
						{
								rb.velocity = new Vector3(0, 0, 0);
								rb.position += Quaternion.AngleAxis(90f, Vector3.Cross(Vector3.up, wallVec)) * input * 1.5f;
								if (!wSkate)
								{
										wSkate = true;
										wSkateCnt = 0;
								}
						}
				}

				if (Input.GetKeyDown(KeyCode.R))//bridge warp
				{
						if (warpCnt > warpCntMax)
						{
								bList = gmanager.GetComponent<bridgeInfo>().bridgeList;
								if (bList.Count != 0 && MP > warpCost)
								{
										MP -= warpCost;
										StartCoroutine("warp", bList[0]);
										bList.Clear();
										warpCnt = 0;
								}
						}
				}

				fList = fmanager.GetComponent<FireInfo>().fireList;
				for (int i = 0; i < fList.Count; i++)
				{
						if ((fList[i] - transform.position).magnitude < 2)
						{
								if (isGround || isWall)
								{
										isSkate = true;
										break;
								}
								else
								{
										isSkate = false;
								}
						}
						else
						{
								isSkate = false;
						}
				}
				if (fList.Count == 0) isSkate = false;

				if (Physics.SphereCast(transform.position + new Vector3(0, 0.3f, 0), 0.15f, -Vector3.up, out hit, 0.3f, mask))
				{
						isGround = true;

						if (Input.GetKeyDown(KeyCode.Space) && canMove)
						{
								rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
								jpCnt = 0;
						}

						if (jpCnt < 20)
						{
								isGround = false;
						}
				}
				else
				{
						jpCnt = 0;
				}

				ani.SetBool("isWall", isWall);
				ani.SetBool("skate", isSkate);
				ani.SetBool("OnGround", isGround);

				if (Input.GetKeyDown(KeyCode.V) && ravenCnt > ravenCntMax && MP > ravenCost)
				{
						MP -= ravenCost;
						StartCoroutine("raven",input);
						ravenCnt = 0;
				}
		}

		private void OnCollisionEnter(Collision collision)
		{
				if (!isGround && collision.gameObject.tag == "wall")
				{
						isWall = true;
						wallVec = collision.gameObject.GetComponent<wallInfo>().wallVec;
						if (Vector3.Dot(wallVec, transform.position - collision.transform.position) < 0)
						{
								wallVec = new Vector3(-wallVec.x, 0, -wallVec.z);
						}
				}

				if (collision.gameObject.tag == "Goal")
				{
						canMove = false;
						ani.SetBool("isDead", true);
						fade.FadeIn(1f, () =>
						{
								SceneManager.LoadScene("Menu");
								fade.FadeOut(1);
						});
				}
		}

		private void OnTriggerEnter(Collider other)
		{
				if (other.gameObject.tag == "checkPoint")
				{
						checkPos = other.ClosestPointOnBounds(this.transform.position);
				}
		}

		private void OnTriggerStay(Collider other)
		{
				if (other.gameObject.tag == "Hole")
				{
						if(other.GetComponent<holeSc>().parent.tag == "wall")
								gameObject.layer = 14;
						if (other.GetComponent<holeSc>().parent.tag == "floor")
								gameObject.layer = 15;
				}
		}
		private void OnTriggerExit(Collider other)
		{
				if (other.gameObject.tag == "Hole")
				{
						gameObject.layer = 8;
				}
		}




		IEnumerator raven(Vector3 input)
		{
				canMove = false;

				ani.SetInteger("raven", 1);
				//Instantiate(ravenEff, transform.position,transform.rotation);
				for (int i = 0; i < 20; i++)
				{
						rb.position += input;
						yield return null;
				}

				Instantiate(ravenEff, GetComponent<Transform>());

				for (int i = 0; i < 10; i++)
				{
						rb.position += input;
						yield return null;
				}
				Cam.GetComponent<CameraControllerUnity>().distance = Cam.GetComponent<CameraControllerUnity>().distance + 0.6f;

				if (Physics.SphereCast(transform.position + new Vector3(0, 0.3f, 0), 0.15f, input, out hit, 10f, mask))
				{
						transform.position = hit.point;
				}
				else
				{
						transform.position += input * 100f;
				}

				ani.SetInteger("raven", 2);

				for (int i = 0; i < 30; i++)
				{
						Cam.GetComponent<CameraControllerUnity>().distance -= 0.02f;
						rb.position += input;
						yield return null;
				}

				ani.SetInteger("raven", 0);
				canMove = true;
		}




		IEnumerator warp(Vector3 pos)
		{
				canMove = false;

				ani.SetInteger("raven", 1);
				//Instantiate(ravenEff, transform.position,transform.rotation);
				for (int i = 0; i < 20; i++)
				{
						rb.position += Cam.transform.forward * 0.1f;
						yield return null;
				}

				Instantiate(ravenEff, GetComponent<Transform>());

				for (int i = 0; i < 10; i++)
				{
						rb.position += Cam.transform.forward * 0.1f;
						yield return null;
				}
				Cam.GetComponent<CameraControllerUnity>().distance = Cam.GetComponent<CameraControllerUnity>().distance + 0.6f;

				transform.position = pos - ((pos-transform.position).normalized * 0.3f);

				ani.SetInteger("raven", 2);

				yield return null;

				for (int i = 0; i < 30; i++)
				{
						Cam.GetComponent<CameraControllerUnity>().distance -= 0.02f;
						//rb.position += Cam.transform.forward * 0.1f;
						yield return null;
				}

				ani.SetInteger("raven", 0);
				canMove = true;
		}

		public void Hit() { }
}