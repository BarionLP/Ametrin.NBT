namespace Ametrin.NBT.Tags;

public sealed class IntTag(int value) : Tag
{
    public int Value { get; } = value;
    public override string ToString() => Value.ToString();
}
