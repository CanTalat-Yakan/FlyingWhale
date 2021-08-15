using Assets.Game.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private Rigidbody m_rigidbody;

	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		m_rigidbody.AddForce(transform.forward * 20, ForceMode.Impulse);
		Destroy(this.gameObject, 2); //Redundanter Code? Das Destroy wird beim instanzieren bereits aufgerufen
	}

	void OnTriggerEnter(Collider col)
	{
		//all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
		if (col.gameObject.tag == "Enemy")
		{

			Enemy hitTarget = col.GetComponent<Enemy>();
			hitTarget.HP -= 1;
			if (hitTarget.HP <= 0)
			{
				Destroy(col.gameObject);
				//add an explosion or something
				//destroy the projectile that just caused the trigger collision
			}
			Destroy(gameObject);
		}
	}
}
