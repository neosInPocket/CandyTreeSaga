using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainStoreSetter : MonoBehaviour
{
	[SerializeField] private PlayerGoodsSetter playerGoodsSetter;
	[SerializeField] private UpgradeBox upgradeBoxFirst;
	[SerializeField] private UpgradeBox upgradeBoxSecond;
	[SerializeField] private Button leftArrowed;
	[SerializeField] private Button rightArrowed;
	[SerializeField] private GameObject[] knobs;
	[SerializeField] private ScrollRect scrollRect;
	[SerializeField] private AnimationCurve accelerationCurve;
	[SerializeField] private float scrollSpeed;

	private void Start()
	{
		CheckStoreSystem();
		SetCurrentBoxInstant(true);
	}

	public void CheckStoreSystem()
	{
		upgradeBoxFirst.SetMainBoxValues();
		upgradeBoxSecond.SetMainBoxValues();
		playerGoodsSetter.SetSystemValues();
	}

	public void SetCurrentBoxInstant(bool leftSided)
	{
		SetSetterControls(leftSided);

		if (leftSided)
		{
			scrollRect.normalizedPosition = Vector3.zero;
		}
		else
		{
			scrollRect.normalizedPosition = Vector2.right;
		}
	}

	public void SetSetterControls(bool leftSided)
	{
		knobs[0].SetActive(leftSided);
		knobs[1].SetActive(!leftSided);
		leftArrowed.interactable = !leftSided;
		rightArrowed.interactable = leftSided;
	}

	public void NavigateTo(bool leftSided)
	{
		StopAllCoroutines();
		StartCoroutine(SetCurrentBox(leftSided));
	}

	private IEnumerator SetCurrentBox(bool leftSided)
	{
		SetSetterControls(leftSided);

		int direction = leftSided ? -1 : 1;
		int target = leftSided ? 0 : 1;
		Vector2 currentPosition = scrollRect.normalizedPosition;
		float distance = Mathf.Abs(target - currentPosition.x);

		while ((direction > 0 && currentPosition.x < 1) || (direction < 0 && currentPosition.x > 0))
		{
			currentPosition.x += direction * accelerationCurve.Evaluate(distance) * scrollSpeed * Time.deltaTime;
			scrollRect.normalizedPosition = currentPosition;
			distance = Mathf.Abs(target - currentPosition.x);
			yield return null;
		}

		currentPosition.x = target;
		scrollRect.normalizedPosition = currentPosition;
	}
}
