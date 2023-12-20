using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	public static PlatformManager Instance = null;

	[SerializeField]
	GameObject platformPrefab;
	

	void Awake()
	{
		if (Instance == null) 
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);
		
	}
	
	void SpawnPlatform()
	{
		Instantiate (platformPrefab, transform.position, platformPrefab.transform.rotation);
	}

}
