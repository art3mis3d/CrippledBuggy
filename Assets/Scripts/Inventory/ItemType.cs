using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Inventory
{
	[Serializable]
	public enum ItemType
	{
		Item, // Generic Item
		Health, // Health Regenerative Item
		Collectible, // Random Collectible
		QuestItem, // Used for tasks
		Weapon, // Weapon Item
	}

	public enum ItemAction
	{
		Consume,
		Equip,
		DoNothing,
	}

	[CreateAssetMenu(fileName = "ItemType", menuName = "Inventory/Item Type")]
	public class ItemTypeSO : ScriptableObject
	{
		[SerializeField] public LocalizedString ActionName { get; private set; }

		[Tooltip("Item's Background color in the UI")]
		[SerializeField] public Color typeColor { get; private set; }

		[SerializeField] public ItemType type { get; private set; }

		[SerializeField] public ItemAction actionType { get; private set; }
	}
}