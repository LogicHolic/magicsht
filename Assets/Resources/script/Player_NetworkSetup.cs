using UnityEngine;
using System.Collections;
using UnityEngine.Networking; //ネットワーク関連で必要なライブラリー

public class Player_NetworkSetup : NetworkBehaviour
{
		//SerializeField] GameObject cam;
		[SerializeField] Camera FPSCharacterCam;
		[SerializeField] AudioListener audioListener;
		[SerializeField] GameObject fmanager;
		[SerializeField] GameObject gmanager;
		[SerializeField] GameObject lmanager;

		[SerializeField]
		Fade fade;
		[SerializeField]
		GameObject canvas;

		int moveCnt = 0;

		System.Random r = new System.Random();


		void Start()
		{
				//自分が操作するオブジェクトに限定する
				if (isLocalPlayer)
				{
						transform.position = new Vector3((r.Next() % 70) - 35, 10, (r.Next() % 70) - 35);

						fade = GameObject.Find("FadeCanvas").GetComponent<Fade>();
						canvas = GameObject.Find("Image");

						GameObject.Find("Canvas").GetComponent<countdownStart>().enabled = true;

		

						Cursor.lockState = CursorLockMode.Locked;

						//fade.FadeIn(1f, () =>
						//{
						//		canvas.SetActive(false);
						//		fade.FadeOut(1);
						//});

						//FPSCharacterCamを使うため、Scene Cameraを非アクティブ化
						GameObject.Find("SceneCamera").SetActive(false);

						FPSCharacterCam.GetComponent<CameraControllerUnity>().enabled = true;

						GetComponent<MoveCon>().enabled = true; //Character Controllerをアクティブ化
						GetComponent<MoveCon>().canMove = false;

						GetComponent<ShotScript>().enabled = true;           //FirstPersonControllerをアクティブ化
						gameObject.tag = "Player";
						gameObject.layer = 8;

						fmanager.transform.parent = null;
						gmanager.transform.parent = null;
						lmanager.transform.parent = null;

						fmanager.SetActive(true);
						gmanager.SetActive(true);
						lmanager.SetActive(true);

						fmanager.GetComponent<FireInfo>().enabled = true;
						gmanager.GetComponent<bridgeInfo>().enabled = true;
						lmanager.GetComponent<DigitalRuby.LightningBolt.lbSc>().enabled = true;
						lmanager.GetComponent<LineRenderer>().enabled = true;

						FPSCharacterCam.enabled = true;
						audioListener.enabled = true;
				}
				else if (!isLocalPlayer)
				{
						GetComponent<EnemySc>().enabled = true;
				}
		}
}