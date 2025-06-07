namespace Ametrin.NBT.Tags;

public sealed class StringTag(string value) : Tag
{
    public string Value { get; } = value;

    public override string ToString() => Value;
}
