using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
	public float Healing;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// add function for up and down movement for the ham to show that it restore hp
	void hover()
	{
		
	}
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Player"))
		{
			Destroy(gameObject, 0.2f);
		}
	}
}
