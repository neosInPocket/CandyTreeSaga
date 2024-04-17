using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutlookWindow : MonoBehaviour
{
	[SerializeField] private Image currentOutlookImage;
	[SerializeField] private TMP_Text currentOutLookText;
	[SerializeField] private TMP_Text extraGemsValue;
	public float Progress
	{
		get => (float)current.currentState / (float)current.maximumState;
		set => currentOutlookImage.fillAmount = value;
	}

	public int CurrentNumberProgress
	{
		get => current.currentState;
		set => currentOutLookText.text = $"{current.currentState}/{current.maximumState}";
	}

	public int ExtraGemsValueProgress
	{
		get => current.extraGems;
		set => extraGemsValue.text = value.ToString();
	}

	private CurrentOutlook current;

	public void Construct(CurrentOutlook currentOutlook)
	{
		current = currentOutlook;
		SetOutLookValues();
	}

	public void SetOutLookValues()
	{
		Progress = (float)current.currentState / (float)current.maximumState;
		CurrentNumberProgress = 1;
		ExtraGemsValueProgress = current.extraGems;
	}
}


public class CurrentOutlook
{
	public int extraGems;
	public int currentState;
	public readonly int maximumState;
	public readonly int levelGrab;

	public CurrentOutlook(int currentLevel)
	{
		levelGrab = GetLevelGrab(currentLevel);
		maximumState = GetMaximumState(currentLevel);
	}

	public int GetLevelGrab(int currentLevel)
	{
		int result = (int)(5f * Mathf.Sqrt(currentLevel + 1));
		return result;
	}

	public int GetMaximumState(int currentLevel)
	{
		int result = (int)(3f * Mathf.Sqrt(currentLevel + 1));
		return result;
	}

	public bool CheckCurrentGameState()
	{
		if (currentState >= maximumState)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
