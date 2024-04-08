using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderView : MonoBehaviour
{
	[SerializeField] private Image musicState;
	[SerializeField] private Image effectsState;
	[SerializeField] private Animator foldAnimator;
	[SerializeField] private Sprite activatedSprite;
	[SerializeField] private Sprite deactivated;
	private SongsOperator songsOperator;

	public bool effectsOn;
	public bool musicOn;
	public bool folded { get; set; } = true;

	private void Start()
	{
		songsOperator = FindObjectOfType<SongsOperator>();

		effectsOn = SaveCompiler.CurrentSystem.effectsOn == 1;
		musicOn = SaveCompiler.CurrentSystem.musicOn == 1;
		effectsState.sprite = effectsOn ? deactivated : activatedSprite;
		musicState.sprite = musicOn ? deactivated : activatedSprite;
	}

	public void ChangeFoldState()
	{
		folded = !folded;
		foldAnimator.SetBool("folded", folded);
	}

	public void ChangeEffectsState()
	{
		effectsOn = !effectsOn;
		songsOperator.SetOperatorEffectsState(effectsOn);
		effectsState.sprite = effectsOn ? deactivated : activatedSprite;
	}

	public void ChangeSongState()
	{
		musicOn = !musicOn;
		songsOperator.SetOperatorState(musicOn);
		musicState.sprite = musicOn ? deactivated : activatedSprite;
	}
}
