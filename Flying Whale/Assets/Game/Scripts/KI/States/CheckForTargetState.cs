using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.KI.States
{
	public class CheckForTargetState : BaseState
	{
		private Enemy m_enemy;
		public CheckForTargetState(Enemy _enemy) : base(_enemy.gameObject)
		{
			m_enemy = _enemy;
		}

		public override Type Tick()
		{
			SetDestination();
			SetRotation();
			//Destination Found
			return typeof(MoveToTargetState);
		}

		private void SetRotation()
		{
			//Set Orientation
			Vector3 fullVector = Vector3.Normalize(m_enemy.m_Destination - m_transform.position);
			m_enemy.m_Direction = new Vector3(fullVector.x, 1f, fullVector.z);    //No movement in Y axis                
			m_enemy.m_RotationToTarget = Quaternion.LookRotation(m_enemy.m_Direction);
		}

		private void SetDestination()
		{
			//Find Turret Game Object Nearest
			GameObject[] foundTurrets = GameObject.FindGameObjectsWithTag("Turret");
			if (foundTurrets.Length < 1)
			{
				foundTurrets = GameObject.FindGameObjectsWithTag("Player");
			}
			float MinDist = Mathf.Infinity;
			Vector3 curPos = m_transform.position;
			foreach (GameObject turret in foundTurrets)
			{
				//The Squared length of the vector takes less proccessing time
				Vector3 directionToTarget = turret.transform.position - curPos;
				float directionSquaredToTarget = directionToTarget.sqrMagnitude;

				if (directionSquaredToTarget < MinDist)
				{
					m_enemy.m_Destination = turret.transform.position;
					MinDist = directionSquaredToTarget;
				}
			}
		}
	}
}
