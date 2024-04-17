using UnityEngine;

public class GamePerformer : MonoBehaviour
{
	[SerializeField] private Arrower arrower;
	[SerializeField] private GameCurrentCondition gameCurrentCondition;
	[SerializeField] private OutlookWindow outlookWindow;
	[SerializeField] private CoachBehaviour coachBehaviour;
	[SerializeField] private TapOutlookStart tapOutlookStart;
	public CurrentOutlook currentOutlook;

	private void Start()
	{
		currentOutlook = new CurrentOutlook(SaveCompiler.CurrentSystem.serializedProgress);
		outlookWindow.Construct(currentOutlook);

		if (!coachBehaviour.CoachStart(OnCoachPassed))
		{
			OnCoachPassed();
		}
	}

	public void OnCoachPassed()
	{
		tapOutlookStart.StartWait(StartWaitEnd);
	}

	public void StartWaitEnd()
	{
		arrower.State = ArrowerState.Enabled;
		arrower.ArrowerBlow += ArrowerBlow;
		arrower.CoinGrab += CoinGrab;
	}

	public void ArrowerBlow(Arrower arrower)
	{
		GameOutlookEnd(false);
	}

	public void CoinGrab()
	{
		currentOutlook.extraGems++;
		outlookWindow.SetOutLookValues();
	}

	public void ArrowerDomainTouched()
	{
		currentOutlook.currentState++;
		if (currentOutlook.CheckCurrentGameState())
		{
			GameOutlookEnd(true);
		}

		outlookWindow.SetOutLookValues();
	}

	public void GameOutlookEnd(bool levelResult)
	{
		arrower.State = ArrowerState.Disabled;
		arrower.ArrowerBlow -= ArrowerBlow;
		arrower.CoinGrab -= CoinGrab;

		var condition = new GameCondition(levelResult, currentOutlook.levelGrab, currentOutlook.extraGems, SaveCompiler.CurrentSystem.serializedProgress);
		gameCurrentCondition.ShowLastCondition(condition);
	}

	private void OnDestroy()
	{
		arrower.State = ArrowerState.Disabled;
		arrower.ArrowerBlow -= ArrowerBlow;
		arrower.CoinGrab -= CoinGrab;
	}
}
