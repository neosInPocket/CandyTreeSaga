using UnityEngine;

[CreateAssetMenu(menuName = "UpgradesInfo")]
public class UpgradesInformation : ScriptableObject
{
	[SerializeField] private float[] firstUpgradesValues;
	[SerializeField] private float[] secondUpgradesValues;
	public float FirstUpgradeValue => firstUpgradesValues[SaveCompiler.CurrentSystem.serializedStoreUpgrade];
	public float SecondUpgradeValue => secondUpgradesValues[SaveCompiler.CurrentSystem.serializedNewStoreUpgrade];
}
