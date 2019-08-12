#if FANATEC_OLD_WAY
#else
//#define FANATEC_OLD_WAY
#endif

using SimFeedback.extension;
using SimFeedback.extension.fanatec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButtonBoxExtension
{
#if FANATEC_OLD_WAY
    public class TestExtension : AbstractSimFeedbackExtension
    {
#else
    public class TestExtension : AbstractSimFeedbackExtension//FanatecExtension
    {
#endif
        private TestConfig extConfig = null;
        private TestControl extControl = null;
        
        private ExtensionConfig conf;
        private SimFeedbackExtensionFacade facade;
        
        private bool simFeedbackLoaded = false;

        private int waitTimeBeforeSimFBInitialized = 3000;
        
        public TestExtension()
        {
            Name = "Button Box Extension";
            Info = "Start/Stop with the press of a button & Change intensity on the fly!";
            Version = "1.5.0";
            Author = "S4nder & HoiHman";
            NeedsOwnTab = false;
            HasControl = true;
        }

        public override void Init(SimFeedbackExtensionFacade facade, ExtensionConfig config)
        {
            base.Init(facade, config); // call this first
            Log("Initialize Fanatec Extension");

            LogDebug("Button Box: Reading Config");
            if (!(config.CustomConfig is TestConfig))
            {
                extConfig = new TestConfig()
                {
                    OverallIntesity = 50,
#if FANATEC_OLD_WAY
                    Test = 100,
#endif
                    ToggleStartStopButton = -1,
                    ToggleDecreaseIntensity1Button = -1,
                    ToggleIncreaseIntensity1Button = -1,
                    ToggleDecreaseIntensity5Button = -1,
                    ToggleIncreaseIntensity5Button = -1,
                    ToggleDecreaseIntensity10Button = -1,
                    ToggleIncreaseIntensity10Button = -1,
                    ToggleDecreaseIntensity20Button = -1,
                    ToggleIncreaseIntensity20Button = -1,
                    StartStopDelay = 5000
                };
                config.CustomConfig = extConfig;
            }
            extConfig = (TestConfig)config.CustomConfig;
            if (extConfig == null)
            {
                LogDebug("Button Box: No Config found, creating new config");
                extConfig = new TestConfig()
                {
                    OverallIntesity = 50,
#if FANATEC_OLD_WAY
                    Test = 100,
#endif
                    ToggleStartStopButton = -1,
                    ToggleDecreaseIntensity1Button = -1,
                    ToggleIncreaseIntensity1Button = -1,
                    ToggleDecreaseIntensity5Button = -1,
                    ToggleIncreaseIntensity5Button = -1,
                    ToggleDecreaseIntensity10Button = -1,
                    ToggleIncreaseIntensity10Button = -1,
                    ToggleDecreaseIntensity20Button = -1,
                    ToggleIncreaseIntensity20Button = -1,
                    StartStopDelay = 5000
                };
                config.CustomConfig = extConfig;
            }
            
            this.conf = config;
            this.facade = facade;

            extControl = new TestControl(facade, this, extConfig.ToggleStartStopButton, extConfig.ToggleDecreaseIntensity1Button, extConfig.ToggleIncreaseIntensity1Button, extConfig.ToggleDecreaseIntensity5Button, extConfig.ToggleIncreaseIntensity5Button, extConfig.ToggleDecreaseIntensity10Button, extConfig.ToggleIncreaseIntensity10Button, extConfig.ToggleDecreaseIntensity20Button, extConfig.ToggleIncreaseIntensity20Button);
            
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(waitTimeBeforeSimFBInitialized);
                simFeedbackLoaded = true;
                Thread.CurrentThread.Join();
            })).Start();
            
            //extConfig.OverallIntesity = 50;
            //extControl = new FanatecExtControl(this, facade);
        }
        
        public void SetToggleStartStopButton(int button)
        {
            extConfig.ToggleStartStopButton = button;
            SaveConfig();
        }

        public void SetStartStopDelay(int value)
        {
            extConfig.StartStopDelay = value;
            SaveConfig();
        }
        
        public int GetStartStopDelay()
        {
            return extConfig.StartStopDelay;
        }

        public byte[] LoadLastUsedController()
        {
            byte[] bytes = new byte[16];
            bytes[0] = (byte) extConfig.LastusedGuidByte1;
            bytes[1] = (byte) extConfig.LastusedGuidByte2;
            bytes[2] = (byte) extConfig.LastusedGuidByte3;
            bytes[3] = (byte) extConfig.LastusedGuidByte4;
            bytes[4] = (byte) extConfig.LastusedGuidByte5;
            bytes[5] = (byte) extConfig.LastusedGuidByte6;
            bytes[6] = (byte) extConfig.LastusedGuidByte7;
            bytes[7] = (byte) extConfig.LastusedGuidByte8;
            bytes[8] = (byte) extConfig.LastusedGuidByte9;
            bytes[9] = (byte) extConfig.LastusedGuidByte10;
            bytes[10] = (byte) extConfig.LastusedGuidByte11;
            bytes[11] = (byte) extConfig.LastusedGuidByte12;
            bytes[12] = (byte) extConfig.LastusedGuidByte13;
            bytes[13] = (byte) extConfig.LastusedGuidByte14;
            bytes[14] = (byte) extConfig.LastusedGuidByte15;
            bytes[15] = (byte) extConfig.LastusedGuidByte16;
            //MessageBox.Show(extConfig.LastusedGuidByte11 + " " + extConfig.LastusedGuidByte14);
            return bytes;
        }

        private void SaveConfig()
        {
            conf.CustomConfig = extConfig;

            if (simFeedbackLoaded)
            {
                facade.IncrementOverallIntensity();
                facade.DecrementOverallIntensity();
            }
            else
            {
                Thread th = new Thread(new ThreadStart(() => { Thread.Sleep(waitTimeBeforeSimFBInitialized); facade.IncrementOverallIntensity(); facade.DecrementOverallIntensity(); Thread.CurrentThread.Join(); }));
                th.Start();
            }
        }

        public void SaveLastUsedController(byte[] controllerBytes)
        {
            extConfig.LastusedGuidByte1 = controllerBytes[0];
            extConfig.LastusedGuidByte2 = controllerBytes[1];
            extConfig.LastusedGuidByte3 = controllerBytes[2];
            extConfig.LastusedGuidByte4 = controllerBytes[3];
            extConfig.LastusedGuidByte5 = controllerBytes[4];
            extConfig.LastusedGuidByte6 = controllerBytes[5];
            extConfig.LastusedGuidByte7 = controllerBytes[6];
            extConfig.LastusedGuidByte8 = controllerBytes[7];
            extConfig.LastusedGuidByte9 = controllerBytes[8];
            extConfig.LastusedGuidByte10 = controllerBytes[9];
            extConfig.LastusedGuidByte11 = controllerBytes[10];
            extConfig.LastusedGuidByte12 = controllerBytes[11];
            extConfig.LastusedGuidByte13 = controllerBytes[12];
            extConfig.LastusedGuidByte14 = controllerBytes[13];
            extConfig.LastusedGuidByte15 = controllerBytes[14];
            extConfig.LastusedGuidByte16 = controllerBytes[15];
            SaveConfig();
        }

        public override void Start()
        {
            if (!IsRunning)
            {
                SimFeedbackFacade.Log("Starting Test Extension");

                if (extControl != null && extControl.IsValid())
                {
                    extControl.Start();
                    IsRunning = true;
                }
            }
        }
        
        public override void Stop()
        {
            if (IsRunning)
            {
                Log("Stopping Test Extension");
                conf.CustomConfig = extConfig;
                if (extControl != null)
                {
                    extControl.Stop();
                }
                IsRunning = false;
            }
        }

        public void SetIsRunning(bool status)
        {
            IsRunning = status;
        }
        
        public override Control ExtensionControl
        {
            get { return extControl; }
        }

        public void SetToggleDecreaseIntensity1Button(int button)
        {
            extConfig.ToggleDecreaseIntensity1Button = button;
            SaveConfig();
        }

        public void SetToggleIncreaseIntensity1Button(int button)
        {
            extConfig.ToggleIncreaseIntensity1Button = button;
            SaveConfig();
        }


        public void SetToggleDecreaseIntensity5Button(int button)
        {
            extConfig.ToggleDecreaseIntensity5Button = button;
            SaveConfig();
        }

        public void SetToggleIncreaseIntensity5Button(int button)
        {
            extConfig.ToggleIncreaseIntensity5Button = button;
            SaveConfig();
        }


        public void SetToggleDecreaseIntensity10Button(int button)
        {
            extConfig.ToggleDecreaseIntensity10Button = button;
            SaveConfig();
        }

        public void SetToggleIncreaseIntensity10Button(int button)
        {
            extConfig.ToggleIncreaseIntensity10Button = button;
            SaveConfig();
        }


        public void SetToggleDecreaseIntensity20Button(int button)
        {
            extConfig.ToggleDecreaseIntensity20Button = button;
            SaveConfig();
        }
        
        public void SetToggleIncreaseIntensity20Button(int button)
        {
            extConfig.ToggleIncreaseIntensity20Button = button;
            SaveConfig();
        }
    }
}
