using UnityEngine;
using UnityEngine.Localization;

namespace Inventory
{
	[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
	public class ItemSO : SerializableScriptableObject
	{
		[Tooltip("The Name of the Item")] public LocalizedString Name { get; private set; }

		[Tooltip("A preview image for the item.")]
		public Sprite PreviewImage { get; private set; }

		[Tooltip("A description of the item.")]
		public LocalizedString Description { get; private set; }

		[Tooltip("Type of Inventory Item")] public ItemTypeSO ItemOfType { get; private set; }
	}
}