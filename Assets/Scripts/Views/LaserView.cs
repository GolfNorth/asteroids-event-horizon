using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Views
{
    /// <summary>
    /// Визуализатор лазера
    /// </summary>
    public sealed class LaserView : View
    {
        [SerializeField, Tooltip("Рендерер линии")]
        private LineRenderer lineRenderer;


        private void Awake()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.loop = false;
        }

        private void LateUpdate()
        {
            Vector3 position = transform.position;
            Vector3 direction = transform.rotation * Vector3.right;

            Ray ray = new Ray(position, direction);

            float maxDistance = 0;
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Context.Instance.Camera);

            for (var i = 0; i < 4; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    maxDistance = Mathf.Max(maxDistance, distance);
                }
            }

            if (maxDistance == 0)
                return;

            Vector3 hitPoint = ray.GetPoint(maxDistance);

            lineRenderer.SetPosition(0, position);
            lineRenderer.SetPosition(1, hitPoint);
        }
    }
}