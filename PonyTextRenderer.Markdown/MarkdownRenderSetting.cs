// File: MarkdownRenderSetting.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System;
using System.Collections.Generic;
using System.Text;

namespace PonyTextRenderer.Markdown
{
    public class MarkdownRenderSetting
    {
        public const string MD_REGION = "md.region";
        public const string MD_REGION_INLINE = "md.ilregion";
        public const string MD_REGION_CODE = "code";
        public const string MD_REGION_QUOTE = "quote";
        public const string MD_REGION_LIST = "lst";
        public const string MD_REGION_LIST_ORDER = "ordlst";
        public const string MD_REGION_LIST_CB = "cblst";
    }
}
