using System.Collections.Generic;
using BaseClasses.Enums;
using Unity.Collections;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState", order = 0)]
	public class GameStateSO : DescriptionBaseSO
	{
		public GameState CurrentGameState => _currentGameState;

		[Header("Game States")] [SerializeField] [ReadOnly]
		private GameState _currentGameState;

		[SerializeField] [ReadOnly] private GameState _previousGameState;

		[Header("Broadcasting On")] [SerializeField]
		private BoolEventChannelSO _onCombatStateEvent;

		private List<Transform> _alertEnemies;

		private void Start()
		{
			_alertEnemies = new List<Transform>();
		}

		public void AddAlertEnemy(Transform enemy)
		{
			if (!_alertEnemies.Contains(enemy))
			{
				_alertEnemies.Add(enemy);
			}

			UpdateGameState(GameState.Combat);
		}

		public void RemoveAlertEnemy(Transform enemy)
		{
			if (_alertEnemies.Contains(enemy))
			{
				_alertEnemies.Remove(enemy);

				if (_alertEnemies.Count == 0)
				{
					UpdateGameState(GameState.Gameplay);
				}
			}
		}

		public void UpdateGameState(GameState newGameState)
		{
			if (newGameState == CurrentGameState)
				return;

			_onCombatStateEvent.RaiseEvent(newGameState == GameState.Combat);

			_previousGameState = _currentGameState;
			_currentGameState = newGameState;
		}

		public void ResetToPreviousGameState()
		{
			if (_previousGameState == _currentGameState)
				return;
			if (_previousGameState == GameState.Combat)
				_onCombatStateEvent.RaiseEvent(false);
			else if (_currentGameState == GameState.Combat) 
				_onCombatStateEvent.RaiseEvent(true);

			(_previousGameState, _currentGameState) = (_currentGameState, _previousGameState);
		}
	}
}