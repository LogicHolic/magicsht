using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonMouseOver : MonoBehaviour, IPointerEnterHandler
{
		public void OnPointerEnter(PointerEventData eventData)
		{
				Debug.Log("aaa");
				GetComponent<Image>().color = new Color(255, 255, 255, 1);
		}
}