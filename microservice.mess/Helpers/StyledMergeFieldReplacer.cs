using Aspose.Words;
using Aspose.Words.Replacing;
using Aspose.Words.Fonts;
using System.Drawing;


namespace microservice.mess.Documents
{
    public class StyledMergeFieldReplacer : IReplacingCallback
{
    private readonly string _replacementText;
    private readonly string _fontName;
    private readonly double _fontSize;
    private readonly Color _fontColor;
    private readonly bool _isBold;

    public StyledMergeFieldReplacer(string replacementText, string fontName = "Arial", double fontSize = 12, Color? fontColor = null, bool isBold = false)
    {
        _replacementText = replacementText;
        _fontName = fontName;
        _fontSize = fontSize;
        _fontColor = fontColor ?? Color.Black;
        _isBold = isBold;
    }

    public ReplaceAction Replacing(ReplacingArgs args)
    {
        DocumentBuilder builder = new DocumentBuilder((Document)args.MatchNode.Document);
        builder.MoveTo(args.MatchNode);

        builder.Font.Name = _fontName;
        builder.Font.Size = _fontSize;
        builder.Font.Color = _fontColor;
        builder.Font.Bold = _isBold;

        builder.Write(_replacementText);

        args.Replacement = ""; // clear placeholder
        return ReplaceAction.Replace;
    }
}

}