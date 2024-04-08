using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBox : MonoBehaviour
{
	[SerializeField] private int gems;
	[SerializeField] private TMP_Text timesPurchased;
	[SerializeField] private TMP_Text buttonPurchaseText;
	[SerializeField] private TMP_Text gemsSetter;
	[SerializeField] private Button buttonPurchase;
	[SerializeField] private MainStoreSetter mainStoreSetter;
	[SerializeField] private int mainUpgrade;
	public bool MainUpgrade => mainUpgrade == 1;

	private void Start()
	{
		SetMainBoxValues();
	}

	public void SetMainBoxValues()
	{
		gemsSetter.text = gems.ToString();

		if (MainUpgrade)
		{
			timesPurchased.text = $"{SaveCompiler.CurrentSystem.serializedStoreUpgrade}/10";
			CheckButtonStatus(SaveCompiler.CurrentSystem.serializedStoreUpgrade, gems);
		}
		else
		{
			timesPurchased.text = $"{SaveCompiler.CurrentSystem.serializedNewStoreUpgrade}/10";
			CheckButtonStatus(SaveCompiler.CurrentSystem.serializedNewStoreUpgrade, gems);
		}
	}

	public void CheckButtonStatus(int alreadyUpgraded, int gemsCost)
	{
		if (alreadyUpgraded == 10)
		{
			buttonPurchase.interactable = false;
			buttonPurchaseText.text = "upgraded";
			buttonPurchaseText.color = Color.white;
		}
		else
		{
			if (SaveCompiler.CurrentSystem.serializedGems >= gemsCost)
			{
				buttonPurchase.interactable = true;
				buttonPurchaseText.text = "upgrade";
				buttonPurchaseText.color = Color.white;
			}
			else
			{
				buttonPurchase.interactable = false;
				buttonPurchaseText.text = "NO gems";
				buttonPurchaseText.color = Color.red;
			}
		}
	}

	public void GetNewUpgrade()
	{
		if (MainUpgrade)
		{
			SaveCompiler.CurrentSystem.serializedStoreUpgrade++;
		}
		else
		{
			SaveCompiler.CurrentSystem.serializedNewStoreUpgrade++;
		}

		SaveCompiler.CurrentSystem.serializedGems -= gems;
		SaveCompiler.CurrentSystem.SerializeSystem();

		mainStoreSetter.CheckStoreSystem();
	}
}
