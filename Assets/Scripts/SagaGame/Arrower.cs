using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ArrowerTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using ArrowerFinger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using System;

public class Arrower : MonoBehaviour
{
	[SerializeField] private UpgradesInformation upgradesInformation;
	[SerializeField] private Rigidbody2D engine;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float radius;
	[SerializeField] private Transform blowEffect;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Vector2 defaultPositionState;
	[Range(0, 1f)]
	[SerializeField] private float topEdgeShift;
	public ArrowerState State
	{
		get => state;
		set
		{
			state = value;
			SetArrowerState();
		}
	}
	private ArrowerState state;
	private float rollSpeed;
	private float acceleration;
	private WindowTools windowTools;
	private bool roll;
	private Vector3 rollCoordinates;
	public Action<Arrower> ArrowerBlow { get; set; }
	public Action<Domain> ArrowerDomainTouched { get; set; }
	public Action CoinGrab { get; set; }

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		State = ArrowerState.Disabled;
	}

	private void Start()
	{
		windowTools = new(topEdgeShift);
		rollCoordinates = transform.eulerAngles;
		acceleration = upgradesInformation.FirstUpgradeValue;
		rollSpeed = upgradesInformation.SecondUpgradeValue;
	}

	public void SetDefaultPosition()
	{
		transform.position = defaultPositionState;
	}

	private void Update()
	{
		if (State == ArrowerState.Disabled) return;

		if (windowTools.IsOutside(transform.position, radius))
		{
			DestroyArrower();
		}

		engine.velocity += acceleration * (Vector2)transform.up * Time.deltaTime;
		if (engine.velocity.magnitude > maxSpeed)
		{
			engine.velocity = (Vector2)transform.up * maxSpeed;
		}

		if (roll)
		{
			rollCoordinates.z += rollSpeed * Time.deltaTime;
			transform.eulerAngles = rollCoordinates;
		}
	}

	public void DestroyArrower()
	{
		State = ArrowerState.Disabled;
		spriteRenderer.enabled = false;
		blowEffect.gameObject.SetActive(true);
		ArrowerBlow?.Invoke(this);
	}

	public void ArrowerRollCallbackDown(ArrowerFinger finger)
	{
		roll = true;
	}

	public void ArrowerRollCallbackUp(ArrowerFinger finger)
	{
		roll = false;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Domain>(out Domain domain))
		{
			if (!domain.Activated)
			{
				domain.Activated = true;
				ArrowerDomainTouched?.Invoke(domain);
			}
		}

		if (collider.TryGetComponent<RandomCoin>(out RandomCoin coin))
		{
			coin.GrabCoin();
			CoinGrab?.Invoke();
		}
	}

	public void SetArrowerState()
	{
		if (state == ArrowerState.Enabled)
		{
			ArrowerTouch.onFingerDown += ArrowerRollCallbackDown;
			ArrowerTouch.onFingerUp += ArrowerRollCallbackUp;
			engine.constraints = RigidbodyConstraints2D.None;
			return;
		}

		if (state == ArrowerState.Disabled)
		{
			ArrowerTouch.onFingerDown -= ArrowerRollCallbackDown;
			ArrowerTouch.onFingerUp -= ArrowerRollCallbackUp;
			engine.velocity = Vector2.zero;
			engine.constraints = RigidbodyConstraints2D.FreezeAll;
			roll = false;
			return;
		}
	}

	private void OnDestroy()
	{
		ArrowerTouch.onFingerDown -= ArrowerRollCallbackDown;
		ArrowerTouch.onFingerUp -= ArrowerRollCallbackUp;
		ArrowerDomainTouched = null;
		ArrowerBlow = null;
	}
}


public enum ArrowerState
{
	Enabled,
	Disabled
}
