namespace Ametrin.NBT.Tags;

public sealed class CompoundTag(string name, Dictionary<string, Tag> value) : Tag(name, TagType.Compound)
{
    public Dictionary<string, Tag> Value { get; } = value;
    public IEnumerable<Tag> Values => Value.Values;
    public override string ToString()
    {
        return $"{{{string.Join(", ", Value.Select(pair => $"{pair.Key}:{pair.Value}"))}}}";
    }
}
