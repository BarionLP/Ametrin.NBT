namespace Ametrin.NBT.Tags;

public sealed class LongTag(long value) : Tag
{
    public long Value { get; } = value;
    public override string ToString() => Value.ToString();
}
