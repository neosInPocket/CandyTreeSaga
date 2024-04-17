using UnityEngine;

public class WindowTools
{
	public Vector3 Size => size;
	private Vector3 size;
	public float currentCameraYPosition => Camera.main.transform.position.y;

	public WindowTools(float yEdge)
	{
		size = new Vector4
		{
			x = Camera.main.orthographicSize * Camera.main.aspect,
			y = 2 * Camera.main.orthographicSize * yEdge - Camera.main.orthographicSize,
			z = Camera.main.orthographicSize
		};
	}

	public bool IsOutside(Vector2 position, float delta)
	{
		bool leftSideOverflow = position.x - delta < -size.x;
		bool rightSideOverflow = position.x + delta > size.x;
		bool topSideOverflow = position.y + delta > size.y + currentCameraYPosition;
		bool bottomSideOverflow = position.y - delta < -size.z + currentCameraYPosition;

		return leftSideOverflow || rightSideOverflow || topSideOverflow || bottomSideOverflow;
	}

	public float GetXCoordViaScreenSize(float value)
	{
		float result = size.x * value;
		return result;
	}
}
