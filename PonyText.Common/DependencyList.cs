// File: DependencyList.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Exceptions;

namespace PonyText.Runtime.Common
{
    public class DependencyList
    {
        HashSet<string> dependencies;

        public DependencyList()
        {
            dependencies = new HashSet<string>();
        }

        public void CheckRecurrentDependency(string includePath)
        {
            if (dependencies.Contains(includePath))
            {
                throw new PonyTextException(ProcessingStage.PreProcessing,
                    $"A recurrent inclusion is detected. Trying to include '{includePath}' twice or cyclic dependency occured.");
            }
        }

        public void AddDependency(string includePath)
        {
            CheckRecurrentDependency(includePath);
            dependencies.Add(includePath);
        }
    }
}
