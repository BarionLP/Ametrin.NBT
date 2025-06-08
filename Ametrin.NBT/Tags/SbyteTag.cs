namespace Ametrin.NBT.Tags;

public sealed class SbyteTag(string name, sbyte value) : Tag(name)
{
    public sbyte Value { get; set; } = value;
    public override string ToString() => Value.ToString();
}
