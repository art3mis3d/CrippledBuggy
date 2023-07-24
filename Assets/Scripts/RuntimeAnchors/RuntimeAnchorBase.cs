using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

public class RuntimeAnchorBase<T> : DescriptionBaseSO where T : UnityEngine.Object
{
	public UnityAction OnAnchorProvided;

	[Header("Debug")]
	[ReadOnly] public bool isSet; // Any script can check if the transform is null before using it, by just checking this bool
	
	[SerializeField]
	[ReadOnly]
	private T value;
	public T Value { get { return value; } }

	// ReSharper disable Unity.PerformanceAnalysis
	public void Provide(T value)
	{
		if(value == null)
		{
			Debug.LogError("A null value was provided to the " + name + " runtime anchor.");
			return;
		}

		this.value = value;
		isSet = true;

		OnAnchorProvided?.Invoke();
	}

	public void Unset()
	{
		value = null;
		isSet = false;
	}

	private void OnDisable()
	{
		Unset();
	}
}
