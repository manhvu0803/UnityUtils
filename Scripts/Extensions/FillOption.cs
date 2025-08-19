namespace Vun.UnityUtils
{
    /// <summary>
    /// Represent different scopes for finding component with <see cref="AutoFillAttribute"/> or <see cref="AutoFillUtils"/>
    /// </summary>
    public enum FillOption
    {
        /// <summary>
        /// From the host <see cref="UnityEngine.GameObject"/>
        /// Equivalent to <see cref="UnityEngine.Component.GetComponent{T}()"/>
        /// </summary>
        FromGameObject,

        /// <summary>
        /// From children <see cref="UnityEngine.GameObject"/>
        /// Equivalent to <see cref="UnityEngine.Component.GetComponentInChildren{T}()"/>
        /// </summary>
        FromChildren,

        /// <summary>
        /// From parents <see cref="UnityEngine.GameObject"/>.
        /// Equivalent to <see cref="UnityEngine.Component.GetComponentInParent{T}()"/>
        /// </summary>
        FromParent,

        /// <summary>
        /// From the whole hierarchy containing the host <see cref="UnityEngine.GameObject"/>.
        /// Equivalent to <see cref="AutoFillUtils.GetComponentInHierarchy"/>
        /// </summary>
        FromHierarchy,

        /// <summary>
        /// From every object in the same scene.
        /// Equivalent to <see cref="UnityEngine.Object.FindAnyObjectByType{T}()"/>
        /// </summary>
        FromScene
    }
}