namespace Ametrin.NBT.Tags;

public abstract class Tag(string name, TagType tagType)
{
    public string Name { get; } = name;
    public TagType TagType { get; } = tagType;
}
