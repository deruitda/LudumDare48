using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.TreeScripts
{
    class BranchBase
    {
        public float posX { get; }
        public float posY { get; }
        public BranchBase(float pPosX, float pPosY)
        {
            posX = pPosX;
            posY = pPosY;
        }
    }
}
