/* Copyright (C) Olivier Nizet https://github.com/onizet/html2openxml - All Rights Reserved
 * 
 * This source is subject to the Microsoft Permissive License.
 * Please see the License.txt file for more information.
 * All other rights reserved.
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 */
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace HtmlToOpenXml.Expressions;

/// <summary>
/// Leaf expression which process a simple text content.
/// </summary>
sealed class TextExpression(INode node) : HtmlDomExpression
{
    private readonly INode node = node;

    /// <inheritdoc/>
    public override IEnumerable<OpenXmlCompositeElement> Interpret (ParsingContext context)
    {
        string text = node.TextContent.Normalize();
        if (text.Trim().Length == 0) return [];

        if (!context.PreverseLinebreaks)
            text = text.StripLineBreaks();

        Run run = new(
            new Text(text) { Space = SpaceProcessingModeValues.Preserve }
        );

        context.CascadeStyles(run);
        return [run];
    }
}