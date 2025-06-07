namespace Ametrin.NBT.Tags;

public sealed class IntArrayTag(int[] value) : Tag
{
    public int[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
