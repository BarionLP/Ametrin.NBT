namespace Ametrin.NBT.Tags;

public sealed class ByteArrayTag(string name, byte[] value) : Tag(name, TagType.ByteArray)
{
    public byte[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
