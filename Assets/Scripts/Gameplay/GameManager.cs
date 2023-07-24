using System;
using BaseClasses.Enums;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private GameStateSO _gameState;

		private void Start()
		{
			StartGame();
		}

		void StartGame()
		{
			_gameState.UpdateGameState(GameState.Gameplay);
		}
	}
}