
using System;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public interface IDamager : IRecipientMoney
{
    float GetDamage();
}

public interface IRecipientMoney
{
    void AddMoney(float _money);
}

public class PlayerController : MonoBehaviour,IDamager
{

    public InterAdb InterAdb;
    public GameObject vipStatus;
    public GameObject vipStatusDone;
    public int tryCount;
    
    public float damage = 1f;
    public float money;
    
    public Camera camera;
    public LayerMask layerClick;
    public GameObject moneyLabel;


     void Start()
    {
        MobileAds.Initialize(initStatus => { });
        PurchaseManager.OnPurchaseNonConsumable += purchaningNonConsumable;
            tryCount = PlayerPrefs.GetInt("tryCount");
    }

    private void purchaningNonConsumable(PurchaseEventArgs args)
    {
        if (money > (float)args.purchasedProduct.metadata.localizedPrice*1000f && vipStatus.activeInHierarchy)
        {
            vipStatus.SetActive(false);
            vipStatusDone.SetActive(true);
            Debug.Log("Purchase item : " + args.purchasedProduct.definition.id);
            Money(-(float) args.purchasedProduct.metadata.localizedPrice*1000f);
        }
    }
    
    void Update() {
        
        if (Input.GetMouseButtonDown(0)) { 
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction*10f, out hit, 1000f, layerClick)) {
                Debug.Log("Hit"+hit.transform.gameObject.name);
                if (hit.transform.gameObject.tag == "ClickDamageEntity")
                { 
                    hit.transform.gameObject.GetComponent<IHitable>().Hit(this);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction*10f);
        }
    }
    
    public float GetDamage()
    {
        return damage;
    }

    public void AddMoney(float _money)
    {
        Money(_money);
        Debug.Log("Stonks!!! +"+_money+"$");
        tryCount++;
        PlayerPrefs.SetInt("tryCount", tryCount);
        if (vipStatus.activeSelf)
            if (tryCount % 5 == 0)
                InterAdb.ShowAdBanner();
            

    }
    
    public void Money(float _money)
    {
        money += _money;
        moneyLabel.GetComponent<TextMeshProUGUI>().text = "Cash :"+money.ToString() + "$";
    }
    
}

