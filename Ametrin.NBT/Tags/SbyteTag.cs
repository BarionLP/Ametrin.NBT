namespace Ametrin.NBT.Tags;

public sealed class SbyteTag(sbyte value) : Tag
{
    public sbyte Value { get; } = value;
    public override string ToString() => Value.ToString();
}
