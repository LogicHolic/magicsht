using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bridgeInfo : MonoBehaviour
{
		public List<Vector3> bridgeList;

		Vector3[] vec3arr = new Vector3[2];

		[SerializeField]
		public GameObject bridge;

		Quaternion rot;
		

		// Use this for initialization
		void Start()
		{
				bridgeList = new List<Vector3>() { };
		}

		// Update is called once per frame
		void Update()
		{
		}

		public void addPos(Vector3 pos)
		{
				Debug.Log("add");
				bridgeList.Add(pos);
				
				if (bridgeList.Count == 2)
				{

						for (int i = 0; i < bridgeList.Count; i++)
						{
								vec3arr[i] = bridgeList[i];
						}
						StartCoroutine("createBridge", vec3arr);

						bridgeList.Clear();
						bridgeList.TrimExcess();
				}
		}

		IEnumerator createBridge(Vector3[] vec)
		{
				Vector3 vc = (vec[1] - vec[0]).normalized;
				System.Random r = new System.Random(1000);
				System.Random g = new System.Random(900);
				System.Random b = new System.Random(800);

				for (int i = 1;i < (int)(vec[0] - vec[1]).magnitude * 1.5; i++)
				{

						GameObject br = Instantiate(bridge, vec[0] + vc * i * 0.7f, Quaternion.Euler(r.Next() % 360, g.Next() % 360, b.Next() % 360));

						//gameObject.transform.FindChild("col").transform.LookAt(vec[1]);
						//col.transform.LookAt(vec[1]);
						//foreach (Transform child in transform)
						//{
						//		if(child.name == "col")
						//		{
						//				child.LookAt(vec[1]);
						//				break;
						//		}
						//}

						yield return new WaitForSeconds(0.06f);
				}
		}
}