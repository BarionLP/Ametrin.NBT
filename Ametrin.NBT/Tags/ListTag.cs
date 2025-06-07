namespace Ametrin.NBT.Tags;

public sealed class ListTag(TagType itemType, List<Tag> value) : Tag
{
    public List<Tag> Value { get; } = value;
    public TagType ItemType { get; } = itemType;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
