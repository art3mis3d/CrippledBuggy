using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
	[Serializable]
	public class DropGroup
	{
		[SerializeField] public List<DropItem> Drops { private get; set; }
		[SerializeField] public float DropRate { private get; set; }
	}
}