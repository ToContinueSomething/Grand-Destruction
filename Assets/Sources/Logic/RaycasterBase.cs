using System;
using UnityEngine;

public class RaycasterBase : MonoBehaviour
{
   [SerializeField] private Transform _entity; 
   private Camera _camera;
   private Vector3 _point;

   private void Awake()
   {
      _camera = Camera.main;
   }

   public void Raycast(Vector3 point)
   {
      _point = point;
      var ray = _camera.ScreenPointToRay(point);
   }

   private void OnDrawGizmos()
   {
      if (_point != default)
      {
        
         var direction = _point - transform.position;
         Ray ray = new Ray(transform.position, direction);
         Gizmos.DrawSphere(_point,1f);
         
         Gizmos.color = Color.green;
         Gizmos.DrawLine(transform.position,transform.position + direction);

         if (Physics.Raycast(ray,out RaycastHit hitInfo, Mathf.Infinity))
         {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(hitInfo.point,1f);
         }

      }
   }
}
