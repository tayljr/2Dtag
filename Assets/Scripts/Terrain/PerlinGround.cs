using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PerlinGround : MonoBehaviour
{
	public bool moving = false;
	public int groundLength = 100;
	public GameObject groundPrefab;
	[FormerlySerializedAs("startY")]
	public float yOffset = 1f;
	public float wavelength = 2f;
	public float amplitude = 10f;
	private List<GameObject> spawnedSquares = new List<GameObject>();
	private Vector3 currentPos;
	
    // Start is called before the first frame update
    void Start()
    {
        SetTerrain();
    }

    // Update is called once per frame
    void Update()
    {
	    if(moving)
	    {
		    yOffset += 0.5f * Time.deltaTime;
		    SetTerrain();
	    }
    }

    public void SetTerrain()
    {
	    if(spawnedSquares != null)
	    {
		    foreach (GameObject square in spawnedSquares)
		    {
			    Destroy(square);
		    }
		    spawnedSquares.Clear();
	    }

	    for (int i = 0; i < groundLength; i++)
	    {
		    var y = Mathf.PerlinNoise1D(i/wavelength + yOffset) * amplitude;
			currentPos = transform.position;
		    currentPos.x = currentPos.x + i;
		    GameObject newSquare = Instantiate(groundPrefab, currentPos, Quaternion.identity);
		    newSquare.transform.localScale = new Vector3(1f, y, 0f);
			spawnedSquares.Add(newSquare);
	    }
    }
}
