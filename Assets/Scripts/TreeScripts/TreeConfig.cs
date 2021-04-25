using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TreeScripts { 
    public class TreeConfig
    {
        public GameObject treePreFab { get; set; }
        public GameObject branchPreFab { get; set; }
        public float seedXPos { get; set; }
        public float seedYPos { get; set; }

        public TreeConfig()
        {

        }

    }
}
