using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInfo : MonoBehaviour
{
		public List<Vector3> fireList;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void addPos(Vector3 pos)
		{
				fireList.Add(pos);
		}

		public void popPos(Vector3 pos)
		{
				fireList.Remove(pos);
		}
}