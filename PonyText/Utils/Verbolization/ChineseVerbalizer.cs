// File: Verbalization.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PonyText.Utils.Verbolization {
    public class ChineseVerbalizer : INumberVerbalizer {
        private readonly string[] ChineseNum = new string[] {
            "零","一","二","三","四","五","六","七","八","九"
        };

        private readonly string[] ChineseHigherNum = new string[] {
            string.Empty, "十", "百", "千"
        };

        private readonly string[] ChineseHHNum = new string[] {
            "万", "亿", "万亿", "兆亿", "京", "垓"
        };

        public string verbalize(int value) {
            return verbalizeInteger(value.ToString());
        }

        public string verbalize(double value) {
            return verbalizeFloat(value.ToString());
        }

        public string verbalizeOneByOne(int value) {
            return verbalizeOneByOne(value.ToString());
        }

        public string verbalizeOneByOne(string value) {
            var sb = new StringBuilder();
            foreach (var item in value) {
                if ('0' <= item && item <= '9') {
                    sb.Append(ChineseNum[item - '0']);
                }
                else {
                    throw new ArgumentException("Invalid numeric string");
                }
            }
            return sb.ToString();
        }

        public string verbalizeFloat(string value) {
            var fracts = value.Split('.');
            var verb = verbalizeInteger(fracts[0]);
            if (fracts.Length > 1) {
                var sb = new StringBuilder();
                for (var i = 0; i < fracts[1].Length; i++) {
                    sb.Append(ChineseNum[fracts[1][i] - '0']);
                }
                return $"{verb}点{sb}";
            }
            else {
                return verb;
            }
        }

        public string verbalizeInteger(string value) {
            var sb = new List<string>();
            bool isneg;
            var isPreviousZero = false;
            var HNUM_LEN = ChineseHigherNum.Length;
            var currentHigherNumDigit = 0;
            if (isneg = value[0] == '-') {
                value = value[1..];
            }
            for (var i = value.Length - 1; i >= 0; i--) {
                var c = value[i];
                var digit = value.Length - i - currentHigherNumDigit * HNUM_LEN;

                if (c == '0') {
                    if (!isPreviousZero && digit > 0) {
                        sb.Add(ChineseNum[0]);
                    }
                    isPreviousZero = true;
                    continue;
                }
                isPreviousZero = false;

                if (digit > HNUM_LEN) {
                    currentHigherNumDigit += 1;
                    digit -= HNUM_LEN;
                    if (currentHigherNumDigit > ChineseHHNum.Length) {
                        throw new ArgumentException($"Given number:{value} is too large to verbalize");
                    }
                    sb.Add(ChineseHHNum[currentHigherNumDigit - 1]);
                }
                sb.Add(ChineseHigherNum[digit - 1]);
                if (digit != 2 || i != 0 || c != '1') {
                    sb.Add(ChineseNum[c - '0']);
                }
            }

            if (sb.Count == 0) {
                sb.Add(ChineseNum[0]);
            }

            sb.Reverse();
            var strB = new StringBuilder();
            foreach (var item in sb) {
                strB.Append(item);
            }
            return isneg ? $"负{strB}" : strB.ToString();
        }
    }
}
