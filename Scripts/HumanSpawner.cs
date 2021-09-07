using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public List<GameObject> humanVariants;
	public float periods;
	public float deltaRandomPeriods;
	public float xCoordsMax;
	public float xCoordsMin;
	public float yCoordsMax;
	public float yCoordsMin;
	private int counter;
	
	
    void Awake()
    {
        Invoke(nameof(SpawnInstance), SetTimer());
		counter = 0;
    }
    

    private void SpawnInstance()
    {
	    int randomPrefabIndex = Random.Range(0, humanVariants.Count);
	    Vector3 coords = new Vector3(Random.Range(xCoordsMin, xCoordsMax), Random.Range(yCoordsMin, yCoordsMax), 0);
	    GameObject newPOV = Instantiate(humanVariants[randomPrefabIndex], coords, Quaternion.identity, this.transform);
	    newPOV.name = "human"+counter.ToString();
	    counter++;
	    Invoke(nameof(SpawnInstance), SetTimer());
    }
	
	private float SetTimer()
	{
		float timer = periods + Random.Range(-deltaRandomPeriods, deltaRandomPeriods);
		return timer;
	}
}
