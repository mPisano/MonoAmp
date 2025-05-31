using System; // For Int32, String, bool

namespace AmpBlazorServer.Models
{
    public class AmpStateDto
    {
        public Int32 Units;
        public Int32 KeypadCount;
        public Int32 ResetCount;
        public bool AmpIsRunning;
        public bool AmpIsResponding;
        public String[]? Sources;
    }
}
