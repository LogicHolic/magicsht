using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class buttonAlphachange : MonoBehaviour{

		float alpha;
		public GameObject _Start;
		// Alpha増減値(点滅スピード調整)
		public float _Step = 0.05f;
		public float AlphaMin = 0;
		public float AlphaMax = 1;

		void Start()
		{
		}

		void Update()
		{
				// 現在のAlpha値を取得
				float toColor = GetComponent<Image>().color.a;
				// Alphaが0 または 1になったら増減値を反転
				if (toColor < AlphaMin || toColor > AlphaMax)
				{
						_Step = _Step * -1;
				}
				// Alpha値を増減させてセット
				GetComponent<Image>().color = new Color(255, 255, 255, toColor + _Step);
		}
}
