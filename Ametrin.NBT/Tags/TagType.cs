namespace Ametrin.NBT.Tags;

public enum TagType : byte
{
    End = 0,
    Byte = 1, // signed byte
    Short = 2, // 2 bytes, signed, big endian
    Int = 3, // 4 bytes, signed, big endian
    Long = 4, // 8 bytes, signed, big endian,
    Float = 5, // 4 bytes, big endian
    Double = 6, // 8 bytes, big endian
    ByteArray = 7, // int size followed by that many bytes
    String = 8, // unsigned short followed by that many bytes (UTF-8 encoded)
    List = 9, // type byte, int count, that many entries
    Compound = 10, // list of name value pairs, ended by End
    IntArray = 11, // int size followed by that many ints
    LongArray = 12, // int size followed by that many longs 
}
