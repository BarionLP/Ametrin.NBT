namespace Ametrin.NBT.Tags;

public sealed class ShortTag(short value) : Tag
{
    public short Value { get; } = value;

    public override string ToString() => Value.ToString();
}
