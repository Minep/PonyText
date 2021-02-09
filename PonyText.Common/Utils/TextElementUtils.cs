// File: TextElementUtils.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Text;
using PonyText.Common.Text.Impl;

namespace PonyText.Common.Utils {
    public static class TextElementUtils {
        public static List<AbstractTextElement> UnfoldToAtomic(this AbstractTextElement textElement) {
            var textElements = new List<AbstractTextElement>();
            switch (textElement.TextElementType) {
                case TextElementType.TextUnit:
                    textElements.Add(textElement);
                    break;
                case TextElementType.TextElementCollection:
                    var collection = (TextElementCollection)textElement;
                    foreach (var item in collection) {
                        var atomics = item.UnfoldToAtomic();
                        foreach (var atomic in atomics) {
                            atomic.CustomProperty.Merge(collection.CustomProperty.ToDictionary());
                            textElements.Add(atomic);
                        }
                    }
                    break;
                case TextElementType.Paragraph:
                    var paragraph = (Paragraph)textElement;
                    foreach (var item in paragraph) {
                        item.CustomProperty.Merge(paragraph.CustomProperty.ToDictionary());
                        textElements.Add(item);
                    }
                    break;
                default:
                    break;
            }
            return textElements;
        }
    }
}
