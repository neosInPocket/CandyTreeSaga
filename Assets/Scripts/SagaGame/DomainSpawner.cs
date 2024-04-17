using System.Collections.Generic;
using UnityEngine;

public class DomainSpawner : MonoBehaviour
{
	[SerializeField] private Domain domainPrefab;
	[SerializeField] private float firstDomainSpawnDistance;
	[SerializeField] private Vector2 domainSpawnDistanceDelta;
	[SerializeField] private float virtualPointerDistance;
	[SerializeField] private Arrower arrower;
	[SerializeField] private LineRenderer connector;
	[SerializeField] private LineRenderer progressConnector;
	[SerializeField] private GamePerformer gamePerformer;
	[Range(0, 1f)]
	[SerializeField] private float xOffsetValue;
	[Range(0, 1f)]
	[SerializeField] private float topEdgeShift;
	private List<Domain> allDomains;
	public Domain lastDomain;
	public int nextDomainIndex;
	private WindowTools windowTools;
	private float xOffset;

	private void Awake()
	{
		allDomains = new();

		connector.positionCount = 1;
		connector.SetPosition(0, Vector2.zero);
		progressConnector.positionCount = 1;
		progressConnector.SetPosition(0, Vector2.zero);
		arrower.ArrowerDomainTouched += OnPlayerDomainTouched;

		windowTools = new WindowTools(topEdgeShift);
		xOffset = windowTools.GetXCoordViaScreenSize(xOffsetValue);
		arrower.SetDefaultPosition();

		SpawnDomain(arrower.transform.position.y + firstDomainSpawnDistance);
		nextDomainIndex = 0;
	}

	private void Update()
	{
		if (arrower.transform.position.y + virtualPointerDistance > lastDomain.transform.position.y)
		{
			var ySpawnPosition = lastDomain.transform.position.y + Random.Range(domainSpawnDistanceDelta.x, domainSpawnDistanceDelta.y);
			SpawnDomain(ySpawnPosition);
		}
	}

	public void SpawnDomain(float yPosition)
	{
		var domain = Instantiate(domainPrefab, transform);
		domain.SetRandomLocalScale();

		Vector2 spawnPosition = new Vector2();
		spawnPosition.y = yPosition;
		spawnPosition.x = Random.Range(-windowTools.Size.x + domain.DomainRadius + xOffset, windowTools.Size.x - domain.DomainRadius - xOffset);
		domain.transform.position = spawnPosition;
		domain.RandomSpawnCoin(windowTools.Size, domain.transform.position.y, xOffset);

		connector.positionCount++;
		connector.SetPosition(connector.positionCount - 1, domain.transform.position);
		lastDomain = domain;
		domain.Activated = false;
		domain.name = allDomains.Count.ToString();
		allDomains.Add(domain);
	}

	public void OnPlayerDomainTouched(Domain domain)
	{
		if (domain != allDomains[nextDomainIndex])
		{
			Debug.Log(nextDomainIndex);
			arrower.DestroyArrower();
			return;
		}

		nextDomainIndex++;
		domain.ActivateStatus();

		progressConnector.positionCount++;
		progressConnector.SetPosition(progressConnector.positionCount - 1, domain.transform.position);
		gamePerformer.ArrowerDomainTouched();
	}

	private void OnDestroy()
	{
		arrower.ArrowerDomainTouched -= OnPlayerDomainTouched;
	}
}
