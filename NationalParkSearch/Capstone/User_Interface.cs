using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone
{
    //TODO: Class is a little too long for my taste. If time permits, split into a separate class that handles user input, 
    //and this one simply displays Console Messages?
    public class User_Interface
    {
        const string Command_GetParks = "1";
        const string Command_Quit = "9";
        const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NationalParkReservation;Integrated Security=True";

        ParkDAL parkDAL = new ParkDAL(DatabaseConnectionString);
        CampgroundDAL campgroundDAL = new CampgroundDAL(DatabaseConnectionString);
        SiteDAL siteDAL = new SiteDAL(DatabaseConnectionString);
        ReservationDAL reservationDAL = new ReservationDAL(DatabaseConnectionString);
        Util util = new Util();

        public void RunUI()
        {
            PrintHeader();

            bool done = false;
            string choice = "";

            while (!done)
            {
                PrintMainMenu();

                choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        List<Park> parks = GetAllParks();
                        ParksMenu(parks);
                        break;

                    case "q":
                        done = true;
                        break;

                    case "Q":
                        done = true;
                        break;

                    default:
                        Console.WriteLine("Please make a valid selection");
                        break;
                }
            }
        }

        public void PrintHeader()
        {
            Console.WriteLine(@"     __        __   _                            _          _   _                               ");
            Console.WriteLine(@"     \ \      / /__| | ___ ___  _ __ ___   ___  | |_ ___   | |_| |__   ___                      ");
            Console.WriteLine(@"      \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \ | __/ _ \  | __| '_ \ / _ \                     ");
            Console.WriteLine(@"       \ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) | | |_| | | |  __/_ _ _                ");
            Console.WriteLine(@"      _ \_/\_/ \___|_|\___\___/|_| |_| |_|\___|__\__\___/   \__|_| |_|\___(_|_|_)");
            Console.WriteLine(@"     | \ | | __ _| |_(_) ___  _ __   __ _| | |  _ \ __ _ _ __| | __                             ");
            Console.WriteLine(@"     |  \| |/ _` | __| |/ _ \| '_ \ / _` | | | |_) / _` | '__| |/ /");
            Console.WriteLine(@"     | |\  | (_| | |_| | (_) | | | | (_| | | |  __/ (_| | |  |   <                              ");
            Console.WriteLine(@"     |_|_\_|\__,_|\__|_|\___/|_| |_|\__,_|_| |_|   \__,_|_|  |_|\_\           _");
            Console.WriteLine(@"     |  _ \ ___  ___  ___ _ ____   ____ _| |_(_) ___  _ __   / ___| _   _ ___| |_ ___ _ __ ___  ");
            Console.WriteLine(@"     | |_) / _ \/ __|/ _ \ '__\ \ / / _` | __| |/ _ \| '_ \  \___ \| | | / __| __/ _ \ '_ ` _ \ ");
            Console.WriteLine(@"     |  _ <  __/\__ \  __/ |   \ V / (_| | |_| | (_) | | | |  ___) | |_| \__ \ ||  __/ | | | | |");
            Console.WriteLine(@"     |_| \_\___||___/\___|_|    \_/ \__,_|\__|_|\___/|_| |_| |____/ \__, |___/\__\___|_| |_| |_|");
            Console.WriteLine(@"                                                                    |___/                       ");
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("\n\n-------Main-Menu-------");
            Console.WriteLine("\nPlease select a command:");
            Console.WriteLine("1) View Parks");
            Console.WriteLine("Q) Quit");
            Console.WriteLine();
            Console.Write("Select a Command: ");
        }

        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            ParkDAL parkDAL = new ParkDAL(DatabaseConnectionString);
            parks = parkDAL.GetAllParks();
            int displayPosition = 1;
            //Account for no parks in system. For some reason.
            if (parks.Count == 0)
            {
                Console.WriteLine("It appears we have no parks in our system. Well isn't this just bloody pointless.");
                return parks;
            }
            Console.WriteLine("Select a Park for Further Details:");
            foreach (Park park in parks)
            {
                Console.WriteLine(displayPosition + ")  " + park.Name);
                displayPosition++;
            }
            Console.WriteLine("Q)  Return to previous screen");
            Console.WriteLine();

            return parks;
        }

        public void ParksMenu(List<Park> parks)
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    Console.Write("Your choice: ");
                    string userInput = Console.ReadLine();
                    Console.WriteLine();

                    if (userInput == "q" || userInput == "Q")
                    {
                        return;
                    }
                    else if (int.TryParse(userInput, out int convertedInput))
                    {
                        int selectedParkID = parks[convertedInput - 1].ParkID;

                        Console.WriteLine();
                        DisplayParkInformation(selectedParkID);
                        SelectedParkMenu(selectedParkID);
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Please make a valid selection");
                        done = false;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Sorry, we couldn't find a park with that number. Please try again.");
                    done = false;
                }
            }
        }

        public void SelectedParkMenu(int parkID)
        {
            Console.WriteLine("Select a Command:");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("3) See Reservations for the next 30 days");
            Console.WriteLine("4) Return to Previous Screen");
            Console.WriteLine();

            bool done = false;
            while (!done)
            {
                try
                {
                    int.TryParse(Console.ReadLine(), out int userInput);
                    Console.WriteLine();

                    switch (userInput)
                    {
                        case 1:
                            List<Campground> campgrounds = DisplayCampgrounds(parkID);
                            CampGroundMenu(campgrounds);
                            done = true;
                            break;

                        case 2:
                            ParkReservationSearchMenu(parkID);
                            done = true;
                            break;

                        case 3:
                            GetReservationsForNext30DaysAtPark(parkID);
                            done = true;
                            break;

                        case 4:
                            break;

                        default:
                            Console.WriteLine("Please make a valid selection");
                            break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void DisplayParkInformation(int parkID)
        {
            Park park = parkDAL.GetPark(parkID);
            int visitorNumber = park.AnnualVisitors;
            string visitorString = visitorNumber.ToString("#,##0");
            int areaNumber = park.Area;
            string areaString = areaNumber.ToString("#,##0");

            Console.WriteLine(park.Name + " National Park");
            Console.WriteLine("Location:".PadRight(20) + park.Location);
            Console.WriteLine("Established:".PadRight(20) + park.EstablishDate.ToString("MM/dd/yyyy"));
            Console.WriteLine("Area:".PadRight(20) + areaString + " acres");
            Console.WriteLine("Annual Visitors:".PadRight(20) + visitorString);
            Console.WriteLine();
            Console.WriteLine(park.Description + "\n");
        }

        public List<Campground> DisplayCampgrounds(int parkID)
        {
            List<Campground> campgrounds = campgroundDAL.GetAllCampgroundsAtPark(parkID);
            int displayPosition = 1;
            Park currentPark = parkDAL.GetPark(parkID);
            string currentParkName = currentPark.Name;

            Console.WriteLine(currentParkName + " National Park Campgrounds");
            Console.WriteLine();
            Console.WriteLine("       " + "Name".PadRight(25) + "Open".PadRight(13) + "Close".PadRight(13) + "Daily Fee");
            foreach (Campground campground in campgrounds)
            {
                string positionString = Convert.ToString(displayPosition);
                Console.WriteLine("#" + positionString.PadRight(6) + campground.Name.PadRight(25) + util.GetMonth(campground.FromMonth).PadRight(13) + util.GetMonth(campground.ToMonth).PadRight(13) + string.Format("{0:C2}", campground.DailyFee));
                displayPosition++;
            }
            Console.WriteLine();

            return campgrounds;
        }

        public void CampGroundMenu(List<Campground> campgrounds)
        {
            Console.WriteLine("Select a Command:");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            Console.WriteLine();

            try
            {
                int.TryParse(Console.ReadLine(), out int userInput);
                Console.WriteLine();

                switch (userInput)
                {
                    case 1:
                        CampgroundReservationSearchMenu(campgrounds);
                        break;

                    case 2:
                        break;

                    default:
                        Console.WriteLine("Please make a valid selection");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CampgroundReservationSearchMenu(List<Campground> campgrounds)
        {
            bool done = false;
            while (!done)
            {
                Console.Write("Please enter the number of the Campground you wish to search (enter [Q] to cancel)? ");
                string userInput = Console.ReadLine();
                if (userInput == "Q" || userInput == "q")
                {
                    done = true;
                }
                else
                {
                    try
                    {
                        int.TryParse(userInput, out int numberInput);
                        int campgroundID = campgrounds[numberInput - 1].CampID;

                        Console.WriteLine("What is the arrival date (please format like: yyyy/mm/dd)?");
                        DateTime.TryParse(Console.ReadLine(), out DateTime fromDate);
                        Console.WriteLine("What is the departure date (please format like: yyyy/mm/dd)?");
                        DateTime.TryParse(Console.ReadLine(), out DateTime toDate);
                        List<Site> availableSites = new List<Site>();
                        // Prompt for running adcanced search and get user input
                        Console.WriteLine("Would you like to run an advanced search? (Y/N)");
                        string advancedSearch = Console.ReadLine();
                        if (advancedSearch[0] == 'Y' || advancedSearch[0] == 'y')
                        {
                            Console.WriteLine("Great! Let's get started....");
                            Console.WriteLine();
                            Console.WriteLine("Does your campsite need to be wheelchair accessible? (Y/N) ");
                            string accessiblString = Console.ReadLine();
                            bool accessible = accessiblString[0] == 'Y' ? true : false;

                            Console.WriteLine("Does the campsite need utilities? (Y/N) ");
                            string utilitiesString = Console.ReadLine();
                            bool utilities = utilitiesString[0] == 'Y' ? true : false;

                            Console.WriteLine("How many people will be joining you? ");
                            int.TryParse(Console.ReadLine(), out int occupants);

                            Console.WriteLine("What is the length of your RV? ");
                            int.TryParse(Console.ReadLine(), out int rvLength);

                            //Get results
                            availableSites = DisplayAdvancedAvailableCampsitesAtCampground(campgroundID, fromDate, toDate, accessible, utilities, rvLength, occupants);
                        }
                        else
                        {
                            availableSites = DisplayAvailableCampsitesAtCampground(campgroundID, fromDate, toDate);
                        }
                        if (availableSites.Count == 0)
                        {
                            continue;
                        }
                        done = true;

                        MakeReservationMenu(fromDate, toDate, availableSites);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public List<Site> DisplayAvailableCampsitesAtCampground(int campgroundID, DateTime fromDate, DateTime toDate)
        {
            List<Site> availableSites = new List<Site>();
            List<string> availableCampsites = siteDAL.GetAvailableSitesAtCampground(fromDate, toDate, campgroundID, ref availableSites);

            //Check if the List is empty and display message that no results were found if true
            if (availableSites.Count == 0)
            {
                Console.WriteLine("Sorry. It appears we have no campsites available that meet your criteria. Please try again.");
                Console.WriteLine();
                return availableSites;
            }
            Console.WriteLine("Site No.".PadRight(12) + "Max Occup.".PadRight(14) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(17) + "Utilities?".PadRight(11) + "Total Cost of Stay");
            foreach (string site in availableCampsites)
            {
                Console.WriteLine(site.ToString());
            }
            return availableSites;
        }

        public List<Site> DisplayAvailableCampsitesAtPark(int parkID, DateTime fromDate, DateTime toDate)
        {
            List<Site> availableSites = new List<Site>();
            List<string> availableCampsites = siteDAL.GetAvailableSitesAtPark(fromDate, toDate, parkID, ref availableSites);
            // Check if the List is empty and display message that no results were found if true
            if (availableSites.Count == 0)
            {
                Console.WriteLine("Sorry. It appears we have no campsites available that meet your criteria. Please try again.");
                Console.WriteLine();
                return availableSites;
            }
            Console.WriteLine("Campground Name".PadRight(35) + "Site No.".PadRight(12) + "Max Occup.".PadRight(14) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(17) + "Utilities?".PadRight(11) + "Total Cost of Stay");
            foreach (string site in availableCampsites)
            {
                Console.WriteLine(site.ToString());
            }

            return availableSites;
        }

        public List<Site> DisplayAdvancedAvailableCampsitesAtCampground(int campgroundID, DateTime fromDate, DateTime toDate, bool accessible, bool utilities, int rvLength, int occupants)
        {
            List<Site> availableSites = new List<Site>();
            List<string> availableCampsites = siteDAL.AdvancedSearchGetAvailableSitesAtCampground(fromDate, toDate, campgroundID, accessible, utilities, occupants, rvLength, ref availableSites);
            //Check if the List is empty and display message that no results were found if true
            if (availableSites.Count == 0)
            {
                Console.WriteLine("Sorry. It appears we have no campsites available that meet your criteria. Please try again.");
                return availableSites;
            }
            Console.WriteLine();
            Console.WriteLine("Campground Name".PadRight(35) + "Site No.".PadRight(12) + "Max Occup.".PadRight(14) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(17) + "Utilities?".PadRight(11) + "Total Cost of Stay");
            foreach (string site in availableCampsites)
            {
                Console.WriteLine(site);
            }

            return availableSites;
        }

        public List<Site> DisplayAdvancedAvailableCampsitesAtPark(int parkID, DateTime fromDate, DateTime toDate, bool accessible, bool utilities, int rvLength, int occupants)
        {
            List<Site> availableSites = new List<Site>();
            // Check if the List is empty and display message that no results were found if true
            List<string> availableCampsites = siteDAL.AdvancedSearchGetAvailableSitesAtPark(fromDate, toDate, parkID, accessible, utilities, occupants, rvLength, ref availableSites);
            if (availableSites.Count == 0)
            {
                Console.WriteLine("Sorry. It appears we have no campsites available that meet your criteria. Please try again.");
                return availableSites;
            }

            foreach (string site in availableCampsites)
            {
                Console.WriteLine(site.ToString());
            }

            return availableSites;
        }

        public void ParkReservationSearchMenu(int parkID)
        {
            List<Site> availableSites = new List<Site>();

            Console.WriteLine("What is the arrival date (please format like: yyyy/mm/dd)?");
            DateTime.TryParse(Console.ReadLine(), out DateTime fromDate);
            Console.WriteLine("What is the departure date (please format like: yyyy/mm/dd)?");
            DateTime.TryParse(Console.ReadLine(), out DateTime toDate);
            Console.WriteLine("Would you like to run an advanced search (Y/N)");
            string advancedSearchString = Console.ReadLine();
            bool advancedSearch = advancedSearchString[0] == 'Y' || advancedSearchString[0] == 'y' ? true : false;
            if (advancedSearch)
            {
                Console.WriteLine("Great! Let's get started....");
                Console.WriteLine();
                Console.WriteLine("Does your campsite need to be wheelchair accessible? (Y/N) ");
                string accessiblString = Console.ReadLine();
                bool accessible = accessiblString[0] == 'y' || accessiblString[0] == 'Y' ? true : false;

                Console.WriteLine("Does the campsite need utilities? (Y/N) ");
                string utilitiesString = Console.ReadLine();
                bool utilities = utilitiesString[0] == 'y' || utilitiesString[0] == 'Y' ? true : false;

                Console.WriteLine("How many people will be joining you? ");
                int.TryParse(Console.ReadLine(), out int occupants);

                Console.WriteLine("What is the length of your RV? ");
                int.TryParse(Console.ReadLine(), out int rvLength);

                //Get results
                availableSites = DisplayAdvancedAvailableCampsitesAtCampground(parkID, fromDate, toDate, accessible, utilities, rvLength, occupants);
            }
            else
            {
                availableSites = DisplayAvailableCampsitesAtPark(parkID, fromDate, toDate);
            }

            int campID = MakeReservationFromPark(parkID);
            List<Site> availableSitesAtParkID = new List<Site>();

            //Make new list of sites at the specified campID
            for (int i = 0; i < availableSites.Count; i++)
            {
                if (campID == availableSites[i].CampgroundID)
                {
                    availableSitesAtParkID.Add(availableSites[i]);
                }
            }
            MakeReservationMenu(fromDate, toDate, availableSitesAtParkID);
        }

        public int MakeReservationFromPark(int parkID)
        {
            try
            {
                int campID = 0;
                bool foundCampgroundName = false;

                while (!foundCampgroundName)
                {
                    Console.WriteLine("Which Campground are you interested in (please type the name exactly as shown)? ");
                    string selectedCampgroundName = Console.ReadLine();
                    List<Campground> campgrounds = campgroundDAL.GetAllCampgroundsAtPark(parkID);
                    foreach (Campground campground in campgrounds)
                    {
                        if (selectedCampgroundName == campground.Name)
                        {
                            campID = campground.CampID;
                            foundCampgroundName = true;
                            break;
                        }
                    }
                    if (!foundCampgroundName)
                    {
                        Console.WriteLine();
                        Console.WriteLine("We could not find a campground with that name. Please try again.");
                        Console.WriteLine();
                    }
                }

                return campID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void MakeReservationMenu(DateTime fromDate, DateTime toDate, List<Site> availableSites)
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    Console.Write("Which site should be reserved (enter 0 to cancel)? ");
                    int.TryParse(Console.ReadLine(), out int siteNum);
                    bool validInput = false;
                    int siteID = 0;
                    foreach (Site site in availableSites)
                    {
                        if (siteNum == site.SiteNumber)
                        {
                            validInput = true;
                            siteID = site.SiteID;
                            break;
                        }
                    }
                    if (siteNum == 0)
                    {
                        done = true;
                    }
                    else if (validInput)
                    {
                        Reservation reservation = new Reservation();
                        reservation.FromDate = fromDate;
                        reservation.ToDate = toDate;
                        reservation.SiteID = siteID;
                        Console.Write("\nWhat is the name your reservation will be under? ");
                        string name = Console.ReadLine();
                        reservation.Name = name;
                        int newReservationID = reservationDAL.AddReservation(reservation);
                        Console.WriteLine($"The reservation has been made and the confirmation id is {newReservationID} ");
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("Please try again.");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void GetReservationsForNext30DaysAtPark(int parkID)
        {
            List<string> reservations = reservationDAL.GetReservationsInNext30Days(parkID);
            Console.WriteLine("");
            if (reservations.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("It appears as though we have no reservations for the next 30 days. So feel free to make a reservation now!");
                Console.WriteLine();
                return;
            }
            Console.WriteLine("Campground Name".PadRight(35) + "Reservation ID".PadRight(16) + "Site ID".PadRight(9) + "Reservation Name".PadRight(35) + "Arrival Date".PadRight(14) + "Departure Date");
            foreach (string reservation in reservations)
            {
                Console.WriteLine(reservation);
            }
        }
    }
}
