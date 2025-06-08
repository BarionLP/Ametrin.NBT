namespace Ametrin.NBT.Tags;

public sealed class ListTag(string name, TagType itemType, List<Tag> value) : Tag(name)
{
    public List<Tag> Value { get; } = value;
    public TagType ItemType { get; } = itemType;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
