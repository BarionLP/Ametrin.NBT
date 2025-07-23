namespace Ametrin.NBT.Tags;

public sealed class IntArrayTag(string name, int[] value) : Tag(name, TagType.IntArray)
{
    public int[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
