using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay
{
	public class EnemyProjectile : MonoBehaviour
	{
		private void Awake()
		{
		}

		void OnTriggerEnter(Collider col)
		{
			//all projectile colliding game objects should be tagged "Enemy" or whatever in inspector but that tag must be reflected in the below if conditional
			if (col.gameObject.tag == "Turret")
			{

				Cannon hitTarget = col.GetComponent<Cannon>();
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
}
