using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PerlinTest : MonoBehaviour
{
	[FormerlySerializedAs("cubeNumber")]
	public int areaLength;
	public int areaWidth;
	public GameObject prefabCube;
	public float yOffset = 1f;
	public float wavelength = 2f;
	public float amplitude = 10f;
	private List<GameObject> spawnedCubes = new List<GameObject>();
	
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
	    if(spawnedCubes != null)
	    {
		    foreach (GameObject cube in spawnedCubes)
		    {
			    Destroy(cube);
		    }
		    spawnedCubes.Clear();
	    }
	    /*
	    //1d test
	    
	    for (int i = 0; i < cubeNumber; i++)
	    {
		    var y = Mathf.PerlinNoise1D(i/wavelength) * amplitude;
		    spawnedCubes.Add(Instantiate(prefabCube, new Vector3(i,y,1f), quaternion.identity));
	    }
	    prefabCube.transform.position = new Vector3(1, Mathf.PerlinNoise1D(Time.time)*wavelength, 1);
	    //Debug.Log(Time.time); 
	    */
	    //2d test
	    for (int i = 0; i < areaLength; i++)
	    {
		    for (int j = 0; j < areaWidth; j++)
		    {
		    var y = Mathf.PerlinNoise(i / wavelength + yOffset, j / wavelength + yOffset) * amplitude;
		    spawnedCubes.Add(Instantiate(prefabCube, new Vector3(i,y,j), quaternion.identity));
		    }
	    }
    }
}
