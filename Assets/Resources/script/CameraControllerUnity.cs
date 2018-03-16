using UnityEngine;
using System.Collections;

public class CameraControllerUnity : MonoBehaviour
{
		[SerializeField]
		Transform target;
		public float spinSpeed = 0.5f;
		public float distance = 0.3f;

		Vector3 nowPos;
		Vector3 pos = Vector3.zero;
		public Vector2 mouse = Vector2.zero;

		private RaycastHit hit;

		public bool canCam;

		void Start()
		{
				transform.parent = null;
				// 初期位置の取得
				nowPos = new Vector3(0,1,-10);
				canCam = true;
		}

		void LateUpdate()
		{
				// マウスの移動の取得
				mouse += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime * spinSpeed;

				mouse.y = Mathf.Clamp(mouse.y, -0.3f + 0.5f, 0.3f + 0.5f);

				// 球面座標系変換
				pos.x = distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
				pos.y = -distance * Mathf.Cos(mouse.y * Mathf.PI);
				pos.z = -distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);

				pos *= nowPos.z;
				pos.y += nowPos.y;

				if (canCam)
				{
						transform.position = pos + target.position;
						transform.LookAt(target.position + Vector3.up * 2f);
				}

				Vector3 cameraForward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;


				if (Physics.Linecast(target.transform.position + Vector3.up * 1.0f, transform.position, out hit))
				{
						if (!(hit.collider.tag == "Hole") && !(hit.collider.tag == "Player") && !(hit.collider.tag == "Bullet"))
						{
								transform.position = hit.point + Vector3.up * 0.7f;
						}
				}
		}
}