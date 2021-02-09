// File: AbstractTextElement.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using PonyText.Common.Context;
using PonyText.Common.Renderer;

namespace PonyText.Common.Text
{
    public abstract class AbstractTextElement
    {
        public PropertyConfiguration CustomProperty { get; }
        public TextElementType TextElementType
        {
            get;
        }

        public AbstractTextElement(TextElementType textElementType)
        {
            TextElementType = textElementType;
            CustomProperty = new PropertyConfiguration();
        }

        public abstract void AddTextElement(AbstractTextElement textElement);
        public abstract void Render(AbstractRendererBase rendererBase, PonyTextContext textContext);

        public override string ToString() {
            return $"\"TextElementType\":\"{TextElementType}\",\"CustomProperty\":{CustomProperty.ToString()}";
        }
    }
}
