using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeSc : MonoBehaviour
{
		public bool destroy = true;

		// Use this for initialization
		void Start()
		{
				if (destroy)
				{
						Destroy(gameObject, 10);
				}
		}

		// Update is called once per frame
		void Update()
		{

		}
}