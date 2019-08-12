using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimFeedback.extension;
using WindowsInput;
using SharpDX.DirectInput;
using System.Threading;
using System.Diagnostics;
using WindowsInput.Native;

namespace ButtonBoxExtension
{
    public partial class TestControl : UserControl
    {
        private bool IsRunning = false;

        private SimFeedbackExtensionFacade facade = null;
        private TestExtension extension = null;

        private bool userMustSelectStartStopButton = false;

        private bool userMustSelectDecreaseIntensity1Button = false;
        private bool userMustSelectIncreaseIntensity1Button = false;

        private bool userMustSelectDecreaseIntensity5Button = false;
        private bool userMustSelectIncreaseIntensity5Button = false;

        private bool userMustSelectDecreaseIntensity10Button = false;
        private bool userMustSelectIncreaseIntensity10Button = false;

        private bool userMustSelectDecreaseIntensity20Button = false;
        private bool userMustSelectIncreaseIntensity20Button = false;
        
        private int startStopButton = -1;

        private int increaseIntensity1Button = -1;
        private int decreaseIntensity1Button = -1;

        private int increaseIntensity5Button = -1;
        private int decreaseIntensity5Button = -1;

        private int increaseIntensity10Button = -1;
        private int decreaseIntensity10Button = -1;

        private int increaseIntensity20Button = -1;
        private int decreaseIntensity20Button = -1;

        private bool simFeedbackIsOn = false;

        private static readonly int TIME_INTERVAL_IN_MILLISECONDS = 1000 / 60;
        private static readonly int MAX_16BIT = 65536;

        private DirectInput directInput;
        private Guid wheelGuid = Guid.Empty;
        private Joystick joystick;
        private JoystickState joystickState;
        private System.Threading.Timer pollTimer;
        // @see https://inputsimulator.codeplex.com/
        private InputSimulator inputSim;

        private bool IsEnabled = false;

        bool leftButtonDown = false;
        bool leftButtonUp = true;
        double mouseSpeed = 2.0;
        int pollCounter = 0;

        List<KeyValuePair<Guid, string>> joystickList = new List<KeyValuePair<Guid, string>>();

        private Stopwatch preventSpamClickStartStopStopWatch = new Stopwatch();

        private int startStopDelay;

        private KeyValuePair<Guid, string> selectedJoystick = new KeyValuePair<Guid, string>(Guid.Empty, null);

        private bool componentsInitialized = false;

        public TestControl(SimFeedbackExtensionFacade facade, TestExtension extension, int startStopButton = -1, int decreaseIntensity1Button = -1, int increaseIntensity1Button = -1, int decreaseIntensity5Button = -1, int increaseIntensity5Button = -1, int decreaseIntensity10Button = -1, int increaseIntensity10Button = -1, int decreaseIntensity20Button = -1, int increaseIntensity20Button = -1)
        {
            this.startStopButton = startStopButton;
            this.decreaseIntensity1Button = decreaseIntensity1Button;
            this.increaseIntensity1Button = increaseIntensity1Button;
            this.decreaseIntensity5Button = decreaseIntensity5Button;
            this.increaseIntensity5Button = increaseIntensity5Button;
            this.decreaseIntensity10Button = decreaseIntensity10Button;
            this.increaseIntensity10Button = increaseIntensity10Button;
            this.decreaseIntensity20Button = decreaseIntensity20Button;
            this.increaseIntensity20Button = increaseIntensity20Button;
            this.facade = facade;
            this.extension = extension;
            directInput = new DirectInput();

            startStopDelay = extension.GetStartStopDelay();
            refreshBtn_Click(null, null);
            if (joystickList.Count > 0)
            {
                enableExtensionButton_Click(null, null);
            }
            
            InitializeComponent();
        }

        private void changeStartStopButtonButton_Click(object sender, EventArgs e)
        {
            changeStartStopButtonButton.BackColor = default(Color);
            if (!userMustSelectStartStopButton)
            {
                userMustSelectStartStopButton = true;
                changeStartStopButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectStartStopButton = false;
                changeStartStopButtonButton.Text = "Change Start-Stop Button";
                startStopButton = -1;
                extension.SetToggleStartStopButton(-1);
            }
        }

        public void finishStartStopButton()
        {
            userMustSelectStartStopButton = false;
            changeStartStopButtonButton.Text = "Change Start-Stop Button (Button " + startStopButton + ")";
            changeStartStopButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishDecreaseIntensity1Button()
        {
            userMustSelectDecreaseIntensity1Button = false;
            changeDecreaseIntensity1ButtonButton.Text = "Change Decrease-Intensity Button (1% intensity per click) (Button " + decreaseIntensity1Button + ")";
            changeDecreaseIntensity1ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishIncreaseIntensity1Button()
        {
            userMustSelectIncreaseIntensity1Button = false;
            changeIncreaseIntensity1ButtonButton.Text = "Change Increase-Intensity Button (1% intensity per click) (Button " + increaseIntensity1Button + ")";
            changeIncreaseIntensity1ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishDecreaseIntensity5Button()
        {
            userMustSelectDecreaseIntensity5Button = false;
            changeDecreaseIntensity5ButtonButton.Text = "Change Decrease-Intensity Button (5% intensity per click) (Button " + decreaseIntensity5Button + ")";
            changeDecreaseIntensity5ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishIncreaseIntensity5Button()
        {
            userMustSelectIncreaseIntensity5Button = false;
            changeIncreaseIntensity5ButtonButton.Text = "Change Increase-Intensity Button (5% intensity per click) (Button " + increaseIntensity5Button + ")";
            changeIncreaseIntensity5ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishDecreaseIntensity10Button()
        {
            userMustSelectDecreaseIntensity10Button = false;
            changeDecreaseIntensity10ButtonButton.Text = "Change Decrease-Intensity Button (10% intensity per click) (Button " + decreaseIntensity10Button + ")";
            changeDecreaseIntensity10ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishIncreaseIntensity10Button()
        {
            userMustSelectIncreaseIntensity10Button = false;
            changeIncreaseIntensity10ButtonButton.Text = "Change Increase-Intensity Button (10% intensity per click) (Button " + increaseIntensity10Button + ")";
            changeIncreaseIntensity10ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishDecreaseIntensity20Button()
        {
            userMustSelectDecreaseIntensity20Button = false;
            changeDecreaseIntensity20ButtonButton.Text = "Change Decrease-Intensity Button (20% intensity per click) (Button " + decreaseIntensity20Button + ")";
            changeDecreaseIntensity20ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void finishIncreaseIntensity20Button()
        {
            userMustSelectIncreaseIntensity20Button = false;
            changeIncreaseIntensity20ButtonButton.Text = "Change Increase-Intensity Button (20% intensity per click) (Button " + increaseIntensity20Button + ")";
            changeIncreaseIntensity20ButtonButton.BackColor = Color.FromArgb(128, 255, 128);
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                //JoystickInit();
            }
        }
        private void JoystickInit()
        {
            if (wheelGuid == Guid.Empty)
            {
                //MessageBox.Show("Please select Controller", "Notice", MessageBoxButtons.OK);
                IsEnabled = false;
                if (componentsInitialized)
                {
                    ///OLD CODE
                    //enableExtensionButton.Text = "Enable Extension";
                }
                return;
            }
            
            inputSim = new InputSimulator();
            
            // Joystick
            joystick = new Joystick(directInput, wheelGuid);
            joystick.Properties.AxisMode = DeviceAxisMode.Absolute;
            joystick.Acquire();
            
            joystickState = new JoystickState();

            // Start the poll thread using a Timer
            pollTimer = new System.Threading.Timer(Poll, null, TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);
            
            IsEnabled = true;
            if (componentsInitialized)
            {
                ///OLD CODE
                //enableExtensionButton.Text = "Disable Extension";
            }
            
            //ListInputDevices();
            extension.SetIsRunning(true);
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                JoystickStop();
            }
        }

        private void JoystickStop()
        {
            if (!IsEnabled)
            {
                return;
            }

            if (pollTimer != null) 
            {
                pollTimer.Dispose();
            }

            // Joystick 
            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
                joystick = null;
                joystickState = null;
            }

            inputSim = null;

            IsEnabled = false;
            if (componentsInitialized)
            {
                ///OLD CODE
                //enableExtensionButton.Text = "Enable Extension";
            }

            extension.SetIsRunning(false);
        }
        
        internal bool IsValid()
        {
            if (joystickList.Count > 0)
            {
                return (joystickList.First().Value.IndexOf("FANATEC") != -1);
            }
            return false;
            //return true;
        }

        private void TestControl_KeyDown(object sender, KeyEventArgs e)
        {
        }

        bool isStartStopButtonDown = false;
        bool isDecreaseIntensity1ButtonDown = false;
        bool isIncreaseIntensity1ButtonDown = false;
        bool isDecreaseIntensity5ButtonDown = false;
        bool isIncreaseIntensity5ButtonDown = false;
        bool isDecreaseIntensity10ButtonDown = false;
        bool isIncreaseIntensity10ButtonDown = false;
        bool isDecreaseIntensity20ButtonDown = false;
        bool isIncreaseIntensity20ButtonDown = false;

        void CheckButtons()
        {
            try
            {
                //TraceButtons();

                joystick.GetCurrentState(ref joystickState);
                bool[] buttons = joystickState.Buttons;

                //bool isButtonPushed = false;
                //foreach(bool i in buttons) {
                //    if (i)
                //    {
                //        isButtonPushed = true;
                //        break;
                //    }     
                //}
                //if (!isButtonPushed) return;

                // Mouse Click, push the joystick button
                ///if (buttons[11])
                ///{
                ///    if (leftButtonUp)
                ///    {
                ///        inputSim.Mouse.LeftButtonDown();
                ///        leftButtonDown = true;
                ///        leftButtonUp = false;
                ///    }
                ///}
                ///else
                ///{
                ///    if (leftButtonDown)
                ///    {
                ///        inputSim.Mouse.LeftButtonUp();
                ///        leftButtonUp = true;
                ///        leftButtonDown = false;
                ///    }
                ///}

                // Oben links Grün
                //if (buttons[9])
                //{
                //    inputSim.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
                //}

                // Unten links, Blau,  Orange 
                ///if (buttons[6])
                ///{
                ///    simFeedbackFacade.IncrementOverallIntensity();
                ///}
                ///if (buttons[8])
                ///{
                ///    simFeedbackFacade.DecrementOverallIntensity();
                ///}

                // unten rechts schwarz
                ///if (buttons[4])
                ///{
                ///    inputSim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                ///    inputSim.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                ///    System.Threading.Thread.Sleep(100);
                ///    inputSim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                ///    System.Threading.Thread.Sleep(200);
                ///}

                // unten links schwarz

                // Assetto Corsa
                // Toggle ASW with hotkeys:
                // CTRL+Numpad1: Disable ASW, go back to the original ATW mode
                // CTRL+Numpad2: Force apps to 45Hz, ASW disabled
                // CTRL+Numpad3: Force apps to 45Hz, ASW enabled
                // CTRL+Numpad4: Enable auto-ASW (default, use this first)
                ///if (buttons[5])
                ///{
                ///    inputSim.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                ///    if (awsState == AWS.DISABLED)
                ///    {
                ///        inputSim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD4);
                ///        awsState = AWS.ENABLED;
                ///    }
                ///    else
                ///    {
                ///        inputSim.Keyboard.KeyPress(VirtualKeyCode.NUMPAD1);
                ///        awsState = AWS.DISABLED;
                ///    }
                ///    inputSim.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                ///}
                if (preventSpamClickStartStopStopWatch.ElapsedMilliseconds > (int)startStopDelay)
                {
                    preventSpamClickStartStopStopWatch.Stop();
                }
                if (userMustSelectStartStopButton)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            startStopButton = i;
                            finishStartStopButton();
                            extension.SetToggleStartStopButton(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectDecreaseIntensity1Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            decreaseIntensity1Button = i;
                            finishDecreaseIntensity1Button();
                            extension.SetToggleDecreaseIntensity1Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectIncreaseIntensity1Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            increaseIntensity1Button = i;
                            finishIncreaseIntensity1Button();
                            extension.SetToggleIncreaseIntensity1Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectDecreaseIntensity5Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            decreaseIntensity5Button = i;
                            finishDecreaseIntensity5Button();
                            extension.SetToggleDecreaseIntensity5Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectIncreaseIntensity5Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            increaseIntensity5Button = i;
                            finishIncreaseIntensity5Button();
                            extension.SetToggleIncreaseIntensity5Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectDecreaseIntensity10Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            decreaseIntensity10Button = i;
                            finishDecreaseIntensity10Button();
                            extension.SetToggleDecreaseIntensity10Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectIncreaseIntensity10Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            increaseIntensity10Button = i;
                            finishIncreaseIntensity10Button();
                            extension.SetToggleIncreaseIntensity10Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectDecreaseIntensity20Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            decreaseIntensity20Button = i;
                            finishDecreaseIntensity20Button();
                            extension.SetToggleDecreaseIntensity20Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else if (userMustSelectIncreaseIntensity20Button)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (buttons[i])
                        {
                            increaseIntensity20Button = i;
                            finishIncreaseIntensity20Button();
                            extension.SetToggleIncreaseIntensity20Button(i);
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }
                else
                {
                    if (IsEnabled && extension.IsActivated)
                    {
                        if (startStopButton >= 0 && startStopButton < buttons.Length)
                        {
                            if (preventSpamClickStartStopStopWatch.ElapsedMilliseconds > (int)startStopDelay || !preventSpamClickStartStopStopWatch.IsRunning)
                            {
                                preventSpamClickStartStopStopWatch.Reset();
                                if (!buttons[startStopButton] && isStartStopButtonDown)
                                {
                                    simFeedbackIsOn = !facade.IsRunning();
                                    if (simFeedbackIsOn)
                                    {
                                        if (componentsInitialized)
                                        {
                                            label1.Text = "SimFeedback is turned ON";
                                        }
                                        facade.Start();
                                    }
                                    else
                                    {
                                        if (componentsInitialized)
                                        {
                                            label1.Text = "SimFeedback is turned OFF";
                                        }
                                        facade.Stop();
                                    }
                                    preventSpamClickStartStopStopWatch.Start();
                                }
                                isStartStopButtonDown = buttons[startStopButton];
                            }
                        }
                        if (decreaseIntensity1Button >= 0 && decreaseIntensity1Button < buttons.Length)
                        {
                            if (!buttons[decreaseIntensity1Button] && isDecreaseIntensity1ButtonDown)
                            {
                                facade.DecrementOverallIntensity();
                            }
                            isDecreaseIntensity1ButtonDown = buttons[decreaseIntensity1Button];
                        }
                        if (increaseIntensity1Button >= 0 && increaseIntensity1Button < buttons.Length)
                        {
                            if (!buttons[increaseIntensity1Button] && isIncreaseIntensity1ButtonDown)
                            {
                                facade.IncrementOverallIntensity();
                            }
                            isIncreaseIntensity1ButtonDown = buttons[increaseIntensity1Button];
                        }


                        if (decreaseIntensity5Button >= 0 && decreaseIntensity5Button < buttons.Length)
                        {
                            if (!buttons[decreaseIntensity5Button] && isDecreaseIntensity5ButtonDown)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    facade.DecrementOverallIntensity();
                                }
                            }
                            isDecreaseIntensity5ButtonDown = buttons[decreaseIntensity5Button];
                        }
                        if (increaseIntensity5Button >= 0 && increaseIntensity5Button < buttons.Length)
                        {
                            if (!buttons[increaseIntensity5Button] && isIncreaseIntensity5ButtonDown)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    facade.IncrementOverallIntensity();
                                }
                            }
                            isIncreaseIntensity5ButtonDown = buttons[increaseIntensity5Button];
                        }


                        if (decreaseIntensity10Button >= 0 && decreaseIntensity10Button < buttons.Length)
                        {
                            if (!buttons[decreaseIntensity10Button] && isDecreaseIntensity10ButtonDown)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    facade.DecrementOverallIntensity();
                                }
                            }
                            isDecreaseIntensity10ButtonDown = buttons[decreaseIntensity10Button];
                        }
                        if (increaseIntensity10Button >= 0 && increaseIntensity10Button < buttons.Length)
                        {
                            if (!buttons[increaseIntensity10Button] && isIncreaseIntensity10ButtonDown)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    facade.IncrementOverallIntensity();
                                }
                            }
                            isIncreaseIntensity10ButtonDown = buttons[increaseIntensity10Button];
                        }


                        if (decreaseIntensity20Button >= 0 && decreaseIntensity20Button < buttons.Length)
                        {
                            if (!buttons[decreaseIntensity20Button] && isDecreaseIntensity20ButtonDown)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    facade.DecrementOverallIntensity();
                                }
                            }
                            isDecreaseIntensity20ButtonDown = buttons[decreaseIntensity20Button];
                        }
                        if (increaseIntensity20Button >= 0 && increaseIntensity20Button < buttons.Length)
                        {
                            if (!buttons[increaseIntensity20Button] && isIncreaseIntensity20ButtonDown)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    facade.IncrementOverallIntensity();
                                }
                            }
                            isIncreaseIntensity20ButtonDown = buttons[increaseIntensity20Button];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    Trace.Write(e.ToString());
                }
            }
        }
        
        void Poll(Object state)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            try
            {
                joystick.Acquire();

                // Joystick
                joystick.Poll();

                CheckButtons();

                //if (pollCounter % 2 == 0)
                //{
                //    PushButtons();
                //}

                //if (IsEnabled)
                //{
                    pollTimer.Change(Math.Max(0, TIME_INTERVAL_IN_MILLISECONDS - watch.ElapsedMilliseconds), Timeout.Infinite);
                //}
                //else
                //{
                //    Stop();
                //}
                


                if (pollCounter == Int16.MaxValue) pollCounter = 0;
                pollCounter++;
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                    Trace.Write(e.ToString());
                }
            }
        }

        private void SaveController()
        {
            wheelGuid = this.selectedJoystick.Key;
            
            byte[] guidBytes = wheelGuid.ToByteArray();

            extension.SaveLastUsedController(guidBytes);
            //MessageBox.Show(((KeyValuePair<Guid, string>)comboBoxJoysticks.SelectedItem).Value);
            //ListInputDevices();
        }
        
        private void ListInputDevices()
        {
            // Find a Joystick Guid
            joystickList.Clear();
            var joystickGuid = Guid.Empty;
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.Driving, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    MessageBox.Show("Driving Added!");
            ///}
            ///
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    //MessageBox.Show("Joystick Added!");
            ///}
            ///
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.FirstPerson, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    //MessageBox.Show("Joystick Added!");
            ///}
            ///
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.Flight, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    //MessageBox.Show("Joystick Added!");
            ///}
            ///
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    //MessageBox.Show("Joystick Added!");
            ///}
            ///
            ///foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            ///{
            ///    joystickGuid = deviceInstance.InstanceGuid;
            ///    joystickList.Add(
            ///        new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
            ///    //MessageBox.Show("Gamepad Added!");
            ///}
            
            KeyValuePair<Guid, string> selectedJoystick = new KeyValuePair<Guid, string>(Guid.Empty, null);
            
            byte[] savedGuidBytes = extension.LoadLastUsedController();
            string s = "";
            for(int i = 0; i < 16; i++)
            {
                s += (int)savedGuidBytes[i];
            }
            //MessageBox.Show(s);
            if(savedGuidBytes == null)
            {
                savedGuidBytes = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            
            foreach (var deviceInstance in directInput.GetDevices())
            {
                if(deviceInstance.Type == DeviceType.Mouse)
                {
                    continue;
                }
                joystickGuid = deviceInstance.InstanceGuid;
                joystickList.Add(
                    new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName));
                byte[] guidBytes = joystickGuid.ToByteArray();
                for (int i = 0; i < 16; i++)
                {
                    if (guidBytes[i] != savedGuidBytes[i])
                    {
                        break;
                    }
                    else if(i == 15)
                    {
                        //MessageBox.Show(i + " " + deviceInstance.InstanceName);
                        selectedJoystick = new KeyValuePair<Guid, string>(joystickGuid, deviceInstance.InstanceName);
                    }
                }
                //MessageBox.Show("Gamepad Added!");
            }
            if (joystickList.Count > 0)
            {
                this.selectedJoystick = joystickList[0];
            }

            if (selectedJoystick.Key != Guid.Empty && selectedJoystick.Value != null)
            {

                //MessageBox.Show(selectedJoystick.Key + "");
                
                this.selectedJoystick = selectedJoystick;
                if (componentsInitialized)
                {
                    comboBoxJoysticks.SelectedItem = this.selectedJoystick;
                }
                //MessageBox.Show(selectedJoystick.Value);
            }

            // Default set the first Guid to be used
            // Autostart Mouse if Fanatec is detected
            //if (joystickList.Count > 0)
            //{
            //    wheelGuid = joystickList.First().Key;
            //    if (joystickList.First().Value.IndexOf("FANATEC") != -1)
            //    {
            //        StartStopToggle();
            //    }
            //}
            wheelGuid = Guid.Empty;
            SaveController();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            ListInputDevices();
        }

        //private void enableExtensionButton_Click(object sender, EventArgs e)
        //{
        //    if (componentsInitialized)
        //    {
        //        //enableExtensionButton.BackColor = default(Color);
        //    }
        //    if (IsEnabled)
        //    {
        //        JoystickStop();
        //        IsEnabled = false;
        //    }
        //    else
        //    {
        //        IsEnabled = true;
        //        JoystickInit();
        //        if (!IsEnabled)
        //        {
        //            MessageBox.Show("Please select a controller before enabling this extension!");
        //        }
        //    }
        //}

        private void enableExtensionButton_Click(object sender, EventArgs e)
        {
            if (componentsInitialized)
            {
                //enableExtensionButton.BackColor = default(Color);
            }
            if (IsEnabled)
            {
                JoystickStop();
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
                JoystickInit();
                if (!IsEnabled)
                {
                    MessageBox.Show("Please select a controller before enabling this extension!");
                }
            }
        }

        private void TestControl_Load(object sender, EventArgs e)
        {
            startStopDelayNumUpDown.Value = startStopDelay;

            comboBoxJoysticks.DataSource = new BindingSource(joystickList, null);
            comboBoxJoysticks.DisplayMember = "Value";
            comboBoxJoysticks.ValueMember = "Key";
            
            comboBoxJoysticks.SelectedItem = this.selectedJoystick;
            
            componentsInitialized = true;
            if (componentsInitialized)
            {
                if (simFeedbackIsOn)
                {
                    label1.Text = "SimFeedback is turned ON";
                }
                else
                {
                    label1.Text = "SimFeedback is turned OFF";
                }
                if (IsEnabled)
                {
                    ///OLD CODE
                    //enableExtensionButton.Text = "Disable Extension";
                }
                else
                {
                    ///OLD CODE
                    //enableExtensionButton.Text = "Enable Extension";
                }
            }
            if (startStopButton != -1)
            {
                finishStartStopButton();
            }
            if (increaseIntensity1Button != -1)
            {
                finishIncreaseIntensity1Button();
            }
            if (decreaseIntensity1Button != -1)
            {
                finishDecreaseIntensity1Button();
            }
            if (increaseIntensity5Button != -1)
            {
                finishIncreaseIntensity5Button();
            }
            if (decreaseIntensity5Button != -1)
            {
                finishDecreaseIntensity5Button();
            }
            if (increaseIntensity10Button != -1)
            {
                finishIncreaseIntensity10Button();
            }
            if (decreaseIntensity10Button != -1)
            {
                finishDecreaseIntensity10Button();
            }
            if (increaseIntensity20Button != -1)
            {
                finishIncreaseIntensity20Button();
            }
            if (decreaseIntensity20Button != -1)
            {
                finishDecreaseIntensity20Button();
            }
        }

        private void startStopDelayNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            startStopDelay = (int) startStopDelayNumUpDown.Value;
            extension.SetStartStopDelay((int)startStopDelayNumUpDown.Value);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void comboBoxJoysticks_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (componentsInitialized)
            {
                //enableExtensionButton.BackColor = Color.IndianRed;
                //enableExtensionButton.Text = "Please re-enable this extension by clicking this button twice!";
            }
            KeyValuePair<Guid, string> item = (KeyValuePair<Guid, string>)comboBoxJoysticks.SelectedItem;
            if (item.Key != Guid.Empty && item.Value != null)
            {
                bool warnUserBecauseOfMouse = false;
                foreach (var device in directInput.GetDevices()) 
                {
                    if (device.InstanceGuid == item.Key && device.Type == DeviceType.Mouse)
                    {
                        warnUserBecauseOfMouse = true;
                    }
                }
                if (warnUserBecauseOfMouse && MessageBox.Show("Do you really want to use your mouse as a controller?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    if (componentsInitialized)
                    {
                        comboBoxJoysticks.SelectedItem = this.selectedJoystick;
                    }
                }
                else
                {
                    this.selectedJoystick = item;
                    if (componentsInitialized)
                    {
                        comboBoxJoysticks.SelectedItem = this.selectedJoystick;
                    }
                }
                //MessageBox.Show(selectedJoystick.Value);
            }
            SaveController();
            enableExtensionButton_Click(null, null);
            enableExtensionButton_Click(null, null);
        }

        private void comboBoxJoysticks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void changeDecreaseIntensity1ButtonButton_Click(object sender, EventArgs e)
        {
            changeDecreaseIntensity1ButtonButton.BackColor = default(Color);
            if (!userMustSelectDecreaseIntensity1Button)
            {
                userMustSelectDecreaseIntensity1Button = true;
                changeDecreaseIntensity1ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectDecreaseIntensity1Button = false;
                changeDecreaseIntensity1ButtonButton.Text = "Change Decrease-Intensity Button (1% intensity per click)";
                decreaseIntensity1Button = -1;
                extension.SetToggleDecreaseIntensity1Button(-1);
            }
        }

        private void changeIncreaseIntensity1ButtonButton_Click(object sender, EventArgs e)
        {
            changeIncreaseIntensity1ButtonButton.BackColor = default(Color);
            if (!userMustSelectIncreaseIntensity1Button)
            {
                userMustSelectIncreaseIntensity1Button = true;
                changeIncreaseIntensity1ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectIncreaseIntensity1Button = false;
                changeIncreaseIntensity1ButtonButton.Text = "Change Increase-Intensity Button (1% intensity per click)";
                increaseIntensity1Button = -1;
                extension.SetToggleIncreaseIntensity1Button(-1);
            }
        }

        private void ChangeDecreaseIntensity5ButtonButton_Click(object sender, EventArgs e)
        {
            changeDecreaseIntensity5ButtonButton.BackColor = default(Color);
            if (!userMustSelectDecreaseIntensity5Button) 
            {
                userMustSelectDecreaseIntensity5Button = true;
                changeDecreaseIntensity5ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectDecreaseIntensity5Button = false;
                changeDecreaseIntensity5ButtonButton.Text = "Change Decrease-Intensity Button (5% intensity per click)";
                decreaseIntensity5Button = -1;
                extension.SetToggleDecreaseIntensity5Button(-1);
            }
        }

        private void ChangeIncreaseIntensity5ButtonButton_Click(object sender, EventArgs e)
        {
            changeIncreaseIntensity5ButtonButton.BackColor = default(Color);
            if (!userMustSelectIncreaseIntensity5Button)
            {
                userMustSelectIncreaseIntensity5Button = true;
                changeIncreaseIntensity5ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectIncreaseIntensity5Button = false;
                changeIncreaseIntensity5ButtonButton.Text = "Change Increase-Intensity Button (5% intensity per click)";
                increaseIntensity5Button = -1;
                extension.SetToggleIncreaseIntensity5Button(-1);
            }
        }

        private void ChangeDecreaseIntensity10ButtonButton_Click(object sender, EventArgs e)
        {
            changeDecreaseIntensity10ButtonButton.BackColor = default(Color);
            if (!userMustSelectDecreaseIntensity10Button)
            {
                userMustSelectDecreaseIntensity10Button = true;
                changeDecreaseIntensity10ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectDecreaseIntensity10Button = false;
                changeDecreaseIntensity10ButtonButton.Text = "Change Decrease-Intensity Button (10% intensity per click)";
                decreaseIntensity10Button = -1;
                extension.SetToggleDecreaseIntensity10Button(-1);
            }
        }

        private void ChangeIncreaseIntensity10ButtonButton_Click(object sender, EventArgs e)
        {
            changeIncreaseIntensity10ButtonButton.BackColor = default(Color);
            if (!userMustSelectIncreaseIntensity10Button)
            {
                userMustSelectIncreaseIntensity10Button = true;
                changeIncreaseIntensity10ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectIncreaseIntensity10Button = false;
                changeIncreaseIntensity10ButtonButton.Text = "Change Increase-Intensity Button (10% intensity per click)";
                increaseIntensity10Button = -1;
                extension.SetToggleIncreaseIntensity10Button(-1);
            }
        }

        private void ChangeDecreaseIntensity20ButtonButton_Click(object sender, EventArgs e)
        {
            changeDecreaseIntensity20ButtonButton.BackColor = default(Color);
            if (!userMustSelectDecreaseIntensity20Button)
            {
                userMustSelectDecreaseIntensity20Button = true;
                changeDecreaseIntensity20ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectDecreaseIntensity20Button = false;
                changeDecreaseIntensity20ButtonButton.Text = "Change Decrease-Intensity Button (20% intensity per click)";
                decreaseIntensity20Button = -1;
                extension.SetToggleDecreaseIntensity20Button(-1);
            }
        }
        
        private void ChangeIncreaseIntensity20ButtonButton_Click(object sender, EventArgs e)
        {
            changeIncreaseIntensity20ButtonButton.BackColor = default(Color);
            if (!userMustSelectIncreaseIntensity20Button)
            {
                userMustSelectIncreaseIntensity20Button = true;
                changeIncreaseIntensity20ButtonButton.Text = "Press a joystick button (or click here to cancel)";
            }
            else
            {
                userMustSelectIncreaseIntensity20Button = false;
                changeIncreaseIntensity20ButtonButton.Text = "Change Increase-Intensity Button (20% intensity per click)";
                increaseIntensity20Button = -1;
                extension.SetToggleIncreaseIntensity20Button(-1);
            }
        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }
    }
}
