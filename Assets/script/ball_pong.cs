using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_pong : MonoBehaviour
{
	public Rigidbody2D rb;
	public float MoveForce;
	public float MoveForceUp;

	Vector2 dir;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	void Awake()
	{
		Invoke("RandomDir", 2.0f);
	}
	
	void RandomDir()
	{
		float force_x;
		float force_y = Random.Range(-1.0f,1.0f) * MoveForceUp;
		if(Random.value < 0.5f)
			force_x = 1.0f;
		else
			force_x = -1.0f;

		dir = new Vector2(force_x, force_y);

		rb.AddForce(dir * MoveForce);
	}

	void ResetBall()
	{
		rb.velocity = Vector2.zero;
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		RandomDir();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Goal"))
		{
			ResetBall();
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
