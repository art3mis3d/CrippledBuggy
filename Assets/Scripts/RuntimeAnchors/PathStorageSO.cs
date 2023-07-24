using Unity.Collections;
using UnityEngine;
/// <summary>
/// This one of a  kind SO stores, during gameplay, the path that was used last (i.e. the one that was taken to get to thr current scene).
/// </summary>
[CreateAssetMenu(fileName = "PathStorage", menuName = "Gameplay/Path Storage", order = 0)]
public class PathStorageSO : DescriptionBaseSO
{
	[Space]
	[ReadOnly]
	public PathSO lastPathTaken;
}