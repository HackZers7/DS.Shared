using System;
using System.IO;
using System.Threading.Tasks;
using DS.Shared.IO;
using NUnit.Framework;

namespace DS.Shared.Tests;

[TestFixture]
public class ProgressTest
{
    private static readonly string _writePath = "d:\\test.png";

    [Test]
    public async Task Read()
    {
        var progress = new Progress<int>(x => TestContext.WriteLine(x));

        using (var progressStream = new ProgressStream(File.OpenRead(IoC.FilePath), progress))
        {
            using (var write = File.OpenWrite(_writePath))
            {
                await progressStream.CopyToAsync(write);
            }
        }
    }
}
