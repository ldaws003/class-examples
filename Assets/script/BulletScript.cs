using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public float DMG;
	public float speed;
	private Rigidbody2D rb;
	
    // Start is called before the first frame update
    void Awake()
    {
		Destroy(gameObject, 4.0f);
    }
}
