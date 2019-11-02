using FinchAPI;
using System;
using System.Collections.Generic;
using System.IO;

namespace FinchControl_Starter
{
    #region  Program Summary
    // *******************************
    // Title: Finch Control
    // Description: Main Menu Program for Finch Robot
    // Application Type: Console
    // Author: Cole Crain
    // Dated Created: 10/2/2019
    // Last Modified: 10/27/2019
    // *******************************
    #endregion

    class Program
    {

        #region Enum Command
        public enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            DONE
        }
        #endregion

        /// <summary>
        /// Main Display Screens
        /// <summary>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }


        /// <summary>
        ///Color Theme
        /// <summary>
        static void SetTheme()
        {
            string dataPath = @"Data\Theme.txt";
            string foregroundColorString;
            ConsoleColor foregroundColor;

            foregroundColorString = File.ReadAllText(dataPath);

            Enum.TryParse(foregroundColorString, out foregroundColor);

            Console.ForegroundColor = foregroundColor;

        }

        #region Main Menu Choices
        /// <summary>
        /// Main Menu
        /// <summary>
        static void DisplayMainMenu()
        {
            // installation

            Finch finchRobot = new Finch();

            bool finchRobotConnected = false;
            bool quitApplication = false;
            bool invalidResponse = false;
            char menuChoice;
            ConsoleKeyInfo menuChoiceKey;
            //string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get menu choice
                //
                Console.WriteLine("a) Connect Finch Robot");
                Console.WriteLine("b) Talent Show");
                Console.WriteLine("c) Data Recorder");
                Console.WriteLine("d) Alarm System");
                Console.WriteLine("e) User Programming");
                Console.WriteLine("f) Disconnect Finch Robot");
                Console.WriteLine("q) Quit");
                Console.WriteLine("Enter Choice");
                //menuChoice = Console.ReadLine().ToUpper();

                Finch myFinch;
                myFinch = new Finch();

                /// <summary>
                ///  Process Menu Choices
                /// <summary>
                /// 
                if (invalidResponse)
                {
                    Console.WriteLine("***********************");
                    Console.WriteLine("Please Input Valid Key");
                    Console.WriteLine("***********************");
                }
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                switch (menuChoice)
                {
                    case 'a':
                        finchRobotConnected = DisplayConnectFinchRobot(finchRobot);

                        break;
                    case 'b':
                        if (finchRobotConnected)
                        {
                            DisplayTalentShow(finchRobot);

                            DisplayMainMenuPrompt();
                        }
                        else
                        {
                            Console.WriteLine("Finch Robot not Connected. Return to the Main Menu");

                            DisplayMainMenuPrompt();
                        }

                        break;
                    case 'c':

                        if (finchRobotConnected)
                        {
                            DisplayDataRecorder(finchRobot);

                            DisplayMainMenuPrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Sorry data can't be Recorded");

                            DisplayMainMenuPrompt();
                        }

                        break;

                    case 'd':
                        if (finchRobotConnected)
                        {
                            Console.WriteLine("Alarm Sound");
                            Console.ReadKey();

                            DisplayAlarmSystem(finchRobot);

                            DisplayMainMenuPrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Alarm Not Sounding");
                            Console.WriteLine("Not working.");
                            Console.WriteLine("Check Connection to the Finch Robot.");
                            Console.ReadKey();

                            DisplayMainMenuPrompt();
                        }

                        break;
                    case 'e':
                        if (finchRobotConnected)
                        {
                            DisplayUserProgramming(finchRobot);
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("User Programming Not Connected to Finch Robot");

                            DisplayMainMenuPrompt();
                        }
                        break;
                    case 'f':
                        if (finchRobotConnected)
                        {
                            DisplayDisconnectFinchRobot(finchRobot);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Finch Robot Disconnected");

                            DisplayMainMenuPrompt();
                        }

                        break;
                    case 'q':
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("\t\t *************************");
                        Console.WriteLine("\t\t Please Enter Menu Choice");
                        Console.WriteLine("\t\t *************************");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }
        #endregion

        #region User Programming

        /// <summary>
        /// Choice (E)
        /// <summary>
        private static void DisplayUserProgramming(Finch finchRobot)
        {
            //string menuChoice;
            bool quitApplication = false;
            //bool finchRobotConnected = false;
            char menuChoice;

            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("Main Menu For User Programming");

                // get menu choice

                Console.WriteLine("a) Set Command Perameters");
                Console.WriteLine("b) Add Commands");
                Console.WriteLine("c) View Commands");
                Console.WriteLine("d) Execute Commands");
                Console.WriteLine("e) Save Commands to text file");
                Console.WriteLine("f) Store data in File");
                Console.WriteLine("q) Quit");
                Console.WriteLine("Enter Choice");

                Finch myFinch;
                myFinch = new Finch();
                bool invalidResponse = false;

                if (invalidResponse)
                {
                    Console.WriteLine("***********************");
                    Console.WriteLine("Please Input Valid Key");
                    // Console.WriteLine("Either a, b, c, d, Or q ");
                    Console.WriteLine("\tThank You");
                    Console.WriteLine("***********************");
                }

                ConsoleKeyInfo menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                switch (menuChoice)
                {
                    case 'a':

                        commandParameters = DisplayGetFinchCommandParameters();
                        Console.ReadKey();

                        break;
                    case 'b':

                        DisplayGetFinchCommands(commands);
                        Console.ReadKey();
                        break;
                    case 'c':

                        DisplayFinchCommands(commands);
                        Console.ReadKey();
                        break;
                    case 'd':
                        DisplayExecuteCommands(finchRobot, commands, commandParameters);
                        Console.ReadKey();
                        break;
                    case 'e':
                        DisplayWriteUserProgramingData(commands);
                        break;
                    case 'f':
                        commands = DisplayReadUserProgramingData();
                        break;

                    case 'q':
                        quitApplication = true;
                        break;
                    default:
                        Console.WriteLine("\t\t *************************");
                        Console.WriteLine("\t\t Please Enter Menu Choice");
                        Console.WriteLine("\t\t *************************");
                        DisplayMainMenuPrompt();
                        break;
                }
            } while (!quitApplication);

            finchRobot.disConnect();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Read User Programing Data
        /// <summary>
        private static List<Command> DisplayReadUserProgramingData()
        {
            string dataPath = @"Data\data.txt";
            List<Command> commands = new List<Command>();
            //List<string> commandsString = new List<string>();
            string[] commandsString;

            DisplayScreenHeader("Read Commands from data Files");

            Console.WriteLine("Ready to read commands from the data file.");
            Console.WriteLine();

            commandsString = File.ReadAllLines(dataPath);

            Command command;
            foreach (string commandString in commandsString)
            {
                Enum.TryParse(commandString, out command);
                commands.Add(command);
            }

            Console.WriteLine();
            Console.WriteLine("Commands  read commands from the data file complete");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Write User  Programing Data
        /// <summary>
        static void DisplayWriteUserProgramingData(List<Command> commands)
        {
            string dataPath = @"Data\data.txt";
            List<string> commandsString = new List<string>();

            DisplayScreenHeader("Write Commands to Data File");

            // create a list of command strings

            foreach (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }
            Console.WriteLine("Ready to write to the data File");
            DisplayContinuePrompt();

            File.WriteAllLines(dataPath, commandsString.ToArray());

            Console.WriteLine();
            Console.WriteLine("Commands written to the data file");


            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display Execute Commands
        /// <summary>
        static void DisplayExecuteCommands(Finch finchRobot, List<Command> commands,
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMillSeconds = commandParameters.waitSeconds * 1000;

            DisplayScreenHeader("Execute Finch Commands");


            // info and pause
            DisplayContinuePrompt();
            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;
                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.STOPMOTORS:
                        break;
                    case Command.WAIT:
                        finchRobot.wait(waitMillSeconds);
                        break;
                    case Command.DONE:
                        break;
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case Command.TURNLEFT:
                        finchRobot.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case Command.LEDOFF:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    default:
                        Console.WriteLine("\t\t *************************");
                        Console.WriteLine("\t\t Please Enter Menu Choice");
                        Console.WriteLine("\t\t *************************");
                        DisplayMainMenuPrompt();
                        break;
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display Finch Command
        /// <summary>
        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Display Finch Commands");

            do
            {
                foreach (Command command in commands)
                {
                    Console.WriteLine(command);

                    Console.ReadLine();

                    DisplayContinuePrompt();
                }
            } while (true);

        }

        /// <summary>
        /// Display Get Finch Commands
        /// <summary>
        static void DisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;
            string userResponse;

            DisplayScreenHeader("Finch Robot Commands");

            while (command != Command.DONE)
            {
                Console.WriteLine("Enter Command");
                userResponse = Console.ReadLine().ToUpper();
                // Enum.TryParse(userResponse, out command);
                bool validResponse = Enum.TryParse(userResponse, out command);

                // todo --Echo Commands

                commands.Add(command);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display Get Finch Command Parameters
        /// <summary>
        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetFinchCommandParameters()
        {
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            string userResponse = null;


            DisplayScreenHeader("Command Parameters");

            // todo - Validate three integers from user!!

            do
            {
                Console.WriteLine("Enter Motor Speed[1-255]:");
                int.TryParse(userResponse, out commandParameters.motorSpeed);


                Console.WriteLine("Enter Led Brightness[1-255]:");
                int.TryParse(userResponse, out commandParameters.ledBrightness);


                Console.WriteLine("Enter Seconds to Wait:");
                int.TryParse(userResponse, out commandParameters.waitSeconds);


                Console.WriteLine(" Sorry userResponse Not Valid");

                Console.ReadKey();
                DisplayContinuePrompt();

                return commandParameters;

            } while (true);

        }

        #endregion

        #region Alarm System
        /// <summary>
        /// Choice (D)
        /// <summary>
        static void DisplayAlarmSystem(Finch finchRobot)
        {
            // todo - Validate !!
            string alarmType;
            int maxSeconds;
            double threshold;
            bool thresholdExceeded;

            DisplayScreenHeader("Alarm System");
            do
            {
                alarmType = DisplayGetAlarmType();
                maxSeconds = DisplayGetMaxSeconds();
                threshold = DisplayGetThreshold(finchRobot, alarmType);

                //todo - warn user and pause
                thresholdExceeded = MonitorCurrentLightLevel(finchRobot, threshold, maxSeconds);

                if (thresholdExceeded)
                {
                    if (alarmType == "light")
                    {
                        Console.WriteLine("Maximum Light Level Exceeded");
                    }
                    else
                    {
                        Console.WriteLine(" Maximun Temperature Level Exceeded");
                    }
                }
                else
                {
                    Console.WriteLine(" Maximum Monitoring Time Exceeded");
                }

                DisplayContinuePrompt();
            }
            while (thresholdExceeded);
            {
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        ///  Monitor Current Light Level
        /// </summary>
        static bool MonitorCurrentLightLevel(Finch finchRobot, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            int currentLightLevel;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                currentLightLevel = finchRobot.getLeftLightSensor();

                DisplayScreenHeader("Monitoring Light Levels");
                Console.WriteLine($"Maximum Light Level: {threshold}");
                Console.WriteLine($"Current Light Level: {currentLightLevel}");
                DisplayContinuePrompt();

                if (currentLightLevel > threshold) thresholdExceeded = true;
                {
                    thresholdExceeded = true;
                }
                finchRobot.wait(500);
                seconds += 0.5;
            }
            return thresholdExceeded;
        }

        /// <summary>
        ///  Get Max Seconds
        /// </summary>
        static int DisplayGetMaxSeconds()
        {
            bool validResponse = true;
            string userResponse;
            int maxSeconds;

            do
            {
                Console.Write("Seconds to monotor");
                userResponse = Console.ReadLine();
                if (!int.TryParse(userResponse, out maxSeconds))
                {
                    //warning
                    validResponse = false;
                    Console.WriteLine("Sorry Can't Monitor Seconds");
                }
                return maxSeconds;

            } while (!validResponse);

        }

        /// <summary>
        ///  Get Alarm Type
        /// </summary>
        static string DisplayGetAlarmType()
        {
            string userResponse;
            bool validResponse = true;
            double alarmType;
            //bool quitApplication = false;
            //bool quitApplication = false;


            do
            {
                Console.Write("Alarm type [light, temperature, or quit to exit application]");
                userResponse = Console.ReadLine();
                if (!double.TryParse(userResponse, out alarmType))
                {
                    validResponse = false;
                    //quitApplication = true;
                }

                //todo - Change for validation
                return Console.ReadLine();

            } while (!validResponse);

        }

        /// <summary>
        /// Get Threshold
        /// </summary>
        static double DisplayGetThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;

            DisplayScreenHeader(" Threshold Value");

            switch (alarmType)
            {
                case "light":
                    Console.WriteLine($"Current Light Level: {finchRobot.getLeftLightSensor()}");
                    Console.Write("Enter Maximum Light Level [0- 255]:");
                    threshold = double.Parse(Console.ReadLine());
                    break;

                case "temperature":
                    Console.WriteLine($"Current Temperature Level: {finchRobot.getTemperature()}");
                    Console.WriteLine("Enter Maximum Temperature Level [35-100]:");
                    threshold = double.Parse(Console.ReadLine());
                    break;
                case "quit":
                    Console.WriteLine("Quiting Application");
                    Console.WriteLine();
                    threshold = double.Parse(Console.ReadLine());
                    break;

                default:
                    Console.WriteLine("\t\t *************************");
                    Console.WriteLine("\t\t Please Enter Light or Temerature");
                    Console.WriteLine("\t\t *************************");
                    DisplayContinuePrompt();
                    break;
            }
            DisplayContinuePrompt();

            return threshold;
        }
        #endregion

        #region Data Recorder
        /// <summary>
        ///   Coice (C)
        /// </summary>
        static void DisplayDataRecorder(Finch finchRobot)
        {
            double datapointFrequency;
            int numberOfDataPoints;
            bool validResponse = true;

            do
            {
                DisplayScreenHeader("Data Recorder");

                //
                // tell user what happening
                //
                datapointFrequency = DoubleDisplayGetDataPointFrequency();
                numberOfDataPoints = DisplayGetNumberOfDataPoints();

                //
                //  instalating (create) the array 
                //
                double[] temperatures = new double[numberOfDataPoints];

                DispalyGetData(numberOfDataPoints, datapointFrequency, temperatures, finchRobot);
                DisplayData(temperatures);
            }
            while (!validResponse);

            Console.WriteLine("Sorry not Connected");
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get Number Of Data Points
        /// <summary>
        static int DisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            bool validResponse = true;

            DisplayScreenHeader("Number of Data Points");
            do
            {
                Console.Write("Enter Number Of Data Points");
                int.TryParse(Console.ReadLine(), out numberOfDataPoints);

                DisplayContinuePrompt();

                return numberOfDataPoints;

            }
            while (!validResponse);

        }

        /// <summary>
        /// Data 
        /// <summary>
        static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Temperature Data");
            bool finchRobotConnected = true;
            do
            {
                for (int index = 0; index < temperatures.Length; index++)
                {
                    Console.WriteLine($" Temperature{index + 1}:{temperatures[index]}");

                    DisplayContinuePrompt();
                }
            }
            while (!finchRobotConnected);

            Console.WriteLine();
            Console.WriteLine("Sorry no Data");
            Console.ReadLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get Data 
        /// <summary>
        static void DispalyGetData(int numberOfDataPoints, double dataPointFrequency, double[] temperatures, Finch finchRobot)
        {
            DisplayScreenHeader("Get Temperature Data ");

            //
            // provide the user info and prompt
            //

            bool finchRobotConnected = true;
            do
            {
                for (int index = 0; index < numberOfDataPoints; index++)
                {
                    //
                    // record data
                    //
                    temperatures[index] = finchRobot.getTemperature();
                    int milliseconds = (int)(dataPointFrequency * 1000);
                    finchRobot.wait(milliseconds);

                    //
                    //echo
                    //
                    Console.WriteLine($" Temperature{ index + 1}: { temperatures[index]}");
                }
                DisplayMainMenuPrompt();
            }
            while (!finchRobotConnected);

            Console.WriteLine();

        }

        /// <summary>
        /// Get Data Point Frequency
        /// <summary>
        static double DoubleDisplayGetDataPointFrequency()
        {
            do
            {
                double datapointFrequency;
                string userResponse;

                DisplayScreenHeader("Data Point Frequency");

                Console.Write("Enter Data Point Frequency");
                userResponse = Console.ReadLine();
                double.TryParse(userResponse, out datapointFrequency);
                //double.TryParse(userResponse, out datapointFrequency);

                DisplayContinuePrompt();

                return datapointFrequency;
            } while (false);

        }
        #endregion

        #region Talent Show
        /// <summary>
        /// Choice (B)
        /// <summary>
        static void DisplayTalentShow(Finch finchRobot)
        {
            DisplayScreenHeader("Talent Show");

            Console.WriteLine("Finch Robot is ready to show you their talent");

            //
            // create (instantiate) a new finch object
            //
            Finch myFinch;
            myFinch = new Finch();

            //
            // connect to the finch robot using the finch method
            //
            myFinch.connect();

            DisplayContinuePrompt();

            //
            // Start Up 
            //
            myFinch.setLED(255, 200, 100);
            myFinch.wait(500);
            myFinch.setLED(150, 10, 255);
            myFinch.wait(500);
            myFinch.setLED(250, 20, 40);
            myFinch.wait(500);
            myFinch.noteOn(500);
            myFinch.wait(200);
            myFinch.noteOff();

            for (int i = 0; i < 3; i++)
            {
                //light
                myFinch.setLED(50, 50, 200);
                myFinch.wait(1000);
                myFinch.setLED(0, 0, 0);
                myFinch.wait(500);

                for (int j = 0; j < 200; j = j + 10)
                {
                    //sound
                    myFinch.noteOn(j);
                    myFinch.wait(500);
                    myFinch.noteOff();
                    myFinch.wait(500);
                    //light
                    myFinch.setLED(255, 200, 100);
                    myFinch.wait(500);
                    myFinch.setLED(150, 10, 255);
                    myFinch.setLED(200, 100, 50);
                    //move                       
                    myFinch.setMotors(100, 100);
                    myFinch.wait(1000);
                    myFinch.setMotors(200, 50);
                    myFinch.wait(1000);
                    myFinch.setMotors(50, 200);
                    myFinch.wait(1000);
                    myFinch.setMotors(-100, -100);
                    myFinch.wait(1000);
                    myFinch.setMotors(-200, -50);
                    myFinch.wait(1000);
                    myFinch.setMotors(-50, -200);
                    myFinch.wait(1000);
                    // off
                    myFinch.setMotors(0, 0);
                    myFinch.setLED(0, 0, 0);

                    Console.WriteLine();
                    DisplayContinuePrompt();
                }
            }

            //
            // disconnect from the finch robot using the finch method
            //
            myFinch.disConnect();
            DisplayContinuePrompt();
        }
        #endregion

        #region DisConnect Finch Robot            
        /// <summary>
        /// Choice (F)
        /// <summary>
        static void DisplayDisconnectFinchRobot(object finchRobot)
        {
            DisplayScreenHeader("Disconnect Finch Robot");
        }
        #endregion

        #region Connect Finch Robot

        /// <summary>
        /// Choice (A)
        /// <summary>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {


            bool finchRobotConnected = false;

            DisplayScreenHeader("Connect to Finch Robot");

            Console.WriteLine("Ready to connect to the finch robot. Be sure to connect the USB to the Robot and Computer");

            DisplayContinuePrompt();

            finchRobotConnected = finchRobot.connect();

            if (finchRobotConnected)
            {
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(15000);
                finchRobot.wait(1000);
                finchRobot.noteOff();

                Console.WriteLine();
                Console.WriteLine("Finch Robot is now Connected");

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Unable to Connect to the Finch Robot");
            }

            return finchRobotConnected;
        }
        #endregion

        #region HELPER METHODS
        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// display Main Menu prompt
        /// </summary>
        static void DisplayMainMenuPrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key return to Main Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }
        #endregion
    }
}