using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Domain : MonoBehaviour
{
	[SerializeField] private Vector2 randomScales;
	[SerializeField] private CircleCollider2D circleCaster;
	[SerializeField] private GameObject activated;
	[SerializeField] private GameObject deactivated;
	[SerializeField] private GameObject activeEffect;
	[SerializeField] private RandomCoin randomCoinInstance;
	[SerializeField] private float randomCoinSpawnChance;
	public float DomainRadius => circleCaster.radius * transform.localScale.x;
	public bool Activated { get; set; }

	private void Awake()
	{
		Activated = false;
	}

	public void RandomSpawnCoin(Vector2 screenSize, float y, float xEdgesOffset)
	{
		if (Random.Range(0, 1f) < randomCoinSpawnChance)
		{
			Instantiate(randomCoinInstance, GetRandomCoinPosition(screenSize, y, xEdgesOffset), Quaternion.identity, transform);
		}
	}

	public Vector2 GetRandomCoinPosition(Vector2 screenSize, float y, float xEdgesOffset)
	{
		Vector2 position = new Vector2();
		position.y = y;
		bool free = false;


		while (!free)
		{
			position.x = Random.Range(-screenSize.x + xEdgesOffset, screenSize.x - xEdgesOffset);
			if (!Physics2D.OverlapCircleAll(position, randomCoinInstance.CoinRadius).Any())
			{
				free = true;
			}
		}

		return position;
	}

	public void SetRandomLocalScale()
	{
		var domainScale = transform.localScale;
		float randomScale = Random.Range(randomScales.x, randomScales.y);
		domainScale.x = randomScale;
		domainScale.y = randomScale;
		domainScale.z = randomScale;

		transform.localScale = domainScale;
	}

	public void ActivateStatus()
	{
		activeEffect.gameObject.SetActive(true);
		activated.gameObject.SetActive(true);
		deactivated.gameObject.SetActive(false);

		Activated = true;
	}
}

public enum DomainStatus
{
	Activated,
	Deactivated
}
