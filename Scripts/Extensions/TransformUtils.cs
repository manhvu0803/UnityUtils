using UnityEngine;

namespace Vun.UnityUtils
{
    public static class TransformUtils
    {
        #region Set position

        public static void SetPositionX(this Transform transform, float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            var position = transform.position;
            position.z = z;
            transform.position = position;
        }

        #endregion

        #region Set local position

        public static void SetLocalPositionX(this Transform transform, float x)
        {
            var position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            var position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            var position = transform.localPosition;
            position.z = z;
            transform.localPosition = position;
        }

        #endregion

        #region Set Euler angles

        public static void SetEulerX(this Transform transform, float x)
        {
            var eulerAngles = transform.eulerAngles;
            eulerAngles.x = x;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetEulerY(this Transform transform, float y)
        {
            var eulerAngles = transform.eulerAngles;
            eulerAngles.y = y;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetEulerZ(this Transform transform, float z)
        {
            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = z;
            transform.eulerAngles = eulerAngles;
        }

        #endregion

        #region Set local Euler angles

        public static void SetLocalEulerX(this Transform transform, float x)
        {
            var eulerAngles = transform.localEulerAngles;
            eulerAngles.x = x;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetLocalEulerY(this Transform transform, float y)
        {
            var eulerAngles = transform.localEulerAngles;
            eulerAngles.y = y;
            transform.eulerAngles = eulerAngles;
        }

        public static void SetLocalEulerZ(this Transform transform, float z)
        {
            var eulerAngles = transform.localEulerAngles;
            eulerAngles.z = z;
            transform.eulerAngles = eulerAngles;
        }

        #endregion

        #region Set local scale

        public static void SetScaleX(this Transform transform, float x)
        {
            var scale = transform.localScale;
            scale.x = x;
            transform.eulerAngles = scale;
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            var scale = transform.localScale;
            scale.y = y;
            transform.eulerAngles = scale;
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            var scale = transform.localScale;
            scale.z = z;
            transform.eulerAngles = scale;
        }
        
        #endregion
    
        public static (int childCount, int depth) GetHierarchyInfo(this Transform transform)
        {
            return transform.GetHierarchyInfo(0);
        }

        private static (int childCount, int depth) GetHierarchyInfo(this Transform transform, int level)
        {
            var totalChildCount = 0;
            var maxDepth = level;

            foreach (Transform child in transform)
            {
                var (childCount, depth) = child.GetHierarchyInfo(level + 1);
                totalChildCount += 1 + childCount;
                maxDepth = Mathf.Max(maxDepth, depth);
            }

            return (totalChildCount, maxDepth);
        }
    }
}