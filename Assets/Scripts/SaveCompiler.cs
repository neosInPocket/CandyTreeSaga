using UnityEngine;

public class SaveCompiler : MonoBehaviour
{
	[SerializeField] private bool defaultTreeSystem;
	[SerializeField] private TreeSerializeSystem defaults;
	public static TreeSerializeSystem CurrentSystem { get; private set; }

	private void Awake()
	{
		if (defaultTreeSystem)
		{
			CurrentSystem = new TreeSerializeSystem(true, defaults);
		}
		else
		{
			CurrentSystem = new TreeSerializeSystem(false, defaults);
		}
	}

	public void Compile()
	{
		CurrentSystem.SerializeSystem();
	}
}
