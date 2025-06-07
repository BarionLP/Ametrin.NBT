namespace Ametrin.NBT.Tags;

public sealed class LongArrayTag(long[] value) : Tag
{
    public long[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
