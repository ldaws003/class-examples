using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
	
	public float HP;
	public float Atk;
	public float speed;
	public uint points;
	public GameObject Player;
	public GameObject Projectile;
	private float movementX;
	private float movementY;
	private float CoolDown;
	public float FiringInterval;
	private Rigidbody2D rb;
	public float BulletSpeed;
	public bool ShouldRotate;
	public float RotationStep;
	public GameObject RestoreHP;
	private GameObject GameController;
	
	void Awake()
	{
		Player = GameObject.FindWithTag("Player");
		GameController = GameObject.FindWithTag("GameController");
	}
	
	void Attack()
	{
		// create a bullet and then fires it towards the player
		GameObject Bullet = Instantiate(Projectile, gameObject.transform.position, Quaternion.identity);
		
		
		Vector2 BulletDir = Player.transform.position - Bullet.transform.position;
		BulletDir.Normalize();
		
		Bullet.GetComponent<Rigidbody2D>().AddForce(BulletDir * BulletSpeed);
		
		
		//rbBullet.AddForce(Vector2.up * BulletSpeed);
		CoolDown = FiringInterval;
	}
	
	void FixedUpdate()
	{
		if(CoolDown <= 0)
		{
			Attack();
		}
		else 
		{
			CoolDown -= Time.deltaTime;
		}
		
		FollowPlayer();
	}
	
	void FollowPlayer()
	{
		Vector3 currentPosition = transform.position; 
		Vector3 playerCurrentPosition = Player.transform.position;
		
		transform.position = Vector3.MoveTowards(currentPosition, playerCurrentPosition, speed * Time.fixedDeltaTime);
		
		if(ShouldRotate)
		{
			
			//Get the angle between the points
			float angle = AngleBetweenTwoPoints(transform.position, Player.transform.position);
 
			transform.rotation = Quaternion.Euler (new Vector3(0f,0f,angle + 90.0f));
		}
	}
	
	
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) 
	{
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
	
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("PlayerAttack"))
		{
			TakeDamage(col.gameObject.GetComponent<BulletScript>().DMG);
		}
	}
	
	void TakeDamage(float dmg)
    {
        float result;
        result = HP - dmg;
        if(result < 0)
        {
            HP = 0;
			Player.GetComponent<PlayerController>().UpdateScore(points);
			GameController.GetComponent<ShootEmUpController>().decreaseEnemyCount();
			
			if(Random.value < 0.5)
				Instantiate(RestoreHP, transform.position, Quaternion.identity);
			
			Destroy(gameObject, 0.0f);
        }
        else
        {
            HP = result;
        }
    }
	

}
