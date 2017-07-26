using System.Diagnostics;
using System.IO;

namespace NCmdArgs
{
    public class VerbCallerData
    {
        public VerbCallerData(string executable, string[] args)
        {
            Executable = executable;
            Args = args;
        }

        public string Executable { get; private set; }
        
        public string[] Args { get; private set; }

        public void RunAndWait(TextWriter output, TextWriter errorOutput)
        {
            var psi = new ProcessStartInfo
            {
                FileName = this.Executable,
                Arguments = string.Join(" ", Args),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            var proc = new Process {StartInfo = psi};

            proc.OutputDataReceived += (sender, args) => output.Write(args.Data);
            proc.ErrorDataReceived += (sender, args) => errorOutput.Write(args.Data);


            proc.WaitForExit();

        }
    }
}
