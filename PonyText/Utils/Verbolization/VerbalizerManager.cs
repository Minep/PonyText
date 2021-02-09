// File: VerbalizerManager.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Utils.Verbolization {
    public class VerbalizerManager {

        private static volatile VerbalizerManager VerbalizerManagerInstance = null;
        private static readonly object locker = new object();

        public static VerbalizerManager Instance {
            get {
                if (VerbalizerManagerInstance == null) {
                    lock (locker) {
                        VerbalizerManagerInstance = VerbalizerManagerInstance ?? new VerbalizerManager();
                    }
                }
                return VerbalizerManagerInstance;
            }
        }

        Dictionary<VerbalizationOptions, INumberVerbalizer> verbalizers = new Dictionary<VerbalizationOptions, INumberVerbalizer>() {
            {VerbalizationOptions.Chinese, new ChineseVerbalizer() }
        };

        private VerbalizerManager() {
            //TODO Constructor code here
        }


        public string Verbalize(string number, VerbalizationOptions language, VerbalizationMode mode) {
            string verbalized = string.Empty;
            if (verbalizers.ContainsKey(language)) {
                switch (mode) {
                    case VerbalizationMode.ReadOut:
                        verbalized = verbalizers[language].verbalizeFloat(number);
                        break;
                    case VerbalizationMode.SpellOut:
                        verbalized = verbalizers[language].verbalizeOneByOne(number);
                        break;
                }
            }
            else {
                throw new InvalidOperationException($"Verbalizer \"{language}\" undefined");
            }
            return verbalized;
        }
    }
}
