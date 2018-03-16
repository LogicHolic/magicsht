using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class countDown : MonoBehaviour
{
		[SerializeField]
		private Text _textCountdown;

		void Start()
		{
				_textCountdown.text = "";
				StartCoroutine(CountdownCoroutine());
		}
		

		IEnumerator CountdownCoroutine()
		{
				_textCountdown.gameObject.SetActive(true);

				_textCountdown.text = "3";
				yield return new WaitForSeconds(1.0f);

				_textCountdown.text = "2";
				yield return new WaitForSeconds(1.0f);

				_textCountdown.text = "1";
				yield return new WaitForSeconds(1.0f);

				_textCountdown.text = "GO!";

				yield return new WaitForSeconds(1.0f);

				_textCountdown.text = "";
				GameObject.FindGameObjectWithTag("Player").GetComponent<MoveCon>().canMove = true;
				_textCountdown.gameObject.SetActive(false);
		}
}