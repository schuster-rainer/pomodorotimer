using System.IO;
using System.Text;

namespace PomodoroTimer.Plugin
{
    public class PythonStreamBridge : MemoryStream
    {
        IOutputStream output;
        public PythonStreamBridge(IOutputStream output)
        {
            this.output = output;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            output.Write(Encoding.UTF8.GetString(buffer, offset, count));
        }
    }
}