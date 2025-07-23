namespace Ametrin.NBT.Tags;

public sealed class ShortTag(string name, short value) : Tag(name, TagType.Short)
{
    public short Value { get; set; } = value;

    public override string ToString() => Value.ToString();
}
