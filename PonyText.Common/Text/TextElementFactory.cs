// File: TextElementFactory.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Text.Impl;

namespace PonyText.Common.Text {
    public class TextElementFactory {
        public static AbstractTextElement CreateTextElement(TextElementType textElementType, string value = "") {
            switch (textElementType) {
                case TextElementType.TextUnit:
                    return new TextUnit(value);
                case TextElementType.TextElementCollection:
                    return new TextElementCollection();
                case TextElementType.Paragraph:
                    if (value.Length == 0) {
                        return new Paragraph();
                    }
                    else {
                        var paragraph = new Paragraph();
                        paragraph.AddTextElement(new TextUnit(value));
                        return paragraph;
                    }
                default:
                    //here can not be executed.
                    return null;
            }
        }
    }
}
