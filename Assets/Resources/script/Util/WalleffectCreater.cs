using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalleffectCreater : MonoBehaviour
{

		public int activeTime;
		public int unactiveTime;
		public GameObject target;

		int cycleCnt;
		// Use this for initialization
		void Start()
		{
				cycleCnt = 0;
				target.SetActive(true);
		}

		// Update is called once per frame
		void Update()
		{
				cycleCnt++;
				if(cycleCnt == activeTime)//アクティブな時間が終われば
				{
						target.SetActive(false);
				}
				if(cycleCnt == activeTime + unactiveTime){//アクティブと非アクティブが一周
						target.SetActive(true);
						cycleCnt = 0;
				}
		}
}