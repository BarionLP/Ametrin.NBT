using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using Ametrin.NBT.Tags;

namespace Ametrin.NBT;

public sealed class NbtWriter(Stream stream, bool leaveOpen = true) : IDisposable
{
    private readonly BinaryWriter _writer = new(stream, Encoding.UTF8, leaveOpen: leaveOpen);

    public void WriteTag(Tag tag)
    {
        _writer.Write((byte)tag.TagType);
        WriteString(tag.Name);
        WriteTagInternal(tag);
    }
    private void WriteTagInternal(Tag root)
    {
        switch (root)
        {
            case SbyteTag tag:
                _writer.Write(tag.Value);
                break;
            case ShortTag tag:
                WriteInt16BigEndian(tag.Value);
                break;
            case IntTag tag:
                WriteInt32BigEndian(tag.Value);
                break;
            case LongTag tag:
                WriteInt64BigEndian(tag.Value);
                break;
            case FloatTag tag:
                WriteSingleBigEndian(tag.Value);
                break;
            case DoubleTag tag:
                WriteDoubleBigEndian(tag.Value);
                break;
            case ByteArrayTag tag:
                WriteInt32BigEndian(tag.Value.Length);
                _writer.Write(tag.Value);
                break;
            case StringTag tag:
                WriteString(tag.Value);
                break;
            case ListTag tag:
                WriteList(tag);
                break;
            case CompoundTag tag:
                WriteCompound(tag.Values);
                break;
            case IntArrayTag tag:
                WriteArray(tag.Value, WriteInt32BigEndian);
                break;
            case LongArrayTag tag:
                WriteArray(tag.Value, WriteInt64BigEndian);
                break;
            default:
                throw new NotSupportedException($"Unkown tag type {root}");
        }

    }

    private void WriteArray<T>(T[] values, Action<T> writer)
    {
        WriteInt32BigEndian(values.Length);
        foreach (var value in values)
        {
            writer(value);
        }
    }

    private void WriteCompound(IEnumerable<Tag> items)
    {
        foreach (var item in items)
        {
            WriteTag(item);
        }
        _writer.Write((byte)TagType.End);
    }

    private void WriteList(ListTag tag)
    {
        _writer.Write((byte)tag.ItemType);
        WriteInt32BigEndian(tag.Value.Count);
        foreach (var item in tag.Value)
        {
            WriteTagInternal(item);
        }
    }

    private void WriteString(string value)
    {
        WriteUInt16BigEndian((ushort)value.Length);
        Span<byte> buffer = stackalloc byte[Encoding.UTF8.GetMaxByteCount(value.Length)];
        var byteCount = Encoding.UTF8.GetBytes(value, buffer);
        _writer.Write(buffer[..byteCount]);
    }

    private void WriteUInt16BigEndian(ushort value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(ushort)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteInt16BigEndian(short value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(short)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteInt32BigEndian(int value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(int)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteInt64BigEndian(long value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(long)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteSingleBigEndian(float value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(float)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteDoubleBigEndian(double value)
    {
        Span<byte> buffer = stackalloc byte[sizeof(double)];
        WriteBigEndian(BitConverter.TryWriteBytes(buffer, value) ? buffer : throw new UnreachableException());
    }

    private void WriteBigEndian(Span<byte> buffer)
    {
        if (BitConverter.IsLittleEndian)
        {
            buffer.Reverse();
        }
        _writer.Write(buffer);
    }

    public static NbtWriter CreateCompressed(Stream stream, bool leaveOpen = true)
    {
        return new NbtWriter(new GZipStream(stream, CompressionMode.Compress, leaveOpen), leaveOpen = false);        
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}