using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCurrentCondition : MonoBehaviour
{
	[SerializeField] private TMP_Text conditionText;
	[SerializeField] private TMP_Text conditionGems;
	[SerializeField] private TMP_Text valueButtonText;
	[SerializeField] private TMP_Text extraGemsResult;
	[SerializeField] private GameObject winConditionPanel;
	[SerializeField] private GameObject loseConditionPanel;
	[SerializeField] private Button reselectButton;
	[SerializeField] private Button selectMenuButton;

	private void Start()
	{
		reselectButton.onClick.AddListener(ReselectCurrentLevel);
		selectMenuButton.onClick.AddListener(SelectMenuScene);
	}

	public void ShowLastCondition(GameCondition condition)
	{
		if (condition.WinResult)
		{
			winConditionPanel.gameObject.SetActive(true);
			conditionText.text = $"LEVEL {condition.Level} COMPLETED!";
			valueButtonText.text = "NEXT";
			conditionGems.text = condition.WinGems.ToString();
			extraGemsResult.text = condition.ExtraGems.ToString();

			SaveCompiler.CurrentSystem.serializedGems += condition.WinGems + condition.ExtraGems;
			SaveCompiler.CurrentSystem.serializedProgress++;
			SaveCompiler.CurrentSystem.SerializeSystem();
		}
		else
		{
			loseConditionPanel.gameObject.SetActive(true);
			conditionText.text = $"LEVEL {condition.Level}: FAIL!";
			valueButtonText.text = "RETRY";
		}

		gameObject.SetActive(true);
	}

	public void ReselectCurrentLevel()
	{
		SceneManager.LoadScene("SagaGame");
	}

	public void SelectMenuScene()
	{
		SceneManager.LoadScene("SagaMenu");
	}
}

public class GameCondition
{
	private bool winResult;
	private int winGems;
	private int extraGems;
	private int level;
	public bool WinResult => winResult;
	public int WinGems => winGems;
	public int ExtraGems => extraGems;
	public int Level => level;

	public GameCondition(bool result, int gems, int extraGems, int level)
	{
		winGems = gems;
		winResult = result;
		this.extraGems = extraGems;
		this.level = level;
	}
}
