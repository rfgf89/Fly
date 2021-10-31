
using UnityEngine;

public class SpawnRegion : MonoBehaviour
{
    private Vector3 spawnArea;

    private void Awake()
    {
        spawnArea = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y,0.1f);
    }

    void Update()
    {
   
        foreach (Transform child in gameObject.transform)
        {
            Spawner spawner = child.gameObject.GetComponent<Spawner>();
            
            if (spawner != null && spawner.readySpawn)
            {
                
                spawner.Spawn(gameObject.transform.position-spawnArea/2f+new Vector3(Random.Range(0f, spawnArea.x),Random.Range(0f, spawnArea.y),0f));
                spawner.readySpawn = false;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f,0.0f,0.0f,0.5f);
        Gizmos.DrawCube(transform.position, spawnArea);
        
    }
}
