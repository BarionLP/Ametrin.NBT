namespace Ametrin.NBT.Tags;

public sealed class ListTag(string name, TagType itemType, List<Tag> value) : Tag(name, TagType.List)
{
    public IReadOnlyList<Tag> Value => value;
    public TagType ItemType { get; } = itemType;
    public void Add(Tag tag)
    {
        if (tag.TagType != ItemType)
        {
            throw new ArgumentException("Invalid TagType", nameof(tag));
        }

        value.Add(tag);
    }

    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
