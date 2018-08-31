using System;
using System.IO;
using System.Reflection;

namespace Tiger
{
    public class ExecutableInfo
    {
        public AssemblyName AssemblyName { get; private set; }
        public string PathInput { get; private set; }
        public string FileDirectory { get { return Path.GetDirectoryName(PathInput); } }
        public string OutputName { get { return Path.GetFileName(Path.ChangeExtension(PathInput, "exe")); } }

        public ExecutableInfo(string input_path, string version)
        {
            PathInput = Directory.Exists(Path.GetDirectoryName(input_path))
                ? input_path : Path.Combine(Environment.CurrentDirectory, input_path);
            AssemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(PathInput));
            AssemblyName.Version = new Version(version);
        }
    }
}
