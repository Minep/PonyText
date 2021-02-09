// File: GlobalConfiguration.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.IO;
using PonyText.Common.Misc;
using PonyText.Common.Parser.EventListener;

namespace PonyText.Common
{
    //FIXME use dependency injection later...
    public class GlobalConfiguration
    {

        private static volatile GlobalConfiguration GlobalConfigurationInstance = null;
        private static readonly object locker = new object();

        public static GlobalConfiguration Instance
        {
            get
            {
                if (GlobalConfigurationInstance == null)
                {
                    lock (locker)
                    {
                        GlobalConfigurationInstance = GlobalConfigurationInstance ?? new GlobalConfiguration();
                    }
                }
                return GlobalConfigurationInstance;
            }
        }

        public IErrorListener ParserErrorListener { get; }
        private StreamWriter LoggerStream { get; }
        public TextLogger Logger { get; }

        private GlobalConfiguration()
        {
            LoggerStream = new StreamWriter(Console.OpenStandardOutput());
            Logger = new TextLogger(LoggerStream);
            LoggerStream.AutoFlush = true;
            Console.SetOut(LoggerStream);

            ParserErrorListener = new SimpleErrorListener(LoggerStream);
        }
    }
}
