public interface IColoredHex
{
    void SetToBasicColor();
    void SetToHighlightColor();
    void SetToPathColor();
    void SelectHex();
    void DeselectHex();
    bool IsHighlighted { get; }
}
