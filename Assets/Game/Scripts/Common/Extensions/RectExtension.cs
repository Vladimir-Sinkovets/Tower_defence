using UnityEngine;

namespace Assets.Game.Scripts.Common.Extensions
{
    public static class RectExtension
    {
        public static Vector2 GetRandomPointInRect(this Rect spawnArea)
        {
            var x = Random.Range(spawnArea.xMin, spawnArea.xMax);
            var y = Random.Range(spawnArea.yMin, spawnArea.yMax);
            
            return new Vector2(x, y);
        }
    }
}