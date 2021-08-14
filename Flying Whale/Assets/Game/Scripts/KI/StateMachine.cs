using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.KI
{
	public class StateMachine : MonoBehaviour
	{
		private Dictionary<Type, BaseState> m_avalableStates;
		public BaseState CurrentState { get; private set; }

		public event Action<BaseState> OnStateChanged;

		public void SetStates(Dictionary<Type, BaseState> _states)		{

			m_avalableStates = _states;
		}

		private void Update()
		{
			if(CurrentState == null)
			{
				CurrentState = m_avalableStates.Values.First();
			}

			var nextState = CurrentState?.Tick();

			if(nextState != null &&
				nextState != CurrentState?.GetType())
			{
				SwitchToNextState(nextState);
			}
		}

		private void SwitchToNextState(Type nextState)
		{
			CurrentState = m_avalableStates[nextState];
			OnStateChanged?.Invoke(CurrentState);
		}
	}
}
