using System.Collections.Generic;
using System.Linq;
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

    public static T GetRandomValue<T>(this List<T> list)
    {
        int maxValue = list.Count;
        int minValue = 0;
        int randomIndex = UnityEngine.Random.Range(minValue, maxValue);
        return list[randomIndex];
    }
}
