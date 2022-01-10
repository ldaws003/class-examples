using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// fix hp buff

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementX;
    private float movementY;
	public float Speed;
    public float HP;
	public float maxHP;
	
	private float FireCoolDown;
	public float CoolDownTime;
	public GameObject Bullet;
	public GameObject BulletOrigin;
	public float BulletSpeed;
	private Animator Anim;
	
	public float TrueMaxHP;
	public GameObject ShootEmUpController;
	public uint GunLevel; 
	private uint MaxGunLevel; 
	public float animWalkThreshold;
	
	
	
	public TextMeshProUGUI scoreText;
	public RawImage HPBar;
	RectTransform HPBarWidth;
	private float OriginalSizeHP; 
	private uint score;
	
	// types of buff would be included: gun level, firing rate buffs, MaxHP buffs, MaxSpeedBuffs

    // Start is called before the first frame update
    void Awake()
    {
		ShootEmUpController = GameObject.FindWithTag("GameController");
        rb = gameObject.GetComponent<Rigidbody2D>();
		score = 0;
		scoreText.text = score.ToString();
		FireCoolDown = 0;
		Anim = gameObject.GetComponent<Animator>();
		HPBarWidth = HPBar.GetComponent<RectTransform>();
		OriginalSizeHP = HPBarWidth.sizeDelta.x;
    }
	
	void UpdateHPBar()
	{
		HPBarWidth.sizeDelta = new Vector2(OriginalSizeHP * (HP / maxHP), HPBarWidth.sizeDelta.y);
	}

    void Move(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
    }
	
	public void UpdateScore(uint addScore)
	{
		score += addScore;
		scoreText.text = score.ToString();
	}

    void TakeDamage(float dmg)
    {
        float result;
        result = HP - dmg;
        if(result < 0)
        {
            HP = 0;
			Death();
        }
        else
        {
            HP = result;
        }
		
		UpdateHPBar();
    }
	
	void RestoreHealth(float restore)
	{
		float result;
        result = HP + restore;
        if(result > maxHP)
        {
            HP = maxHP;
        }
        else
        {
            HP = result;
        }
		
		UpdateHPBar();
	}
	
	void Death()
	{
		ShootEmUpController.GetComponent<ShootEmUpController>().GameOver();
	}
	
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy Projectile"))
        {
            TakeDamage(col.gameObject.GetComponent<projectile_script>().DMG);
            col.gameObject.SetActive(false);
            Destroy(col.gameObject, 0.1f);
        }
		
		if(col.CompareTag("Enemy"))
		{
			TakeDamage(col.gameObject.GetComponent<EnemyScript>().Atk);
		}
		
		if(col.CompareTag("HealingItem"))
		{
			RestoreHealth(col.gameObject.GetComponent<HealingItem>().Healing);
			//col.gameObject.SetActive(false);
			Destroy(col.gameObject);
		}
		
		/*
		if(col.CompareTag("Buff"))
		{
			if(GunLevel < MaxGunLevel)
			{
				GunLevel++;
			}
			
			col.gameObject.SetActive(false);
		}
		*/

        //include animation for taking damage
    }

    void FixedUpdate()
    {
		
        Vector2 movement = new Vector2(movementX, movementY);
		rb.AddForce(movement * Speed);		
		
		if(FireCoolDown > 0)
			FireCoolDown -= Time.deltaTime;
		
		if(FireCoolDown <= 0 && Input.GetKey(KeyCode.Mouse0))
		{ 
			GameObject FiredBullet = Instantiate(Bullet, BulletOrigin.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
			Vector3 playerVel = gameObject.GetComponent<Rigidbody2D>().velocity;
			Vector3 vel = new Vector3(playerVel.x, playerVel.y, playerVel.z);
			Rigidbody2D rbBullet = FiredBullet.GetComponent<Rigidbody2D>();
			rbBullet.velocity = vel;
			rbBullet.AddForce(BulletOrigin.transform.up * BulletSpeed, ForceMode2D.Impulse);
			
			FireCoolDown = CoolDownTime;			
		}
		
    }
	
	void LateUpdate()
	{
		//Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
         
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) + 90.0f;
 
        transform.rotation = Quaternion.Euler (new Vector3(0f,0f,angle));		
	}
	
	void Update()
	{
		Vector3 vel = rb.velocity;
		
		if(vel.magnitude > animWalkThreshold)
		{
			Anim.SetInteger("walk", 1);
		}
		else 
		{
			Anim.SetInteger("walk", 0);
		}
	}
	
	void OnMove(InputValue movementValue)
	{
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
		
	}
	
	float AngleBetweenTwoPoints(Vector3 a, Vector3 b) 
	{
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
