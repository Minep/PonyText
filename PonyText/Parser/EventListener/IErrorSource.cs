// File: IErrorSource.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Parser.EventListener;

namespace PonyText.Parser.EventListener
{
    public interface IErrorSource
    {
        IErrorListener errorListener
        {
            get; set;
        }
    }
}
