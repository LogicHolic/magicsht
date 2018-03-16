using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeManage : MonoBehaviour
{
		public GameObject hole;

		public class holeInfo
		{
				public holeInfo(Vector3 pos, Vector3 angle, float thc)
				{
						holePos = pos;
						holeAngle = angle;
						thick = thc;
				}
				public Vector3 holePos;
				public Vector3 holeAngle;
				public float thick;
		}

		List<holeInfo> holeList;

		void Start()
		{
				holeList = new List<holeInfo>();
		}

		void Update()
		{

		}

		public void holeAdd(holeInfo info)
		{
				
				holeList.Add(new holeInfo(info.holePos,info.holeAngle,info.thick));
				Debug.Log(info);
		}

		public void holePop(holeInfo info)
		{
				Debug.Log(info);
				holeList.Remove(info);
		}
}