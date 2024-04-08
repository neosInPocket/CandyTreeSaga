using UnityEngine;

public class SongsOperator : MonoBehaviour
{
	private AudioSource songSource;

	private static SongsOperator operatorInstance;

	private void Awake()
	{
		if (operatorInstance == null)
		{
			operatorInstance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			if (operatorInstance != this)
			{
				Destroy(gameObject);
			}
		}
	}

	private void Start()
	{
		songSource = GetComponent<AudioSource>();
		SetSongDefaultVolume();
	}

	public void SetSongDefaultVolume()
	{
		songSource.volume = SaveCompiler.CurrentSystem.musicOn == 1 ? 1f : 0f;
	}

	public void SetOperatorState(bool value)
	{
		songSource.volume = value ? 1f : 0f;
		SaveCompiler.CurrentSystem.musicOn = value ? 1 : 0;
		SaveCompiler.CurrentSystem.SerializeSystem();
	}

	public void SetOperatorEffectsState(bool value)
	{
		SaveCompiler.CurrentSystem.effectsOn = value ? 1 : 0;
		SaveCompiler.CurrentSystem.SerializeSystem();
	}
}
