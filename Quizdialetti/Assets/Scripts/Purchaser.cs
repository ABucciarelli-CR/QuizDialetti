using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchaser : MonoBehaviour 
{

	public void RemoveAds()
	{
		PurchaseManager.Instance.BuyRemoveAds();
	}
}
