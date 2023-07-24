namespace BaseClasses.Enums
{
	public enum GameState
	{
		Gameplay, // regular state: player moves, attacks can perform actions
		Pause, // pause menu is opened, the whole game world is frozen
		Inventory, // when UI is open
		Dialogue,
		Cutscene,
		LocationTransition, // When the character steps into LocationExit trigger, fade to black begins and control is removed from the player.
		Combat, // Enemy is nearby and alert, player can't open Inventory or initiate dialogues, but can pause the game.
	}
}