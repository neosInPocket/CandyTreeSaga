using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class HoldCameraScript : CinemachineExtension
{
	[SerializeField] private CinemachineVirtualCamera vCam;
	[SerializeField] private Transform target;
	[SerializeField] private float targetZPosition = -100;
	private const float constantXPosition = 0;
	private float lastYPosition;

	private void Start()
	{
		lastYPosition = 0;
		transform.position = Vector2.zero;
	}

	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		var pos = state.RawPosition;

		if (stage == CinemachineCore.Stage.Body)
		{
			pos.x = constantXPosition;
			pos.z = targetZPosition;
			state.RawPosition = pos;
		}
	}

	private void Update()
	{
		if (transform.position.y < lastYPosition)
		{
			vCam.Follow = null;
		}

		if (target.transform.position.y > lastYPosition)
		{
			vCam.Follow = target;

			lastYPosition = transform.position.y;
		}
	}
}
