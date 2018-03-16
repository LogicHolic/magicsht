using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallInfo : MonoBehaviour
{
		public Vector3 wallVec;

		[SerializeField]
		public bool canHole;
		[SerializeField]
		public bool canHook;
		[SerializeField]
		public bool canBridge;


		public Vector3 angle;
		[SerializeField]
		public int needHole;

		public Vector3 holePos;

		public List<Vector3> holeList;

		// Use this for initialization
		void Start()
		{
				angle = transform.rotation.eulerAngles;
				Debug.Log(angle.y);
				wallVec = new Vector3(Mathf.Sin(angle.y * (Mathf.PI / 180)), 0, Mathf.Cos(angle.y * (Mathf.PI / 180)));
				//wallVec.Normalize();
		}

		// Update is called once per frame
		void Update()
		{	
		}

		public void holeAdd(Vector3 pos)
		{
				holeList.Add(pos);
		}

		public void holePop(Vector3 pos)
		{
				holeList.Remove(pos);
		}
}