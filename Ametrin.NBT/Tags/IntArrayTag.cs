namespace Ametrin.NBT.Tags;

public sealed class IntArrayTag(string name, int[] value) : Tag(name)
{
    public int[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
