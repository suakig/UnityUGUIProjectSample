using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GUITouchEnable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, ICancelHandler {

	public void OnPointerEnter(PointerEventData ped) {
		InputTouch.Enabled (false);
	}

	public void OnPointerExit(PointerEventData ped) {
		InputTouch.Enabled (true);
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		InputTouch.Enabled (false);
	}

	public void OnCancel (BaseEventData eventData)
	{
		InputTouch.ClearDown ();
	}
}