using UnityEngine;
using System.Collections;

namespace RTS
{
    public static class ResourceManager
    {
        public static int ScrollWidth { get { return 15; } }
        public static float ScrollSpeed { get { return 2; } }
        public static float ZoomSpeed { get { return 10; } }
        public static float RotateAmount { get { return 10; } }
        public static float RotateSpeed { get { return 100; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 2000; } }
        public static float SnapToPlayerDist { get { return 30; } }
        public static float MouseSensitivity { get { return 15; } }
        public static float CloudTurnSpeed { get { return 3; } }
        public static float CloudMoveSpeed { get { return 0.1f; } }
    }
}