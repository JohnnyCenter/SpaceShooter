using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCircleSpawner : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject[] spawnPoints; //An array of the spawn points assigned in the inspector
    public GameObject[] enemyTypes; //This array should be ordered, where the more difficult enemies are placed at the "bottom" of the array.
    //This can (probably) be used together with a "int difficulty" to unlock more difficult spawns based on different parameters
    private int numberOfEachEnemyTypeInPool;
    [Tooltip("How much time there is between each spawning cycle. Defaults to five")]
    [SerializeField] private float spawnTimer = 5f;
    [Tooltip("This int decides what type of enemy spawns each cycle. It is manualy set for now, will increment based on score later")]
    private int gameIntensety = 0;
    private int enemyLevel = 1;
    private List<GameObject> poolOfEnemyType1;
    private List<GameObject> poolOfEnemyType2;
    private List<GameObject> poolOfEnemyType3;


    private void Awake()
    {
        poolOfEnemyType1 = new List<GameObject>();
        poolOfEnemyType2 = new List<GameObject>();
        poolOfEnemyType3 = new List<GameObject>();
        numberOfEachEnemyTypeInPool = 8;
        for(int i = 0; i < numberOfEachEnemyTypeInPool; i ++)
        {
            InstantiateEnemies();
        }
    }
    private void Start()
    {
        gameIntensety = 3; // ;)
        StartCoroutine(StartSpawnTimer(spawnTimer));
        //gameIntensety = scrGameWaveManager.gameWaveManager.CurrentWave; //This reference does not work yet

    }
    private void Update() //This is just for testing!
    {
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            print("Game Intensity set to 1");
            gameIntensety = 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("Game Intensity set to 2");
            gameIntensety = 2;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("Game Intensity set to 3");
            gameIntensety = 3;
        }

    }
    private void RandomizeEnemies()
    {
        if(gameIntensety > 1)
        {
            enemyLevel = Random.Range(1, gameIntensety + 1); //Returns a random number between 1 and the game intensety
            print("Randomized gameintensity, enemyLevel is set to: " + enemyLevel);
        }
        else
        {
            gameIntensety = 1;
            print("Could not randomize enemies...");
        }
    }
    private void InstantiateEnemies()
    {
        foreach(GameObject enemy in enemyTypes)
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
                case 3:
                    poolOfEnemyType3.Add(newInstance); //Adds enemy to list 2
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
        InstantiateObjectsByType(enemyLevel);
    }
    private void InstantiateObjectsByType(int _type) //The type of enemy that spawns depends on the "gameIntensety" var
    {
        print("Spawning type is set to: " + _type);
        switch(_type)
        {
            case 1:
                print("Spawning type 1");
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    GameObject _enemy = poolOfEnemyType1[i]; //Get the enemy 0 for spawn point 0, enemy 1 for spawn point 1 and so on
                    //Check that the enemy is not currenty on screen
                    if(_enemy.GetComponent<scrEnemyStats>().IsVisibleOnScreen)
                    {
                        break;
                    }
                    //spawn enemy at i possition
                    _enemy.transform.position = spawnPoints[i].transform.position; //Move the enemy to the right possition
                    _enemy.transform.parent = null; //Remove parents
                    _enemy.SetActive(true); //Set the enemy to active
                }
                return;
            case 2:
                print("Spawning type 2");
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    //spawn enemy at i possition
                    GameObject _enemy = poolOfEnemyType2[i]; //Get the enemy 0 for spawn point 0, enemy 1 for spawn point 1 and so on
                    //Check that the enemy is not currenty on screen
                    if (_enemy.GetComponent<scrEnemyStats>().IsVisibleOnScreen)
                    {
                        break;
                    }
                    _enemy.transform.position = spawnPoints[i].transform.position; //Move the enemy to the right possition
                    _enemy.transform.parent = null; //Remove parents
                    _enemy.SetActive(true); //Set the enemy to active
                }
                return;
            case 3:
                print("Spawning type 3");
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    //spawn enemy at i possition
                    GameObject _enemy = poolOfEnemyType3[i]; //Get the enemy 0 for spawn point 0, enemy 1 for spawn point 1 and so on
                    //Check that the enemy is not currenty on screen
                    if (_enemy.GetComponent<scrEnemyStats>().IsVisibleOnScreen)
                    {
                        break;
                    }
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
        RandomizeEnemies();
        SpawnEnemies();
    }
}
