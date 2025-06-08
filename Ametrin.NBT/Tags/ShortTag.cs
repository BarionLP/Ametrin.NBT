namespace Ametrin.NBT.Tags;

public sealed class ShortTag(string name, short value) : Tag(name)
{
    public short Value { get; set; } = value;

    public override string ToString() => Value.ToString();
}
