using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Avance2
{
    class PlatformSpawner
    {
        public Vector3 lastPosition;
        public Vector3 newPos;

        public void GeneratePosition()
        {
            newPos = lastPosition;

            int rand = UnityEngine.Random.Range(0, 2);

            if (rand > 0)
            {
                newPos.x += 2f;
            }
            else
            {
                newPos.z += 2f;
            }
        }
    }
}
