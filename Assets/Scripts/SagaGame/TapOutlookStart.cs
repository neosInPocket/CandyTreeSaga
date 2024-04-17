using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TapOutlookStart : MonoBehaviour
{
	[SerializeField] private TMP_Text tapText;

	public Action OnWaitEnd { get; private set; }

	public void StartWait(Action onWaitEnd)
	{
		tapText.text = $"START LEVEL {SaveCompiler.CurrentSystem.serializedProgress}";

		OnWaitEnd = onWaitEnd;
		gameObject.SetActive(true);
		Touch.onFingerDown += StopWait;
	}

	public void StopWait(Finger finger)
	{
		Touch.onFingerDown -= StopWait;
		gameObject.SetActive(false);
		OnWaitEnd();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= StopWait;
	}
}
