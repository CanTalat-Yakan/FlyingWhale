using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.KI.States
{
	public class MoveToTargetState : BaseState
	{
		private Enemy m_enemy;
		public float m_StoppingDistanceSquared = 5;
		public float m_TurnSpeed;
		public MoveToTargetState(Enemy _enemy) : base(_enemy.gameObject)
		{
			m_enemy = _enemy;
		}

		public override Type Tick()
		{
			Vector3 directionToTarget = m_enemy.m_Destination - m_transform.position;
			float directionSquaredToTarget = directionToTarget.sqrMagnitude;
			Debug.Log(directionSquaredToTarget);
			if (directionSquaredToTarget <= m_StoppingDistanceSquared)
			{
				return typeof(AttackTargetState);
			}

			m_transform.rotation = Quaternion.Slerp(m_transform.rotation, m_enemy.m_RotationToTarget, Time.deltaTime * m_TurnSpeed);
			m_transform.LookAt(m_enemy.m_Destination);
			//If forward blocked?

			m_transform.Translate(Vector3.forward * Time.deltaTime * m_enemy.m_MovementModifier);

			return null;
		}
	}
}
