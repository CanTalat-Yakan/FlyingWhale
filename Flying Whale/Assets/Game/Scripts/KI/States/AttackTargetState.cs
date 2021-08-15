using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.KI.States
{
	public class AttackTargetState : BaseState
	{
		private Enemy m_enemy;
		public float m_SpawnCooldown = 1f;
		private float m_elapsedTime;

		public AttackTargetState(Enemy _enemy) : base(_enemy.gameObject)
		{
			m_enemy = _enemy;
			m_elapsedTime = 0f;

		}

		public override Type Tick()
		{
			//Cooldown
			if (Wait())
			{
				m_enemy.Shoot();
			}
			return typeof(CheckForTargetState);
		}

		private bool Wait()
		{
			m_elapsedTime += Time.deltaTime;
			if (m_elapsedTime >= m_SpawnCooldown)
			{
				m_elapsedTime = 0;
				return true;
			}
			return false;
		}
	}
}
