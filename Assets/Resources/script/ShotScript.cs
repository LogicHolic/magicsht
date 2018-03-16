using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
		[SerializeField, Range(0.0f, 1.0f)]
		private float lookTotalWeight = 0.0f;
		[SerializeField, Range(0.0f, 1.0f)]
		private float bodyWeight = 0.0f;
		[SerializeField, Range(0.0f, 1.0f)]
		private float headWeight = 0.0f;
		[SerializeField, Range(0.0f, 1.0f)]
		private float eyeWeight = 0.0f;

		int shotAttribute = 0;//雷→炎→水

		[SerializeField]
		Transform rightHandTransform;
		[SerializeField]
		Transform hookHandTransform;
		[SerializeField]
		Transform bridgeHandTransform;

		Animator ani;

		public GameObject Cam;
		public GameObject Bullet;
		public GameObject muzzle;
		public GameObject muzzleFlash;
		public GameObject Holer;
		public GameObject holerEff;
		public GameObject Hook;
		public GameObject Bridger;
		public GameObject Firer;
		public GameObject Fire;
		public GameObject Water;

		public GameObject IKtarget;

		public Vector3 throwTargetVec;

		bool shotIK = false;
		bool hookIK = false;
		bool bridgeIK = false;

		float shotInterval = 15;
		float shotIntervalMax = 15;

		float fireShotInterval = 120;
		float fireShotIntervalMax = 120;

		float bridgeShotInterval = 40;
		float bridgeShotIntervalMax = 40;

		float holeShotInterval = 500;
		float holeShotIntervalMax = 500;

		float hookShotInterval = 20;
		float hookShotIntervalMax = 20;

		int shotCost = 5;
		int hookCost = 10;
		int bridgeCost = 20;
		int fireCost = 50;
		int holeCost = 60;

		// Use this for initialization
		void Start()
		{
				ani = GetComponent<Animator>();
		}

		// Update is called once per frame
		void LateUpdate()
		{
				LayerMask layerMask = 1 << 12;

				shotInterval++;
				hookShotInterval++;
				fireShotInterval++;
				bridgeShotInterval++;
				holeShotInterval++;

				IKtarget.transform.position = transform.position + Vector3.up * 0.7f + Cam.transform.forward * 30f;

				if (Input.GetKeyDown(KeyCode.Alpha0))
				{
						shotAttribute = 0;
				}
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
						shotAttribute = 1;
				}
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
						shotAttribute = 2;
				}

				if (Input.GetMouseButton(0))//通常弾
				{
						if (shotInterval > shotIntervalMax && GetComponent<MoveCon>().MP > shotCost)
						{
								GetComponent<MoveCon>().MP -= shotCost;
								shotIK = true;
								shotInterval = 0;
								Vector3 toTargetVec;
								toTargetVec = Cam.transform.forward;


								if (shotAttribute == 1)
								{
										Debug.Log(shotAttribute);
										Instantiate(Fire, muzzle.transform.position, Quaternion.LookRotation(toTargetVec));
								}
								else if (shotAttribute == 0)
								{
										Debug.Log(shotAttribute);
										Instantiate(Bullet, muzzle.transform.position, Quaternion.LookRotation(toTargetVec));
								}
								else if (shotAttribute == 2)
								{
										Debug.Log(shotAttribute);
										Instantiate(Water, muzzle.transform.position, Quaternion.LookRotation(toTargetVec));
								}

						};
				}
				if (shotInterval == shotIntervalMax)
				{
						shotIK = false;
				}

				if (Input.GetMouseButtonDown(1))//フック
				{
						if (hookShotInterval > hookShotIntervalMax && GetComponent<MoveCon>().MP > hookCost)
						{
								GetComponent<MoveCon>().MP -= hookCost;
								hookIK = true;
								hookShotInterval = 0;
								Vector3 toTargetVec;
								toTargetVec = Cam.transform.forward;

								Instantiate(Hook, muzzle.transform.position, Quaternion.LookRotation(toTargetVec));
						}
				};
				if (Input.GetMouseButtonUp(1))
				{
						hookIK = false;
				}


				if (Input.GetKeyDown(KeyCode.G))//holer
				{
						if (holeShotInterval > holeShotIntervalMax && GetComponent<MoveCon>().MP > holeCost)
						{
								GetComponent<MoveCon>().MP -= holeCost;
								StartCoroutine("aniHole");
						}
				};


				if (Input.GetKeyDown(KeyCode.B))//bridger
				{
						if (bridgeShotInterval > bridgeShotIntervalMax && GetComponent<MoveCon>().MP > bridgeCost)
						{
								GetComponent<MoveCon>().MP -= bridgeCost;
								bridgeIK = true;
								bridgeShotInterval = 0;
								Vector3 toTargetVec;
								toTargetVec = Cam.transform.forward;

								Instantiate(Bridger, muzzle.transform.position, Quaternion.LookRotation(toTargetVec));
						}
				};
				if (bridgeShotInterval == bridgeShotIntervalMax)
				{
						bridgeIK = false;
				}

				if (Input.GetKeyDown(KeyCode.F) && GetComponent<MoveCon>().isGround)//fire
				{
						if (fireShotInterval > fireShotIntervalMax)
						{
								StartCoroutine("aniThrow");
						}
				};
		}

		IEnumerator aniHole()
		{
				ani.SetBool("hole", true);
				holeShotInterval = 0;

				yield return new WaitForSeconds(0.3f);

				GetComponent<MoveCon>().canMove = false;

				throwTargetVec = Cam.transform.forward;

				Instantiate(Holer, muzzle.transform.position, Quaternion.LookRotation(throwTargetVec));

				yield return new WaitForSeconds(0.8f);

				ani.SetBool("hole", false);
				GetComponent<MoveCon>().canMove = true;
		}

		IEnumerator aniThrow()
		{
				ani.SetBool("throw", true);
				fireShotInterval = 0;

				yield return new WaitForSeconds(0.15f);

				GetComponent<MoveCon>().canMove = false;

				throwTargetVec = Cam.transform.forward;

				Instantiate(Firer, muzzle.GetComponent<Transform>());

				yield return new WaitForSeconds(0.90f);

				ani.SetBool("throw", false);
				GetComponent<MoveCon>().canMove = true;
		}

		void OnAnimatorIK()
		{
				ani.SetLookAtWeight(lookTotalWeight, bodyWeight, headWeight, eyeWeight);
				ani.SetLookAtPosition(IKtarget.transform.position);

				if (shotIK)
				{
						ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKPosition(AvatarIKGoal.RightHand, rightHandTransform.position);
						ani.SetIKRotation(AvatarIKGoal.RightHand, rightHandTransform.rotation);
				}

				if (hookIK)
				{
						ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKPosition(AvatarIKGoal.RightHand, hookHandTransform.position);
						ani.SetIKRotation(AvatarIKGoal.RightHand, hookHandTransform.rotation);
				}

				if (bridgeIK)
				{
						ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
						ani.SetIKPosition(AvatarIKGoal.RightHand, bridgeHandTransform.position);
						ani.SetIKRotation(AvatarIKGoal.RightHand, bridgeHandTransform.rotation);
				}

				if(!bridgeIK && !shotIK && !hookIK)
				{
						ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
						ani.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
						ani.SetIKPosition(AvatarIKGoal.RightHand, rightHandTransform.position);
						ani.SetIKRotation(AvatarIKGoal.RightHand, rightHandTransform.rotation);
				}
		}
}