using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.KI;
using Assets.Game.Scripts.KI.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
	public Vector3 m_Destination;
	public Quaternion m_RotationToTarget;
	public Vector3 m_Direction;
	public EnemyState m_CurrentState;
	public float m_MovementModifier = 5f;

	public Transform Target { get; private set; }

	public StateMachine StateMachine;

	// Start is called before the first frame update
	void Start()
	{
		StateMachine = GetComponent<StateMachine>();

		Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>()
		{
			{ typeof(CheckForTargetState), new CheckForTargetState(this) },
			{ typeof(MoveToTargetState), new MoveToTargetState(this) },
			{ typeof(AttackTargetState), new AttackTargetState(this) },
		};
		StateMachine.SetStates(states);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
