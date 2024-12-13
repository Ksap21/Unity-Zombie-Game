using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombieSpawn : MonoBehaviour
{

    public int initialZombiePerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f;
    public int currentWave = 0;
    public float waveCooldown=10.0f;

    public bool isCoolDown;
    public float CoolDownCounter;



    public List<Enemy> currentZombieAlive;
    public GameObject zombiePrefab;
    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI coolDownCounterUI;

    // Start is called before the first frame update
    void Start()
    {
        currentZombiesPerWave = initialZombiePerWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombieAlive.Clear();
        currentWave++;
        StartCoroutine(SpawnWave());

    }


    private IEnumerator SpawnWave()
    {
        for(int i=0; i<currentZombiesPerWave; i++)
        {
            Vector3 spawnOffset=new Vector3(Random.Range(-1f, 1f),
                0f, Random.Range(-1f, 1f));
        Vector3 spawnPosition = transform.position + spawnOffset;

        var Zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = Zombie.GetComponent<Enemy>();

        currentZombieAlive.Add(enemyScript);

        yield return new WaitForSeconds(spawnDelay);
         }
    }
    // Update is called once per frame
     

private void Update()
{
    List<Enemy> zombiesToRemove = new List<Enemy>();
    foreach(Enemy zombie in currentZombieAlive)
    {
        if(zombie.isDead)
        {
            zombiesToRemove.Add(zombie);
        }
    }

    foreach(Enemy zombie in zombiesToRemove)
    {
            currentZombieAlive.Remove(zombie);
    }
    zombiesToRemove.Clear();

    if(currentZombieAlive.Count==0 && isCoolDown==false)
    {
        StartCoroutine(WaveCooldown());
    }

    if(isCoolDown)
    {
            CoolDownCounter -= Time.deltaTime;
    }

    else
    {
            CoolDownCounter = waveCooldown;
    }

    coolDownCounterUI.text=CoolDownCounter.ToString();
}

    private IEnumerator WaveCooldown()
    {
        isCoolDown = true;
        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);

        isCoolDown = false;
        waveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2;

        StartNextWave();
    }
    
}
