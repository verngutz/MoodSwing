namespace MoodSwingGUI
{
    /// <summary>
    /// represents the different Alignments that a MoodSwingGUI element may have when arranged in a border layout, use MANUAL to disable the border layout
    /// <seealso cref="MoodSwingGUI.MSPanel"/>
    /// </summary>
    public enum Alignment
    {
        TOP_LEFT,
        TOP_CENTER,
        TOP_RIGHT,
        MIDDLE_LEFT,
        MIDDLE_CENTER,
        MIDDLE_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_CENTER,
        BOTTOM_RIGHT,
        MANUAL
    }

    /// <summary>
    /// the three discrete states that any button or button-like MoodSwingGUI element may have
    /// </summary>
    public enum MSButtonState { UNCLICKED, CLICKED, HOVERED }
}
