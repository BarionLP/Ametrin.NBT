namespace Ametrin.NBT.Tags;

public sealed class CompoundTag(Dictionary<string, Tag> value) : Tag
{
    public Dictionary<string, Tag> Value { get; } = value;
    public override string ToString()
    {
        return $"{{{string.Join(", ", Value.Select(pair => $"{pair.Key}:{pair.Value}"))}}}";
    }
}
