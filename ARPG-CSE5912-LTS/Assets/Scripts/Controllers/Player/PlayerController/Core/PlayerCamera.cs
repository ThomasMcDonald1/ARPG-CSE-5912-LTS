using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Core
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        void Update()
        {
            transform.position = target.position;
        }
    }
}
