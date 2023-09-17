using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace Avance2
{
    class CarController : MonoBehaviour
    {
        public bool firstInput = true;
        public bool movingLeft = false;

        public void CheckInput()
        {
            if (firstInput)
            {
                firstInput = false;
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                ChangeDirection();
            }
        }

        public void ChangeDirection()
        {
            if (movingLeft)
            {
                movingLeft = false;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                movingLeft = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
