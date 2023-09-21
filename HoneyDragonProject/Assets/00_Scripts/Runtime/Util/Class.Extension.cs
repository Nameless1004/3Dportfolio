using UnityEngine;

static class Extensions
{
	public static void DestroyChild(this Transform transform)
	{
		int childCount = transform.childCount;

		for(int i = 0; i < childCount; i++)
		{
			MonoBehaviour.Destroy(transform.GetChild(i).gameObject);
		}
	}

    public static bool IsSameLayer(this GameObject go, string layerName)
	{
		int a = 1 << go.layer ;
		
		return (a & LayerMask.GetMask(layerName)) != 0;
	}
}
