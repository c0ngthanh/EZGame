// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AttackRange : MonoBehaviour
// {
//     [SerializeField] private Unit unit;

//     private void OnTriggerStay(Collider other)
//     {
//         // Ensure the other object is on the "Unit" layer
//         if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
//         {
//             if (other.TryGetComponent(out Unit checkUnit))
//             {
//                 if (checkUnit.faction != unit.faction)
//                 {
//                     if (unit.IsUnitInRange(checkUnit))
//                     {
//                         return;
//                     }
//                     unit.AddUnitInRange(checkUnit);
//                 }
//             }
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         // Ensure the other object is on the "Unit" layer
//         if (other.gameObject.layer == LayerMask.NameToLayer("Unit"))
//         {
//             if (other.TryGetComponent(out Unit checkUnit))
//             {
//                 if (checkUnit.faction != unit.faction)
//                 {
//                     if (unit.IsUnitInRange(checkUnit))
//                     {
//                         unit.RemoveUnitInRange(checkUnit);
//                     }
//                 }
//             }
//         }
//     }
// }
