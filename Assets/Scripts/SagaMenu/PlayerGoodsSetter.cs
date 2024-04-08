using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGoodsSetter : MonoBehaviour
{
	[SerializeField] private List<TMP_Text> gemsSetter;
	[SerializeField] private List<TMP_Text> levelSetter;

	private void Start()
	{
		levelSetter.ForEach(x => x.text = $"PLAY LEVEL {SaveCompiler.CurrentSystem.serializedProgress}");
		gemsSetter.ForEach(x => x.text = SaveCompiler.CurrentSystem.serializedGems.ToString());
	}

	public void SetSystemValues()
	{
		levelSetter.ForEach(x => x.text = $"PLAY LEVEL {SaveCompiler.CurrentSystem.serializedProgress}");
		gemsSetter.ForEach(x => x.text = SaveCompiler.CurrentSystem.serializedGems.ToString());
	}

	public void LevelSetter()
	{
		SceneManager.LoadScene("SagaGame");
	}
}
