using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PerlinTest : MonoBehaviour
{
	public bool perlin1D;
	public bool perlin2D;
	public bool perlin3D;
	public int areaLength;
	public int areaHeight;
	public int areaWidth;
	public GameObject prefabCube;
	public float offset = 1f;
	public float wavelength = 2f;
	public float amplitude = 10f;
	private List<GameObject> spawnedCubes = new List<GameObject>();
	public bool update;
	
	
    // Start is called before the first frame update
    void Start()
    {
	    update = true;
    }

    // Update is called once per frame
    void Update()
    {
	    if (!update) return;

	    if (spawnedCubes != null)
		{
			foreach (GameObject cube in spawnedCubes)
			{
				Destroy(cube);
			}

			spawnedCubes.Clear();
		}
		
		//1d test
		if(perlin1D){
			for (int x = 0; x < areaLength; x++)
			{
				var y = Mathf.PerlinNoise1D(x/wavelength + offset) * amplitude;
				spawnedCubes.Add(Instantiate(prefabCube, new Vector3(x,y,1f), quaternion.identity));
			}
		}
		
		//2d test
		if(perlin2D)
		{
			for (int x = 0; x < areaLength; x++)
			{
				for (int z = 0; z < areaWidth; z++)
				{
					var y = Mathf.PerlinNoise(x / wavelength + offset, z / wavelength + offset) * amplitude;
					spawnedCubes.Add(Instantiate(prefabCube, new Vector3(x, y, z), quaternion.identity));
				}
			}
		}

		//3d test
		if(perlin3D)
		{
			for (int x = 0; x < areaLength; x++)
			{
				for (int y = 0; y < areaHeight; y++)
				{
					for (int z = 0; z < areaWidth; z++)
					{
						var xy = Mathf.PerlinNoise(x / wavelength + offset, y / wavelength + offset) * amplitude;
						var yz = Mathf.PerlinNoise(y / wavelength + offset, z / wavelength + offset) * amplitude;
						var xz = Mathf.PerlinNoise(x / wavelength + offset, z / wavelength + offset) * amplitude;
						var yx = Mathf.PerlinNoise(y / wavelength + offset, x / wavelength + offset) * amplitude;
						var zy = Mathf.PerlinNoise(z / wavelength + offset, y / wavelength + offset) * amplitude;
						var zx = Mathf.PerlinNoise(z / wavelength + offset, x / wavelength + offset) * amplitude;
						if ((xy + yz + xz + yx + zy + zx) / 6 > 0.5f)
						{
							spawnedCubes.Add(Instantiate(prefabCube, new Vector3(x, y, z), quaternion.identity));
						}
					}
				}
			}
		}
		//update = false;
    }
}
