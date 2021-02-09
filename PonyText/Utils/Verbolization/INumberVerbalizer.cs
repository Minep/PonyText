// File: IVerbalizer.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Utils.Verbolization {
    public interface INumberVerbalizer {
        string verbalize(int value);
        string verbalize(double value);
        string verbalizeOneByOne(int value);
        string verbalizeOneByOne(string value);
        string verbalizeFloat(string value);
        string verbalizeInteger(string value);
    }
}
