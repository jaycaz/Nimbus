using UnityEngine;
using System.Collections;

namespace RTS
{
    public static class ResourceManager
    {
        public static int ScrollWidth { get { return 15; } }
        public static float ScrollSpeed { get { return 200; } }
        public static float ZoomSpeed { get { return 1000; } }
        public static float MinCameraHeight { get { return 10; } }
        public static float MaxCameraHeight { get { return 2000; } }
        public static float SnapToPlayerDist { get { return 30; } }
        public static float MouseSensitivity { get { return 1000; } }
        public static float CloudAutoTurnSpeed { get { return 1; } }
        public static float CloudManualTurnSpeed { get { return 200 * CloudAutoTurnSpeed; } }
        public static float CloudMoveSpeed { get { return 5.0f; } }
        public static float CloudPointTolerance { get { return 20f; } }
    }
}