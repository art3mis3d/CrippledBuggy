using System.Collections.Generic;
using UnityEngine;

namespace ProjectX.StateMachine.ScriptableObjects
{
	public abstract class StateActionSO : DescriptionSMActionBaseSO
	{
		/// <summary>
		/// Will create a new custom <see cref="StateAction"/> or return an existing one inside <paramref name="createdInstances"/>
		/// </summary>
		internal StateAction GetAction(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
		{
			if (createdInstances.TryGetValue(this, out var obj))
				return (StateAction)obj;

			StateAction action = CreateAction();
			createdInstances.Add(this, action);
			action.OriginSO = this;
			action.Awake(stateMachine);
			return action;
		}
		protected abstract StateAction CreateAction();
		public float Speed { get; set; }
	}

	public abstract class StateActionSO<T> : StateActionSO where T : StateAction, new()
	{
		protected override StateAction CreateAction() => new T();
	}
}
