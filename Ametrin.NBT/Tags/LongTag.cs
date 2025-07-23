namespace Ametrin.NBT.Tags;

public sealed class LongTag(string name, long value) : Tag(name, TagType.Long)
{
    public long Value { get; set; } = value;
    public override string ToString() => Value.ToString();
}
