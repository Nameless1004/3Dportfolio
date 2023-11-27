using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class Extensions
{
	public static bool IsBehind(this Transform transform, Vector3 targetPosition)
	{
		var targetVec = (targetPosition - transform.position).normalized;
		targetVec.y = 0f;
		targetPosition.y = 0f;

        var dot = Vector3.Dot(transform.forward, targetVec);

		return dot >= 0f ? false : true;
	}

    public static bool IsBehind(this Transform transform, Vector3 targetPosition, float fov)
    {
        var targetVec = (targetPosition - transform.position).normalized;
        targetVec.y = 0f;
        targetPosition.y = 0f;
		float radian = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);
        var dot = Vector3.Dot(transform.forward, targetVec);

        return dot >= radian ? false : true;
    }

    public static void DestroyChildren(this Transform transform)
	{
		int childCount = transform.childCount;

		for (int i = 0; i < childCount; i++)
		{
			DestroyChildren(transform.GetChild(i));
            MonoBehaviour.Destroy(transform.GetChild(i).gameObject);
		}
	}

	public static bool IsSameLayer(this GameObject go, string layerName)
	{
		int a = 1 << go.layer;

		return (a & LayerMask.GetMask(layerName)) != 0;
	}

	// y값은 제외시킨 거리
	public static float GetDistance(this Vector3 vec, Vector3 target)
	{
		Vector3 myVec = new Vector3(vec.x, 0f, vec.z);
		myVec.y = 0f;
		target.y = 0f;
		return Vector3.Distance(myVec, target);
	}

	public static void SetRandomDirectionXZ(this ref Vector3 vec)
	{
        float randomX = Random.Range(0f, 1f);
        float randomZ = Random.Range(0f, 1f);
		vec.x = randomX;
		vec.y = 0f;
		vec.z = randomZ;
	}

    public static T GetRandomValue<T>(this List<T> list)
    {
        int maxValue = list.Count;
        int minValue = 0;
        int randomIndex = UnityEngine.Random.Range(minValue, maxValue);
        return list[randomIndex];
    }
}
