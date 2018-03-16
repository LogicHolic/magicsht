using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeSc : MonoBehaviour
{
		public bool destroy = true;
		public GameObject parent;
		public Vector3 holePos;

		// Use this for initialization
		void Start()
		{
				if (destroy)
				{
						Destroy(gameObject, 8.34f);
				}
				StartCoroutine("hole");
		}

		// Update is called once per frame
		void Update()
		{

		}

		private IEnumerator hole()
		{
				parent.GetComponent<wallInfo>().SendMessage("holeAdd",holePos);
				for (float i = 1;i < 10; i++)
				{
						transform.localScale = new Vector3(0.2f * i,0.5f,0.3f * i);
						yield return null;
				}
				yield return new WaitForSeconds(8);
				for (float i = 10; i > 0; i--)
				{
						transform.localScale = new Vector3(0.2f * i, 0.5f, 0.3f * i);
						yield return null;
						parent.GetComponent<wallInfo>().SendMessage("holePop",holePos);
				}
		}
}