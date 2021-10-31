
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IHitable
{
     void Hit(IDamager damager);
}

public interface IPursuer
{
    void Haunt(Vector3 positionMoving);
}

public class FlyController : MonoBehaviour,IHitable,IPursuer
{
    [Header("Player")]
    public GameObject player;
    [Header("Attribute fly")]
    public float healthMax = 1f;
    public float armor;
    public float health;
    [Header("Get paid for fly")]
    public float stonks;

    public void Start()
    {
        health = healthMax;
    }

    public void Hit(IDamager damager)
    {
        health -= Mathf.Max(Mathf.Abs(damager.GetDamage()-armor),0.05f);
        if (health <= 0f)
        {
            damager.AddMoney(stonks);
            StartCoroutine(DeathFly());
        }

    }
    public void Haunt(Vector3 positionMoving)
    {
        gameObject.transform.position = positionMoving;
    }
    IEnumerator DeathFly()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        float random = Mathf.PerlinNoise(transform.position.x*10f, Time.time);
        rigidbody.AddForce((Vector3.up*10f+Vector3.forward*4f)*random+
                           Vector3.Max(Vector3.right*15f*(random-0.5f),Vector3.zero)+
                           Vector3.Min(Vector3.right*15f*(random-0.5f),Vector3.zero), ForceMode.Impulse);
        Debug.Log(gameObject.name+" is Death");
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
        Debug.Log(gameObject.GetInstanceID()+" is Destroy");
    }
}
