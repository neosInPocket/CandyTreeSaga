using System;
using UnityEngine;

[Serializable]
public class TreeSerializeSystem
{
	public int serializedProgress;
	public int serializedGems;
	public int serializedStoreUpgrade;
	public int serializedNewStoreUpgrade;
	public int effectsOn;
	public int musicOn;
	public int tuition;
	private TreeSerializeSystem defaultTreeSystem;

	public TreeSerializeSystem(bool getNewSystem, TreeSerializeSystem defaultSystem)
	{
		defaultTreeSystem = defaultSystem;

		if (getNewSystem)
		{
			CompileDefaultSystem(defaultSystem);
			LoadTreeSystem();
			SerializeSystem();
		}

		LoadTreeSystem();
	}

	public void CompileDefaultSystem(TreeSerializeSystem defaultTree)
	{
		PlayerPrefs.SetInt(nameof(serializedProgress), defaultTree.serializedProgress);
		PlayerPrefs.SetInt(nameof(serializedGems), defaultTree.serializedGems);
		PlayerPrefs.SetInt(nameof(serializedStoreUpgrade), defaultTree.serializedStoreUpgrade);
		PlayerPrefs.SetInt(nameof(serializedNewStoreUpgrade), defaultTree.serializedNewStoreUpgrade);
		PlayerPrefs.SetInt(nameof(effectsOn), defaultTree.effectsOn);
		PlayerPrefs.SetInt(nameof(musicOn), defaultTree.musicOn);
		PlayerPrefs.SetInt(nameof(tuition), defaultTree.tuition);
	}

	public void SerializeSystem()
	{
		PlayerPrefs.SetInt(nameof(serializedProgress), serializedProgress);
		PlayerPrefs.SetInt(nameof(serializedGems), serializedGems);
		PlayerPrefs.SetInt(nameof(serializedStoreUpgrade), serializedStoreUpgrade);
		PlayerPrefs.SetInt(nameof(serializedNewStoreUpgrade), serializedNewStoreUpgrade);
		PlayerPrefs.SetInt(nameof(effectsOn), effectsOn);
		PlayerPrefs.SetInt(nameof(musicOn), musicOn);
		PlayerPrefs.SetInt(nameof(tuition), tuition);
	}

	public void LoadTreeSystem()
	{
		serializedProgress = PlayerPrefs.GetInt(nameof(serializedProgress), defaultTreeSystem.serializedProgress);
		serializedGems = PlayerPrefs.GetInt(nameof(serializedGems), defaultTreeSystem.serializedGems);
		serializedStoreUpgrade = PlayerPrefs.GetInt(nameof(serializedStoreUpgrade), defaultTreeSystem.serializedStoreUpgrade);
		serializedNewStoreUpgrade = PlayerPrefs.GetInt(nameof(serializedNewStoreUpgrade), defaultTreeSystem.serializedNewStoreUpgrade);
		effectsOn = PlayerPrefs.GetInt(nameof(effectsOn), defaultTreeSystem.effectsOn);
		musicOn = PlayerPrefs.GetInt(nameof(musicOn), defaultTreeSystem.musicOn);
		tuition = PlayerPrefs.GetInt(nameof(tuition), defaultTreeSystem.tuition);
	}
}
