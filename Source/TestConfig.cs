#if FANATEC_OLD_WAY
#else
#define FANATEC_OLD_WAY
#endif

using SimFeedback.conf;
using SimFeedback.extension.fanatec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtonBoxExtension
{

#if (FANATEC_OLD_WAY)

    [Serializable]
    public class TestConfig : ICustomConfig
    {

#else

    [Serializable]
    public class TestConfig : ICustomConfig//FanatecExtConfig
    {
#endif

        public int OverallIntesity { get; set; }
        public int ToggleStartStopButton { get; set; }
        public int ToggleDecreaseIntensity1Button { get; set; }
        public int ToggleIncreaseIntensity1Button { get; set; }
        public int ToggleDecreaseIntensity5Button { get; set; }
        public int ToggleIncreaseIntensity5Button { get; set; }
        public int ToggleDecreaseIntensity10Button { get; set; }
        public int ToggleIncreaseIntensity10Button { get; set; }
        public int ToggleDecreaseIntensity20Button { get; set; }
        public int ToggleIncreaseIntensity20Button { get; set; }

        public int LastusedGuidByte1 { get; set; }
        public int LastusedGuidByte2 { get; set; }
        public int LastusedGuidByte3 { get; set; }
        public int LastusedGuidByte4 { get; set; }
        public int LastusedGuidByte5 { get; set; }
        public int LastusedGuidByte6 { get; set; }
        public int LastusedGuidByte7 { get; set; }
        public int LastusedGuidByte8 { get; set; }
        public int LastusedGuidByte9 { get; set; }
        public int LastusedGuidByte10 { get; set; }
        public int LastusedGuidByte11 { get; set; }
        public int LastusedGuidByte12 { get; set; }
        public int LastusedGuidByte13 { get; set; }
        public int LastusedGuidByte14 { get; set; }
        public int LastusedGuidByte15 { get; set; }
        public int LastusedGuidByte16 { get; set; }

        public int StartStopDelay { get; set; }

    }
}
