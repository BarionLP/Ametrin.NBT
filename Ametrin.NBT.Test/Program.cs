using Ametrin.NBT;

using var reader = NbtReader.CreateFromFile(@"C:\Users\Barion\AppData\Roaming\.curseforge\Instances\1.21.4 Test\saves\New World\level.dat");


var tag = reader.ReadTag();
var s1 = tag.ToString();

Console.WriteLine(tag);

var file = Path.GetTempFileName();
System.Console.WriteLine(file);
var ms = File.Create(file);
using var writer = NbtWriter.CreateCompressed(ms);
writer.WriteTag(tag);
writer.Dispose();
ms.Dispose();

// Console.WriteLine(Convert.ToHexString(ms.ToArray()));
// ms.Position = 0;

using var reader2 = NbtReader.CreateFromFile(file);

tag = reader2.ReadTag();
var s2 = tag.ToString();
reader2.Dispose();

Console.WriteLine();
Console.WriteLine(tag);

Console.WriteLine(s1 == s2);

File.Delete(file);
