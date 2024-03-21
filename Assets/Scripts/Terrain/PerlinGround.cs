using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PerlinGround : MonoBehaviour
{
	public bool moving = false;
	public float moveSpeed = 1f;
	public int groundLength = 100;
	public GameObject groundPrefab;
	public float yOffset = 1f;
	public float wavelength = 2f;
	public float amplitude = 10f;
	public bool round = false;
	public int decimalPlaces = 1;
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
		    yOffset += 0.5f * moveSpeed * Time.deltaTime;
		    UpdateTerrain();
		    //SetTerrain();
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

    public void UpdateTerrain()
    {
	    if(spawnedSquares != null)
	    {
		    for (int i = 0; i < spawnedSquares.Count; i++)
		    {
			    var y = Mathf.PerlinNoise1D(i/wavelength + yOffset) * amplitude;

			    if (round)
			    {
				    y = Mathf.Round(y * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces);
			    }
			    //Debug.Log(y);
			    spawnedSquares[i].transform.localScale = new Vector3(1f, y, 0f);
		    }
	    }
    }
}
