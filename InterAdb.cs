
using UnityEngine;
using GoogleMobileAds.Api;
public class InterAdb : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    public string interstitialUnitId = "ca-app-pub-5474643765791352/2193851325";

    private void OnEnable()
    {
        interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
        
    }

    public void ShowAdBanner()
    {
       // if(interstitialAd.IsLoaded())
            interstitialAd.Show();
    }
    
    
    
}
