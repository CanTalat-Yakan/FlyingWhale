using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Scripts.KI.States
{
	public class AttackTargetState : BaseState
	{
		public AttackTargetState(Enemy _enemy) : base(_enemy.gameObject)
		{

		}

		public override Type Tick()
		{
			throw new NotImplementedException();
		}
	}
}
