using UnityEngine;

namespace Vun.UnityUtils
{
    public static class VectorUtils
    {
        #region Vector4

        public static Vector4 WithX(this Vector4 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector4 WithY(this Vector4 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector4 WithZ(this Vector4 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector4 WithW(this Vector4 vector, float w)
        {
            vector.w = w;
            return vector;
        }

        public static Vector4 WithXY(this Vector4 vector, float x, float y)
        {
            vector.x = x;
            vector.y = y;
            return vector;
        }

        public static Vector4 WithXZ(this Vector4 vector, float x, float z)
        {
            vector.x = x;
            vector.z = z;
            return vector;
        }

        public static Vector4 WithXW(this Vector4 vector, float x, float w)
        {
            vector.x = x;
            vector.w = w;
            return vector;
        }

        public static Vector4 WithYZ(this Vector4 vector, float y, float z)
        {
            vector.y = y;
            vector.z = z;
            return vector;
        }

        public static Vector4 WithYW(this Vector4 vector, float y, float w)
        {
            vector.y = y;
            vector.w = w;
            return vector;
        }

        public static Vector4 WithZW(this Vector4 vector, float z, float w)
        {
            vector.z = z;
            vector.w = w;
            return vector;
        }

        public static Vector4 WithXYZ(this Vector4 vector, float x, float y, float z)
        {
            vector.x = x;
            vector.y = y;
            vector.z = z;
            return vector;
        }

        public static Vector4 WithXZW(this Vector4 vector, float x, float z, float w)
        {
            vector.x = x;
            vector.z = z;
            vector.w = w;
            return vector;
        }

        public static Vector4 WithYZW(this Vector4 vector, float y, float z, float w)
        {
            vector.y = y;
            vector.z = z;
            vector.w = w;
            return vector;
        }

        public static Vector4 With(this Vector4 vector, float? x = null, float? y = null, float? z = null, float? w = null)
        {
            vector.x = x ?? vector.x;
            vector.x = y ?? vector.y;
            vector.x = z ?? vector.z;
            vector.w = w ?? vector.w;
            return vector;
        }

        #endregion

        #region Vector3

        public static Vector3 WithX(this Vector3 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            vector.x = x ?? vector.x;
            vector.x = y ?? vector.y;
            vector.x = z ?? vector.z;
            return vector;
        }

        #endregion

        #region Vector3Int

        public static Vector3Int WithX(this Vector3Int vector, int x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector3Int WithY(this Vector3Int vector, int y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector3Int WithZ(this Vector3Int  vector, int z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector3Int With(this Vector3Int vector, int? x = null, int? y = null, int? z = null)
        {
            vector.x = x ?? vector.x;
            vector.x = y ?? vector.y;
            vector.x = z ?? vector.z;
            return vector;
        }

        #endregion

        #region Vector2

        public static Vector2 WithX(this Vector2 vector, int x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector2 WithY(this Vector2 vector, int y)
        {
            vector.y = y;
            return vector;
        }

        #endregion

        #region Vector2Int

        public static Vector2Int WithX(this Vector2Int vector, int x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector2Int WithY(this Vector2Int vector, int y)
        {
            vector.y = y;
            return vector;
        }
        
        #endregion
    }
}