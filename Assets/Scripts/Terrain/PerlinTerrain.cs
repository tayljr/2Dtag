using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PerlinTerrain : MonoBehaviour
{
	public GameObject prefabGround;
	public int areaLength;
	public int areaWidth;
	public float squareSize = 1.0f;
	public float xWavelength;
	public float yWavelength;
	public float xOffset;
	public float yOffset;
	public float amplitude;
	public bool invert;
	public bool update;
	private List<GameObject> spawnedGround = new List<GameObject>();
	private Vector3 currentPos;
	private GameObject currentSquare;
	
	
    // Start is called before the first frame update
    void Start()
    {
        SetTerrain();
    }

    // Update is called once per frame
    void Update()
    {
	    if(!update) return;
	    SetTerrain();
    }

    public void RandomOffset()
    {
	    xOffset = Random.Range(-9999f, 9999f);
	    yOffset = Random.Range(-9999f, 9999f);
    }

    public void SetTerrain()
    {
	    if (squareSize < 1) update = false;
	    if (spawnedGround != null)
	    {
		    foreach (GameObject cube in spawnedGround)
		    {
			    Destroy(cube);
		    }
		    spawnedGround.Clear();
	    }
		for (float x = 0; x < areaLength; x += squareSize)
		{
			for (float y = 0; y < areaWidth; y += squareSize)
			{
				var z = Mathf.PerlinNoise(x / xWavelength + xOffset, y / yWavelength + yOffset) * amplitude;
				currentPos = transform.position;
				z -= 0.5f;
				if (invert) z = -z;
				if (z > 0)
				{
					currentSquare = Instantiate(prefabGround, new Vector3(currentPos.x + x, currentPos.y +y, 1), quaternion.identity);
					if (squareSize < 0.1f)
					{
						squareSize = 0.1f;
					}
					currentSquare.transform.localScale = new Vector2(squareSize, squareSize);
					spawnedGround.Add(currentSquare);
				}
			}
		}

    }
}
