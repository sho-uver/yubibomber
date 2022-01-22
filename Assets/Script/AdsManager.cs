using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameId = "4294088";
#else
    string gameId = "4294089";
#endif

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId);
        ShowBanner();
    }

    public void ShowBanner()
    {
        if (gameId == "4294088")
        {
            if (Advertisement.IsReady("BattleBanner"))
            {
                Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
                Advertisement.Banner.Show("BattleBanner");
            }
            else
            {
                StartCoroutine(RepeatShowBanner());
            }
        }
        else
        {
            if (Advertisement.IsReady("Banner_Android"))
            {
                Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
                Advertisement.Banner.Show("Banner_Android");
            }
            else
            {
                StartCoroutine(RepeatShowBanner());
            }
        }
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    IEnumerator RepeatShowBanner()
    {
        yield return new WaitForSeconds(1);
        ShowBanner();
    }

    public void PlayAd()
    {

        // Advertisement.Banner.Hide();
        if(gameId == "4294088")
        {
            if (Advertisement.IsReady("Interstitial_iOS"))
            {
                Advertisement.Show("Interstitial_iOS");
            }
        }
        else
        {
            if (Advertisement.IsReady("Interstitial_Android"))
            {
                Advertisement.Show("Interstitial_Android");
            }
        }
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
}
