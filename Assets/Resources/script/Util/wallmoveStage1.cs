using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallmoveStage1 : MonoBehaviour
{

		int Cnt;

		public float speed;
		public Vector3 vec;
		public int loopTime;

		// Use this for initialization
		void Start()
		{
				vec.Normalize();
		}

		// Update is called once per frame
		void Update()
		{
				Cnt++;
				if (Cnt > loopTime)
				{
						vec = -vec;
						Cnt = 0;
				}

				transform.position += vec * speed;
		}
}