using UnityEngine;

public class RandomCoin : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject grabEffect;
	[SerializeField] private GameObject outerEffect;
	[SerializeField] private Collider2D coinCollider;
	[SerializeField] private float coinRadius;
	public float CoinRadius => coinRadius;

	public void GrabCoin()
	{
		coinCollider.enabled = false;
		grabEffect.gameObject.SetActive(true);
		outerEffect.gameObject.SetActive(false);
		spriteRenderer.enabled = false;
	}
}
