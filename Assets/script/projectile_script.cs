using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_script : MonoBehaviour
{
	public float DMG;
	public float speed;
	private Rigidbody2D rb;
	
    // Start is called before the first frame update
    void Awake()
    {
		Destroy(gameObject, 7.0f);
    }
}
