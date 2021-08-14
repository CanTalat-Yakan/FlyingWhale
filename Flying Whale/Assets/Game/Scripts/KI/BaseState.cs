using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.KI
{
	public abstract class BaseState
	{
		protected GameObject m_gameObject;
		protected Transform m_transform;

		public BaseState(GameObject _gameObject)
		{
			m_gameObject = _gameObject;
			m_transform = _gameObject.transform;
		}

		public abstract Type Tick();
	}
}
