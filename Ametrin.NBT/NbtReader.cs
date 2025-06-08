using System.IO;
using System.IO.Compression;
using System.Text;
using Ametrin.NBT.Tags;

namespace Ametrin.NBT;

public sealed class NbtReader(Stream stream, bool leaveOpen = false) : IDisposable
{
    private readonly BinaryReader _reader = new(stream, Encoding.UTF8, leaveOpen: leaveOpen);

    public Tag ReadTag()
    {
        var type = _reader.ReadByte();
        var name = ReadString();
        return ReadTag((TagType)type, name);
    }

    private Tag ReadTag(TagType type, string name) => type switch
    {
        TagType.Byte => new SbyteTag(name, _reader.ReadSByte()),
        TagType.Short => new ShortTag(name, ReadInt16BigEndian()),
        TagType.Int => new IntTag(name, ReadInt32BigEndian()),
        TagType.Long => new LongTag(name, ReadInt64BigEndian()),
        TagType.Float => new FloatTag(name, ReadSingleBigEndian()),
        TagType.Double => new DoubleTag(name, ReadDoubleBigEndian()),
        TagType.ByteArray => new ByteArrayTag(name, _reader.ReadBytes(ReadInt32BigEndian())),
        TagType.String => new StringTag(name, ReadString()),
        TagType.List => ReadList(name),
        TagType.Compound => new CompoundTag(name, ReadCompound()),
        TagType.IntArray => new IntArrayTag(name, ReadArray(ReadInt32BigEndian)),
        TagType.LongArray => new LongArrayTag(name, ReadArray(ReadInt64BigEndian)),
        _ => throw new NotSupportedException($"Unkown tag type {type}"),
    };

    private T[] ReadArray<T>(Func<T> reader)
    {
        var count = ReadInt32BigEndian();
        var result = new T[count];
        for (var i = 0; i < count; i++)
        {
            result[i] = reader();
        }
        return result;
    }

    private Dictionary<string, Tag> ReadCompound()
    {
        var result = new Dictionary<string, Tag>();
        while (true)
        {
            var elementType = (TagType)_reader.ReadByte();
            if (elementType is TagType.End) break;
            var elementName = ReadString();
            result[elementName] = ReadTag(elementType, elementName);
        }
        return result;
    }
    
    private ListTag ReadList(string name)
    {
        var elementType = (TagType)_reader.ReadByte();
        var count = ReadInt32BigEndian();
        var result = new List<Tag>(count);
        for (var i = 0; i < count; i++)
        {
            result.Add(ReadTag(elementType, ""));
        }
        return new(name, elementType, result);
    }

    private string ReadString()
    {
        var size = ReadUInt16BigEndian();
        Span<byte> buffer = stackalloc byte[size];
        _reader.BaseStream.ReadExactly(buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    private ushort ReadUInt16BigEndian() => BitConverter.ToUInt16(ReadBigEndian(stackalloc byte[2]));
    private short ReadInt16BigEndian() => BitConverter.ToInt16(ReadBigEndian(stackalloc byte[2]));
    private int ReadInt32BigEndian() => BitConverter.ToInt32(ReadBigEndian(stackalloc byte[4]));
    private long ReadInt64BigEndian() => BitConverter.ToInt64(ReadBigEndian(stackalloc byte[8]));
    private float ReadSingleBigEndian() => BitConverter.ToSingle(ReadBigEndian(stackalloc byte[4]));
    private double ReadDoubleBigEndian() => BitConverter.ToDouble(ReadBigEndian(stackalloc byte[8]));
    private Span<byte> ReadBigEndian(Span<byte> buffer)
    {
        _reader.BaseStream.ReadExactly(buffer);
        if (BitConverter.IsLittleEndian)
        {
            buffer.Reverse();
        }
        return buffer;
    }

    public static NbtReader CreateFromFile(string path)
    {
        Stream stream = File.OpenRead(path);
        Span<byte> header = stackalloc byte[2];
        stream.ReadExactly(header);
        stream.Position -= 2;
        if (header is [0x1F, 0x8B])
        {
            stream = new GZipStream(stream, CompressionMode.Decompress, leaveOpen: false);
        }
        else if (header is [var cmf, var flg] && (cmf & 0xF) == 8 && (((cmf << 8) + flg) % 32) == 0)
        {
            stream = new ZLibStream(stream, CompressionMode.Decompress, leaveOpen: false);
        }

        return new NbtReader(stream, leaveOpen: false);
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}
