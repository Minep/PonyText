// File: StructureType.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;

namespace PonyText.Common.Structure
{
    [Flags]
    public enum StructureType
    {
        PTextStruct,
        DirectiveStruct,
        DirectiveBlockStruct,
        ParagraphStruct,
        LiteralStruct,
        MarcoStruct,
        NumberStruct,
        MapStruct,
        Any = PTextStruct | DirectiveStruct | ParagraphStruct | MapStruct | NumberStruct | MapStruct | ParagraphStruct
    }
}
