using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CoachBehaviour : MonoBehaviour
{
	[SerializeField] public TMP_Text coachValue;
	[SerializeField] public Animator auxiliaryIndex;
	public Action CoachGuidePassed;

	public bool CoachStart(Action coachGuided)
	{
		if (SaveCompiler.CurrentSystem.tuition == 1)
		{
			SaveCompiler.CurrentSystem.tuition = 0;
			SaveCompiler.CurrentSystem.SerializeSystem();
		}
		else
		{
			return false;
		}

		CoachGuidePassed = coachGuided;
		Touch.onFingerDown += Attention;
		coachValue.text = "WELCOME TO BONANZA STRANGE PATH!";
		gameObject.SetActive(true);
		return true;
	}

	private void Attention(Finger finger)
	{
		Touch.onFingerDown -= Attention;
		Touch.onFingerDown += AntiClockWise;
		coachValue.text = "I present to your attention your new friend - an amazing candy that you can control!";
		auxiliaryIndex.SetTrigger("increaseIndex");
	}

	public void AntiClockWise(Finger finger)
	{
		Touch.onFingerDown -= AntiClockWise;
		Touch.onFingerDown += WhiteCharges;
		coachValue.text = "Tap the screen as she moves to turn her in anticlockwise: your candy hero never stops moving, guide her to avoid trouble!";
		auxiliaryIndex.SetTrigger("increaseIndex");
	}

	public void WhiteCharges(Finger finger)
	{
		Touch.onFingerDown -= WhiteCharges;
		Touch.onFingerDown += GemChance;
		coachValue.text = "your goal is to aim your candy at the white charges that come your way. charges must be connected in series, otherwise you will lose";
		auxiliaryIndex.SetTrigger("increaseIndex");
	}

	public void GemChance(Finger finger)
	{
		Touch.onFingerDown -= GemChance;
		Touch.onFingerDown += RequiredLuck;
		coachValue.text = "a gem may appear near each charge with some chance - collect it to increase the level reward!";
		auxiliaryIndex.SetTrigger("increaseIndex");
	}

	public void RequiredLuck(Finger finger)
	{
		Touch.onFingerDown -= RequiredLuck;
		Touch.onFingerDown += CoachPassed;
		coachValue.text = "get to the required number of charges and complete the level! Good luck!!";
		auxiliaryIndex.SetTrigger("increaseIndex");
	}

	public void CoachPassed(Finger finger)
	{
		Touch.onFingerDown -= CoachPassed;
		CoachGuidePassed?.Invoke();
		gameObject.SetActive(false);
	}

	public void OnDestroyScenario()
	{
		Touch.onFingerDown -= Attention;
		Touch.onFingerDown -= AntiClockWise;
		Touch.onFingerDown -= WhiteCharges;
		Touch.onFingerDown -= GemChance;
		Touch.onFingerDown -= RequiredLuck;
		Touch.onFingerDown -= CoachPassed;
	}

	private void OnDestroy()
	{
		OnDestroyScenario();
	}
}
