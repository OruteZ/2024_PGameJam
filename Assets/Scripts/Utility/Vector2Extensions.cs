using UnityEngine;

namespace Utility
{
    public static class Vector2Extensions
    {
        public static Vector2 Random()
        {
            return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        }
    }
}
