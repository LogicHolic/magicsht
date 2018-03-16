using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
		[SerializeField]
		public int HP = 100;
		[SerializeField]
		public int MP = 100;

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
				if (HP <= 0) Destroy(gameObject);
		}
}