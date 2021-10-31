using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawn
{
    void Spawn();
}

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float interval;
    public bool readySpawn;


    public void Spawn(Vector3 positionMoving)
    {
     
        if (readySpawn)
        {
            Instantiate(spawnObject, transform.position, spawnObject.transform.rotation)
                .GetComponent<IPursuer>()?.Haunt(positionMoving);
         

            StartCoroutine(CoolDownSpawn());
            
        }

 
    }

    IEnumerator CoolDownSpawn()
    {
        yield return new WaitForSeconds(interval);
        readySpawn = true;
    }
}


