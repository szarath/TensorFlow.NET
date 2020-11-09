﻿using System.IO;

namespace TensorFlowNET.UnitTest
{
    public class TestHelper
    {
        public static string GetFullPathFromDataDir(string fileName)
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "data");
            return Path.GetFullPath(Path.Combine(dir, fileName));
        }
    }
}
