namespace Ametrin.NBT.Tags;

public sealed class LongArrayTag(string name, long[] value) : Tag(name)
{
    public long[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
