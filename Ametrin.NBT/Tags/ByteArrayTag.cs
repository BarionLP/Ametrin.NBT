namespace Ametrin.NBT.Tags;

public sealed class ByteArrayTag(byte[] value) : Tag
{
    public byte[] Value { get; } = value;
    public override string ToString() => $"[{string.Join(", ", Value)}]";
}
