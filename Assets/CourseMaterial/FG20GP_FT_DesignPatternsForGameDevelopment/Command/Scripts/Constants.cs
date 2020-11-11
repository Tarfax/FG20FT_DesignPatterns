using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.CommandPattern
{
    public static class Constants
    {
        public const float margin = 0.5f;
        public const float size = 1f;

        const float moveAmp = size + margin;
        public static Vector3 moveUp = Vector3.zero.With(y: moveAmp);
        public static Vector3 moveDown = Vector3.zero.With(y: -moveAmp);

        public static Vector3 moveRight = Vector3.zero.With(x: moveAmp);
        public static Vector3 moveLeft = Vector3.zero.With(x: -moveAmp);
    }
}