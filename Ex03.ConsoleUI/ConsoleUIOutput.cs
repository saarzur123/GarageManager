using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public class ConsoleUIOutput
    {
        private readonly List<string> r_DetailTypes = new List<string>();

        public ConsoleUIOutput()
        {
            const string k_OwnerDetails = "Owner details: ";
            const string k_GeneralDetails = "General details: ";
            const string k_VehicleDetails = "Vehicle details: ";
            const string k_EngineDetails = "Engine details: ";
            r_DetailTypes.Add(k_OwnerDetails);
            r_DetailTypes.Add(k_GeneralDetails);
            r_DetailTypes.Add(k_VehicleDetails);
            r_DetailTypes.Add(k_EngineDetails);
        }

        internal void PrintGarageMenu()
        {
            Console.WriteLine(@"Welcome to garage operations! Choose from the following:
1. Insert a new vehicle to the garage.
2. Present license number of all garage vehicles.
3. Change vehicle status inside the garage.
4. Fill a vehicle's tire pressure to maximum.
5. Fill fueled vehicle with gas.
6. Recharge electric vehicle.
7. Present full vehicle data by license number.
8. Exit garage manager");
        }

        internal void PrintRequestInputAccordingToList(List<string> i_PresentedList)
        {
            const string k_Message = "one of the following";
            PrintRequestInput(k_Message, eTypeInput.ObjectType);
            foreach(string item in i_PresentedList)
            {
                Console.WriteLine(item);
            }
        }

        internal void PrintRequestYesOrNoInput(string i_RequestMessage)
        {
            string message = string.Format("{0} Insert 'Y' for yes 'N' for no", i_RequestMessage);
            Console.WriteLine(message);
        }

        internal void PrintListByProperty(List<string> i_DetailsList, string i_DetailType)
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine(i_DetailType);
            foreach (string detail in i_DetailsList)
            {
                Console.WriteLine(detail);
            }
        }

        internal void PrintListOfListsVehicleDetails(List<List<StringBuilder>> i_FullGarageDetails)
        {
            int numberOfDetailTypes = i_FullGarageDetails.Count;
            for (int i = 0; i < numberOfDetailTypes; ++i)
            {
                Console.WriteLine(r_DetailTypes[i]);
                foreach(StringBuilder detailSB in i_FullGarageDetails[i])
                {
                    Console.WriteLine(detailSB);
                }

                Console.Write(Environment.NewLine);
            }
        }

        internal void PrintRequestGasType()
        {
            Console.WriteLine(@"Please enter vehicle's gas type:
1. Soler
2. Octan98
3. Octan96
4. Octan95");
        }

        internal void PrintRequestInput(string i_RequestedInput, eTypeInput i_InputType)
        {
            StringBuilder requestOutputMessage = new StringBuilder("Please enter ");
            requestOutputMessage.Append(i_RequestedInput);
            switch (i_InputType)
            {
                case eTypeInput.Name:
                    requestOutputMessage.Append(" name");
                    break;
                case eTypeInput.Manufacturer:
                    requestOutputMessage.Append(" manufacturer");
                    break;
                case eTypeInput.OwnerNumber:
                case eTypeInput.LicenseNumber:
                case eTypeInput.Number:
                    requestOutputMessage.Append(" number");
                    break;
                case eTypeInput.ObjectType:
                    requestOutputMessage.Append(" type");
                    break;
                default:
                    break;
            }

            Console.WriteLine(requestOutputMessage.ToString());
        }

        internal void PrintInvalidParsedType()
        {
            Console.WriteLine("Invalid format inserted, Please try again");
        }

        internal void PrintInvalidRange(float i_MinValue, float i_MaxValue)
        {
            string invalidRangeStr = string.Format(
                @"Invalid range! 
Please enter a number between {0} to {1}",
                i_MinValue,
                i_MaxValue);
            Console.WriteLine(invalidRangeStr);
        }

        internal void PrintInvalidInput(string i_InvalidType)
        {
            string invalidMessage = string.Format("Invalid {0} inserted! Please try again", i_InvalidType);
            Console.WriteLine(invalidMessage);
        }

        internal void PrintMessageWithDetail(string i_Detail, string i_Message)
        {
            string messageToPrint = string.Format("{0}, {1}", i_Detail, i_Message);
            Console.WriteLine(messageToPrint);
        }

        internal void PrintNoneToShowMessage(string i_Property)
        {
            string messageToPrint = string.Format("No {0} to show!", i_Property);
            Console.WriteLine(messageToPrint);
        }

        internal void PrintPauseMessageAndPause()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        internal void PrintGeneralMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        internal void RequestInput(string i_Message)
        {
            StringBuilder requestOutputMessage = new StringBuilder("Please enter ");
            requestOutputMessage.Append(i_Message);
            Console.WriteLine(requestOutputMessage.ToString());
        }
    }
}
