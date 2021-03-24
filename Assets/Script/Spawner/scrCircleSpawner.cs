using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCircleSpawner : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject[] spawnPoints; //An array of the spawn points assigned in the inspector
    public GameObject[] testEnemyType; //This array should be ordered, where the more difficult enemies are placed at the "bottom" of the array.
    //This can (probably) be used together with a "int difficulty" to unlock more difficult spawns based on different parameters
    private int numberOfEachEnemyTypeInPool;
    [Tooltip("How much time there is between each spawning cycle. Defaults to five")]
    [SerializeField] private float spawnTimer = 5f;
    [Tooltip("This int decides what type of enemy spawns each cycle. It is manualy set for now, will increment based on score later")]
    [SerializeField] private int gameIntensety = 1; //Make this increment based on score!
    private List<GameObject> poolOfEnemyType1;
    private List<GameObject> poolOfEnemyType2;


    private void Awake()
    {
        poolOfEnemyType1 = new List<GameObject>();
        poolOfEnemyType2 = new List<GameObject>();
        numberOfEachEnemyTypeInPool = 8;
        for(int i = 0; i < numberOfEachEnemyTypeInPool; i ++)
        {
            InstantiateEnemies();
        }
    }
    private void Start()
    {
        StartCoroutine(StartSpawnTimer(spawnTimer));
    }
    private void InstantiateEnemies()
    {
        foreach(GameObject enemy in testEnemyType)
        {
            GameObject newInstance = Instantiate(enemy, transform.position, Quaternion.identity, enemySpawner.transform); //Instantiates enemies with spawner as parent
            scrEnemyTypeDefiner _enemyType = newInstance.GetComponent<scrEnemyTypeDefiner>(); //Get the reference
            
            switch(_enemyType.EnemyType) //Sort enemies by type, into lists
            {
                case 1:
                    poolOfEnemyType1.Add(newInstance); //Adds enemy to list 1
                    newInstance.SetActive(false); //Hides it

                    break;
                case 2:
                    poolOfEnemyType2.Add(newInstance); //Adds enemy to list 2
                    newInstance.SetActive(false); //Hides it

                    break;
                default:
                    Debug.LogError("The variable EnemyType is not vallid for the instantiation switch in scrCircleSpawner. Make sure each enemy type has assigned a value");
                    return;
            }
        }
    }

    private void SpawnEnemies()
    {
        print("Spawning enemies");
        StartCoroutine(StartSpawnTimer(spawnTimer)); //Start the next cycle
        //Assign possitions:
        InstantiateObjectsByType(gameIntensety);
    }
    private void InstantiateObjectsByType(int _type) //The type of enemy that spawns depends on the "gameIntensety" var
    {
        switch(_type)
        {
            case 1:
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    //spawn enemy at i possition
                    GameObject _enemy = poolOfEnemyType1[i]; //Get the enemy 0 for spawn point 0, enemy 1 for spawn point 1 and so on
                    _enemy.transform.position = spawnPoints[i].transform.position; //Move the enemy to the right possition
                    _enemy.transform.parent = null; //Remove parents
                    _enemy.SetActive(true); //Set the enemy to active
                }
                return;
            case 2:
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    //spawn enemy at i possition
                    GameObject _enemy = poolOfEnemyType2[i]; //Get the enemy 0 for spawn point 0, enemy 1 for spawn point 1 and so on
                    _enemy.transform.position = spawnPoints[i].transform.position; //Move the enemy to the right possition
                    _enemy.transform.parent = null; //Remove parents
                    _enemy.SetActive(true); //Set the enemy to active
                }
                return;
        }
    }

    private IEnumerator StartSpawnTimer(float _timer)
    {
        yield return new WaitForSeconds(_timer);
        SpawnEnemies();
    }
}
