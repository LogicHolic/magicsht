using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySc : MonoBehaviour
{
		public int armorPoint;
		public int armorPointMax = 100;
		int damage;

		Animator ani;

		// Use this for initialization
		void Start()
		{
				ani = GetComponent<Animator>();
				armorPoint = armorPointMax;
		}

		// Update is called once per frame
		void Update()
		{
				if (armorPoint <= 0)
				{
						StartCoroutine("Dead");
				}
		}

		IEnumerator Dead()
		{
				ani.SetBool("isDead", true);
				yield return new WaitForSeconds(5);
				ani.SetBool("isDead", false);
		}

		//private void OnCollisionEnter(Collision collision)
		//{
		//  if (collision.gameObject.tag == "Bullet")
		//  {
		//    damage = collision.gameObject.GetComponent<bulletSc>().damage;

		//    armorPoint -= damage;
		//    Debug.Log(armorPoint);

		//    if (armorPoint <= 0)
		//    {
		//      Destroy(gameObject);
		//    }
		//  }
		//}
}
