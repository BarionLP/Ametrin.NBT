namespace Ametrin.NBT.Tags;

public sealed class StringTag(string name, string value) : Tag(name, TagType.String)
{
    public string Value { get; set; } = value;

    public override string ToString() => Value;
}
