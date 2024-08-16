using System.Collections.Generic;

public class Regulation
{
    public string Title { get; private set; }
    public List<RegulationText> Texts { get; private set; }

    public Regulation(string title, List<RegulationText> texts)
    {
        Title = title;
        Texts = texts;
    }
}
