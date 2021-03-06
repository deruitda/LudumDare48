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
        public GameObject leavesPreFab { get; set; }

        public float seedXPos { get; set; }
        public float seedYPos { get; set; }

        public double chanceOfCreatingABranch { get; set; }
        public double chanceOfBranchGrowing { get; set; }

        public double minSizeOfTreeBeforeForking { get; set; }
        public double minSizeOfBranchBeforeFork { get; set; }
        public double chanceOfCreatingAFork { get; set; }
        public double percentageOfTreeHasLeaves { get; set; }
        public int numberOfGrowthsPerThickness { get; set; }
        public int maxAmountOfForks { get; set; }
        public int thicknessOfLeaves { get; set; }
        public int startingWater { get; set; }
        public TreeConfig()
        {

        }

    }
}
