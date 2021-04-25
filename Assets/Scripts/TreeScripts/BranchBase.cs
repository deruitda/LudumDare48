using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.TreeScripts
{
    class BranchBase
    {
        public float posX { get; set;  }
        public float posY { get; set;  }
        public BranchBase(float pPosX, float pPosY)
        {
            posX = pPosX;
            posY = pPosY;
        }

        public void addPosX()
        {
            posX++;
        }

        public void addPosY()
        {
            posY++;
        }
        public void subPosX()
        {
            posX--;
        }

        public void subPosY()
        {
            posY--;
        }
    }
}
