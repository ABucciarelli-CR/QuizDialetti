using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
	public static AdsManager Instance { set; get; }
	public bool AdsActive = true;
	// Update is called once per frame
	void Update () 
	{
		/*
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartSkippableAds();
		}*/
	}

	public void StartSkippableAds()
	{
		if (AdsActive)
		{
			if (Advertisement.IsReady("video"))
			{
				Advertisement.Show("video");
			}
		}
	}
	
	public void StartUnskippableAds()
	{
		if (AdsActive)
		{
			if (Advertisement.IsReady("rewardedVideo"))
			{
				Advertisement.Show("rewardedVideo");
			}
		}
	}
}
