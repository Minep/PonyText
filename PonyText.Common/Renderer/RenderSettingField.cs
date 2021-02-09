// File: DefaultRenderSetting.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyText.Common.Renderer
{
    public class RenderSettingField
    {
        public const string PAGE_LAYOUT_CONFIG = "document.layout";
        public const string PAGE_INFO_CONFIG = "document.info";
        public const string PAGE_STYLE_CONFIG = "document.style";

        public const string PAGE_MARGIN = "margin";
        public const string PAGE_PADDING = "padding";
        public const string PAGE_ORIENTATION = "orientation";
        public const string PAGE_PageSize = "pageDim";
        public const string PAGE_PageFormat = "pageSize";

        public const string PAGE_ORIENTATION_LANDSCAPE = "landscape";
        public const string PAGE_ORIENTATION_PROTRIAT = "portrait";

        public const string PARAGRAPH_LINE_INDENT_SIZE = "firstLineIndent";

        public const string HEADING_LEVEL = "headingLevel";
        public const string HORIZONTAL_ALIGN = "hAlignTo";
        public const string VERTICAL_ALIGN = "vAlignTo";

        public const string TEXT_FONTFAMILY = "fontFamily";
        public const string TEXT_FONTSIZE = "fontSize";
        public const string TEXT_STYLE = "style";
        public const string TEXT_COLOR_ARGB = "fontColor";

        public const string ALIGN_H_LEFT = "left";
        public const string ALIGN_H_RIGHT = "right";
        public const string ALIGN_V_TOP = "top";
        public const string ALIGN_V_JUSTIFY = "justify";
        public const string ALIGN_V_BOTTOM = "bottom";
        public const string ALIGN_VH_MID = "mid";

        public const string TEXT_STYLE_REGULAR = "regular";
        public const string TEXT_STYLE_BOLD = "bold";
        public const string TEXT_STYLE_ITALIC = "italic";
        public const string TEXT_STYLE_UNDERLINE = "underline";
        public const string TEXT_STYLE_DELETELINE = "strike";

        public const string STYLE_OVERRIDE_HEADING = "style.Heading#";
        public const string STYLE_OVERRIDE_PARAGRAPH = "style.Paragraph";
        public const string STYLE_OVERRIDE_FOOTER = "style.Footer";
        public const string STYLE_OVERRIDE_HEADER = "style.Header";

        public const string DOCUMENT_INFO = "document.info";

    }
}
