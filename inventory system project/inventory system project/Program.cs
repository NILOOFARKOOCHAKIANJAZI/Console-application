//using Bytescout.Spreadsheet;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Reflection;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
class InventoryManagementSys
{
    //Constants
    const int INVENTORYSIZE = 20;       //Maximum items in system
    const string SEPERATOR = "_________________________________________________________________\n";

    //Item Information Arrays
    static string[] itemName = new string[INVENTORYSIZE];               //Name
    static string[] itemDescription = new string[INVENTORYSIZE];        //Description
    static int[] itemQuantity = new int[INVENTORYSIZE];                 //Quantity
    static double[] itemPrice = new double[INVENTORYSIZE];              //Price
    static int[] itemID = new int[INVENTORYSIZE];                       //ID
    static int[] itemCategory = new int[INVENTORYSIZE];                 //Category
    static DateTime[] itemCreationDate = new DateTime[INVENTORYSIZE];   //Creation Date
    static DateTime[] itemUpdateDate = new DateTime[INVENTORYSIZE];     //Update Date

    //Category Names
    static string[] category = { "Mouse", "Keyboard", "Computer", "Laptop", "Speakers" };

    //Main Array Index
    static int itemIndex = 0;

    static Random random = new Random();

    public static void Main()
    {
        bool flag = true;
        int option;
        //DataLoad();       //Optional dataload method to load excel sheets into inventory
        while (flag)
        {
            DisplayMainMenu();      //Displays main menu
            while (true)
            {
                //Reads user input to navigate main menu
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", enter a number! try again: ");
                }
            }
            //Uses the input to send the program to the correct method
            switch (option)
            {
                //Create new item
                case 1:
                    Console.Clear();
                    CreateItem();
                    break;

                //Update existing item
                case 2:
                    Console.Clear();
                    UpdateItem();
                    break;

                //Deletes an existing item
                case 3:
                    Console.Clear();
                    DeleteItem();
                    break;

                //Generates all manner of reports
                case 4:
                    Console.Clear();
                    GenerateReport();
                    break;

                //Creates an item with random information
                case 5:
                    Console.Clear();
                    CreateRandomItem();
                    break;

                //Exits the program
                case 0:
                    flag = false;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Error");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", invalid number!\n");
                    Loading();
                    break;
            }
        }
    }

    //Displays the main menu
    public static void DisplayMainMenu()
    {
        //Title
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\t\tINVENTORY MANAGEMENT SYSTEM");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //Menu options
        Console.WriteLine("\t\tPlease select an option: ");
        Console.WriteLine("\t\t1. Create a New Entry");
        Console.WriteLine("\t\t2. Update Existing Entry");
        Console.WriteLine("\t\t3. Delete an Entry");
        Console.WriteLine("\t\t4. Generate a Report");
        Console.WriteLine("\t\t5. Create a Random Entry");
        Console.WriteLine("\t\t0. Exit\n");
        Console.WriteLine(SEPERATOR);

        //Asks the user for input
        Console.Write("Enter the number of your choice: ");
    }

    //Create new items
    public static void CreateItem()
    {
        //Item information local variables
        string ItemName = "";               //Name
        string ItemDescription = "";        //Description
        int ItemQuantity = 0;               //Quantity
        double ItemPrice = 0;               //Price
        int catTemp = 0;                    //Category

        //Title
        Console.Clear();
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\t\t\tCREATE NEW ENTRY");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //This loop accepts user input for the new item
        bool flag = true;
        while (flag)
        {
            //Displays all categories
            Console.WriteLine("Here are the categories:\n");
            for (int i = 0; i < category.Length; i++)
            {
                Console.Write($"{i + 1}) {category[i]}  ");
            }
            Console.WriteLine();
            Console.WriteLine(SEPERATOR);

            //Accepts user input in order to choose a category
            Console.Write("Choose the number of the category : ");
            while (true)
            {
                try
                {
                    catTemp = Convert.ToInt32(Console.ReadLine());
                    if (catTemp <= 0 || catTemp > category.Length)//Catches invalid numbers
                    {
                        Console.Write("Error, invalid number! choose number between 1 and {0}: ", category.Length);
                    }
                    else
                    {
                        break;
                    }
                    //break
                }
                //Catches null or string inputs
                catch
                {

                    Console.Write("Error, enter a number! try again: ");
                }
                
                
            }

            //Accepts user input for new item information
            Console.Write("\nEnter the details of the item: \n");

            //Input item name
            Console.Write("\nName: ");
            while (true)
            {
                try
                {
                    ItemName = Console.ReadLine();
                    ItemName = CleanString(ItemName);
                    //Catches null inputs
                    if (ItemName == "" || ItemName == " ")
                    {
                        Console.Write("Error, enter a name! try again: ");
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.Write("Error, enter a name! try again: ");
                }
            }

            //Input item description
            Console.Write("\nDescription: ");
            while (true)
            {
                try
                {
                    ItemDescription = Console.ReadLine();
                    ItemDescription = CleanString(ItemDescription);
                    //Catches null inputs
                    if (ItemDescription == "" || ItemDescription == " ")
                    {
                        Console.Write("Error, enter a description! try again: ");
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.Write("Error, enter a description! try again: ");
                }
            }

            //Input item quantity
            Console.Write("\nQuantity: ");
            while (true)
            {
                try
                {
                    ItemQuantity = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            //Input item price
            Console.Write("\nPrice: ");
            while (true)
            {
                try
                {
                    ItemPrice = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            Console.WriteLine(SEPERATOR);

            //Displays the information entered
            Console.Write("This is the information you have entered:\n");
            Console.WriteLine($"\nName:{ItemName}\tDescription:{ItemDescription}\tQuantity:{ItemQuantity}\tPrice:{ItemPrice:c2}\tCategory:{category[catTemp - 1]}\n");

            //This loop asks the user to confirm the information entered
            bool flag2 = true;
            while (flag2)
            {
                Console.Write("Is this information correct? Yes/No : ");
                string answer = "";

                //This loop catches null and invalid inputs
                while (true)
                {
                    try
                    {
                        answer = Console.ReadLine();
                        answer = CleanString(answer);
                        //Catches null inputs
                        if (answer == "" || answer == " ")
                        {
                            Console.Write("Error, enter yes or no! try again: ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.Write("Error, enter yes or no! try again: ");
                    }
                }

                //Confirms the information and continues the program
                if (answer == "yes")
                {
                    flag = false;
                    flag2 = false;
                }

                //Repeats the loop to input new information
                else if (answer == "no")
                {
                    Console.WriteLine();
                    flag = true;
                    flag2 = false;
                }

                //Catches invalid input
                else
                {
                    Console.Write("Error, enter yes or no! try again: ");
                }
            }
        }

        //Generates and displays a new ID for the item
        int newID = GenerateID();
        Console.Write($"This is the item ID: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{newID}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" . please remember that!");

        //Gets the current date and time
        DateTime currentDateAndTime = DateTime.Now;

        //Puts the information into parallel arrays
        itemName[itemIndex] = ItemName;                     //Name
        itemDescription[itemIndex] = ItemDescription;       //Description
        itemQuantity[itemIndex] = ItemQuantity;             //Quantity
        itemPrice[itemIndex] = ItemPrice;                   //Price
        itemID[itemIndex] = newID;                          //ID
        itemCategory[itemIndex] = catTemp;                  //Category
        itemCreationDate[itemIndex] = currentDateAndTime;   //Creation Date
        itemUpdateDate[itemIndex] = currentDateAndTime;     //Update Date

        //Increases the main index
        itemIndex++;

        //Clears the screen and return to main menu
        Loading();
    }

    //This method clears the screen and return to the main menu when any key is pressed
    //it is called at the end of every method
    public static void Loading()
    {
        //This int controls the time before the screen clears
        int milliseconds = 100;

        Console.WriteLine(SEPERATOR);
        Console.Write("Press any key to return to the main menu");
        Console.ReadKey();
        Console.Write("Returning to main menu");

        //This loop writes "....." before clearing the screen, it is only for aesthetics.
        for (int i = 0; i < 20; i++)
        {
            Console.Write(".");
            Thread.Sleep(milliseconds);
        }
        Console.Clear();
    }

    //This method cleans string variables by trimming spaces and converting to lowercase
    //It is called everytime the user is asked for a string input
    //This also limits the user to only be able to input lowercase letters.
    public static string CleanString(string dirtyString = "")
    {
        string cleanedString = System.Text.RegularExpressions.Regex.Replace(dirtyString, @"\s+", " ");
        cleanedString = cleanedString.Trim();
        cleanedString = cleanedString.ToLower();
        return cleanedString;
    }

    //Generate a unique random 4 digit ID 
    static int GenerateID()
    {
        //Local variables
        int id;

        do
        {
            id = random.Next(1000, 10000);
        } while (Array.IndexOf(itemID, id, 0, itemIndex) != -1);
        return id;
    }

    //Generates various inventory reports 
    static void GenerateReport()
    {
        //Local variables
        int response = 0;
        bool flag = true;

        //Title
        Console.Clear();
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\t\t\tGENERATE REPORT");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //Displays the reports menu
        Console.WriteLine("1) Report for all items    2) Search and report for one item    3)Report items with low quantity\n");
        Console.Write("Select the type of report you want to generate: ");

        //Accepts user input to choose which report to generate
        //Repeats in case of null or invalid input
        while (flag)
        {
            while (true)
            {
                try
                {
                    response = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }
            Console.WriteLine();

            //After accepting the user input, generate the selected report
            switch (response)
            {
                //Report all items
                case 1:
                    Console.WriteLine(SEPERATOR);
                    Console.WriteLine("All Items:\n");
                    for (int index = 0; index < itemIndex; index++)
                    {
                        Console.WriteLine($"ID: {itemID[index]}\t Name: {itemName[index]}\t Description: {itemDescription[index]}\t Quantity: {itemQuantity[index]}\t Price: {itemPrice[index]:c2}\t Category: {category[index]}\t Creation Date: {itemCreationDate[index]}\t Date of Update: {itemUpdateDate[index]}\n");
                    }
                    flag = false;
                    break;

                //Search for one item and display it
                case 2:
                    //Search
                    int index2 = SearchItem();
                    if (index2 == -1)
                    {
                        return;
                    }
                    //Display
                    Console.WriteLine();
                    Console.WriteLine($"ID: {itemID[index2]}\t Name: {itemName[index2]}\t Description: {itemDescription[index2]}\t Quantity: {itemQuantity[index2]}\t Price: {itemPrice[index2]:C2}\t Category: {category[index2]}\t Creation Date: {itemCreationDate[index2]}\t Date of Update: {itemUpdateDate[index2]}");
                    flag = false;
                    break;

                //Display all items that have less than a certain amount
                case 3:
                    DisplayLowQuantity();
                    flag = false;
                    break;

                //Catches invalid inputs and repeats the loop
                default:
                    Console.WriteLine("Error, invalid number! try again:");
                    break;
            }
        }
        //Clears screen and returns to main menu
        Loading();
    }

    //Asks the user to input an int, displays all items with a quantity lower than the input.
    public static void DisplayLowQuantity()
    {
        //Local variables
        int minQty = 0;         //The quantity to search by
        int itemsFound = 0;     //The number of items that the search found

        //Asks the user for input
        Console.Write("Enter the minimun Item Quantity to display: ");
        while (true)
        {
            try
            {
                minQty = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                break;
            }
            //Catches null or string inputs
            catch
            {
                Console.Write("Error, enter a number! try again: ");
            }
        }

        //Searches through all items and displays those with quantity lower than the input
        for (int i = 0; i < itemIndex; i++)
        {
            if (itemQuantity[i] <= minQty)
            {
                Console.WriteLine($"ID: {itemID[i]}\t Name: {itemName[i]}\t Description: {itemDescription[i]}\t Quantity: {itemQuantity[i]}\t Price: {itemPrice[i]:C2}\t Creation Date: {itemCreationDate[i]}\t Date of Update: {itemUpdateDate[i]}");
                Console.WriteLine();
                itemsFound++;
            }
        }

        //Displays the number of items found
        if (itemsFound > 0)
        {
            Console.WriteLine($"{itemsFound} items found");
        }
        else { Console.WriteLine("No items found"); }
    }

    //Update the information of an existing item
    static void UpdateItem()
    {
        //Item information local variables
        string updateItemName = "";             //Name
        string updateItemDescription = "";      //Description
        int updateItemQuantity = 0;             //Quantity
        double updateItemPrice = 0;             //Price

        //Loop handler
        bool flag = true;

        //Title
        Console.Clear();
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\t\t    UPDATE EXISTING ENTRY");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //First goes to the search method and return the array index number of the item to update
        int index = SearchItem();

        //return to main menu if user decided to exit through the search menu
        if (index == -1)
        {
            return;
        }

        //Displays the item you want to update (before update)
        Console.WriteLine($"ID: {itemID[index]}\t Name: {itemName[index]}\t Description: {itemDescription[index]}\t Quantity: {itemQuantity[index]}\t Price: {itemPrice[index]:C2}\t Date of Update: {itemUpdateDate[index]}");
        Console.WriteLine(SEPERATOR);

        //This loop prompt the user for new item information and repeats untill s/he confirms the information
        while (flag)
        {
            //New name for the item
            Console.Write("New name for the item: ");
            while (true)
            {
                try
                {
                    updateItemName = Console.ReadLine();
                    updateItemName = CleanString(updateItemName);
                    //Catches null inputs
                    if (updateItemName == "" || updateItemName == " ")
                    {
                        Console.Write("Error, enter a name! try again: ");
                    }
                    else
                    {
                        break;
                    }
                }
                //Catches int inputs
                catch
                {
                    Console.Write("Error, enter a name! try again: ");
                }
            }

            //New Description for the item
            Console.WriteLine();
            Console.Write("New description for the item: ");
            while (true)
            {
                try
                {
                    updateItemDescription = Console.ReadLine();
                    updateItemDescription = CleanString(updateItemDescription);
                    //Catches null inputs
                    if (updateItemDescription == "" || updateItemDescription == " ")
                    {
                        Console.Write("Error, enter a description! try again:");
                    }
                    else
                    {
                        break;
                    }
                }
                //Catches int inputs
                catch
                {
                    Console.Write("Error, enter a description! try again: ");
                }
            }

            //New quantity for the item
            Console.WriteLine();
            Console.Write("New quantity for the item: ");
            while (true)
            {
                try
                {
                    updateItemQuantity = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            //New price for the item
            Console.WriteLine();
            Console.Write("New price for the item: ");
            while (true)
            {
                try
                {
                    updateItemPrice = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            //Displays the new information entered
            Console.WriteLine();
            Console.WriteLine("This is the new information you have entered:\n");
            Console.WriteLine($"Name: {updateItemName}\t Description: {updateItemDescription}\t Quantity: {updateItemQuantity}\t Price: {updateItemPrice:c2}\n");

            //Asks the user to confirm the information, repeats if null or invalid inputs
            bool flag2 = true;
            while (flag2)
            {
                Console.Write("Is this information correct? Yes/No: ");
                string answer = "";
                while (true)
                {
                    try
                    {
                        answer = Console.ReadLine();
                        answer = CleanString(answer);
                        //Catches null inputs
                        if (answer == "" || answer == " ")
                        {
                            Console.Write("Error, enter yes or no!");
                        }
                        else
                        {
                            break;
                        }
                    }
                    //Catches int inputs
                    catch
                    {
                        Console.Write("Error, enter yes or no!");
                    }
                }
                //Confirms information and continues the program
                if (answer == "yes")
                {
                    flag = false;
                    flag2 = false;
                }

                //Declines information and repeats the loop to input new information
                else if (answer == "no")
                {
                    flag = true;
                    flag2 = false;
                }

                //Invalid input, repeats only the confirmation loop
                else
                {
                    Console.Write("Error, enter yes or no! ");
                }
            }
        }

        //Changes the update time of the item to the current time
        DateTime currentDateAndTime = DateTime.Now;

        //Puts the new information into parallel arrays
        itemName[index] = updateItemName;                   //Name
        itemDescription[index] = updateItemDescription;     //Description
        itemQuantity[index] = updateItemQuantity;           //Quantity
        itemPrice[index] = updateItemPrice;                 //Price
        itemUpdateDate[index] = currentDateAndTime;         //Update Time

        //Clears screen and return to the main menu
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Update Succesfull!");
        Console.ForegroundColor = ConsoleColor.White;
        Loading();
    }

    //This method removes all the information of one item from the arrays
    static void DeleteItem()
    {
        //Item information local variables, all set to -1 to remove item
        string[] tempitemName = new string[INVENTORYSIZE - 1];                  //Name
        string[] tempitemDescription = new string[INVENTORYSIZE - 1];           //Description
        int[] tempitemQuantity = new int[INVENTORYSIZE - 1];                    //Quantity
        double[] tempitemPrice = new double[INVENTORYSIZE - 1];                 //Price
        int[] tempitemID = new int[INVENTORYSIZE - 1];                          //ID
        int[] tempitemCategory = new int[INVENTORYSIZE - 1];                    //Category
        DateTime[] tempitemCreationDate = new DateTime[INVENTORYSIZE - 1];      //Creation Date
        DateTime[] tempitemUpdateDate = new DateTime[INVENTORYSIZE - 1];        //Update Date

        //Main array index
        int indexToRemove = 0;

        //Title
        Console.Clear();
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\t\t\tDELETE AN ENTRY");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //First goes to the search method and return the array index number of the item to delete
        Console.WriteLine("Search for an entry to delete:\n");
        indexToRemove = SearchItem();

        //Return to main menu if user decided to exit through the search menu
        if (indexToRemove == -1)
        {
            return;
        }

        Console.WriteLine($"ID: {itemID[indexToRemove]}\t Name: {itemName[indexToRemove]}\t Description: {itemDescription[indexToRemove]}\t Quantity: {itemQuantity[indexToRemove]}\t Price: {itemPrice[indexToRemove]:C2}\t Date of Update: {itemUpdateDate[indexToRemove]}");
        Console.WriteLine("\n" + SEPERATOR);

        // Copy elements from the original array to the new array, excluding the item to remove
        for (int i = 0, j = 0; i < INVENTORYSIZE; i++)
        {
            if (i != indexToRemove)
            {
                tempitemName[j] = itemName[i];                      //Name
                tempitemDescription[j] = itemDescription[i];        //Description
                tempitemQuantity[j] = itemQuantity[i];              //Quantity
                tempitemPrice[j] = itemPrice[i];                    //Price
                tempitemID[j] = itemID[i];                          //ID
                tempitemCategory[j] = itemCategory[i];              //Category
                tempitemCreationDate[j] = itemCreationDate[i];      //Creation Date
                tempitemUpdateDate[j] = itemUpdateDate[i];          //Update Date
                j++;
            }
        }

        //Return to default values the original arrays
        itemName.Initialize();              //Name
        itemDescription.Initialize();       //Description
        itemQuantity.Initialize();          //Quantity
        itemPrice.Initialize();             //Price
        itemID.Initialize();                //ID
        itemCategory.Initialize();          //Category
        itemCreationDate.Initialize();      //Creation Date
        itemUpdateDate.Initialize();        //Update Date

        //Copy only the remaining data
        for (int i = 0; i < INVENTORYSIZE - 1; i++)
        {
            itemName[i] = tempitemName[i];                      //Name
            itemDescription[i] = tempitemDescription[i];        //Description
            itemQuantity[i] = tempitemQuantity[i];              //Quantity
            itemPrice[i] = tempitemPrice[i];                    //Price
            itemID[i] = tempitemID[i];                          //ID
            itemCategory[i] = tempitemCategory[i];              //Category
            itemCreationDate[i] = tempitemCreationDate[i];      //Creation Date
            itemUpdateDate[i] = tempitemUpdateDate[i];          //Update Date
        }

        //Clears screen and return to main menu
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Delete Succesfull!");
        Console.ForegroundColor = ConsoleColor.White;

        //Reduce main index
        itemIndex--;
        Loading();
    }

    //Displays the search menu and directs the program to the selected method of searching
    static int SearchItem()
    {
        //Initializers
        int choice = 0;
        bool flag = true;

        //Returns -1 to send the program to the main menu
        int searchIndex = -1;

        //Displays search menu
        Console.WriteLine("Choose how you would like to search:\n");
        Console.WriteLine("1)Name  2)Category  3)ID\n");
        Console.Write("Choose a number: ");

        //This loop repeats in case of invalid inputs
        while (flag)
        {
            //Prompts the user for input to choose a search method
            while (true)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            //Sends the program to the corresponding method
            switch (choice)
            {
                //Search by name
                case 1:
                    searchIndex = SearchItemByName();
                    flag = false;
                    break;

                //Search by category
                case 2:
                    searchIndex = SearchItemByCategory();
                    flag = false;
                    break;

                //Search by ID
                case 3:
                    searchIndex = SearchItemByID();
                    flag = false;
                    break;

                //Catches invalid numbers and repeats the loop
                default:
                    Console.Write("Error, invalid number! try again: ");
                    break;
            }
        }

        //Sends the array number of the found item to the method that called search
        return searchIndex;
    }

    //Search for an item by name
    static int SearchItemByName()
    {
        //Loop handler
        bool flag = true;
        //Initializer
        string name = "";
        //Returns -1 to send the program to the main menu
        int searchIndex = -1;
        Console.WriteLine();
        //This loop repeats in case of invalid inputs
        while (flag)
        {
            Console.Write("Enter the item name or 0 to exit: ");
            while (true)
            {
                try
                {
                    name = Console.ReadLine();
                    name = CleanString(name);
                    //Catches null inputs
                    if (name == "" || name == " ")
                    {
                        Console.Write("Error, enter a name! try again :");
                    }
                    else
                    {
                        break;
                    }
                }
                //Catches int inputs
                catch
                {
                    Console.Write("Error, enter a name! try again: ");
                }
            }

            //Returns -1 to send the program to the main menu
            if (name == "0")
            {
                Console.Clear();
                break;
            }

            //Searches through all items for an item with a matching name to the input
            searchIndex = Array.IndexOf(itemName, name, 0, itemIndex);

            //Item found, exits the loop and continues program
            if (searchIndex != -1)
            {
                Console.WriteLine(SEPERATOR);
                Console.WriteLine("Item found:");
                Console.WriteLine();
                flag = false;
            }

            //Item not found
            else
            {
                Console.WriteLine("Item not found!");
                Console.WriteLine();

                //Asks the user if to continue or return to search menu
                bool flag2 = true;
                while (flag2)
                {
                    string choice = "";
                    Console.Write("Return to main menu? ");
                    while (true)
                    {
                        try
                        {
                            choice = Console.ReadLine();
                            choice = CleanString(choice);
                            //Catches null inputs
                            if (choice == "" || choice == " ")
                            {
                                Console.Write("Error, enter yes or no! try again: ");
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch
                        {
                            Console.Write("Error, enter yes or no! try again: ");
                        }
                    }
                    switch (choice)
                    {
                        //Exits the loops and return to search menu
                        case "yes":
                            flag2 = false;
                            flag = false;
                            Console.Clear();
                            break;

                        //Repeats the loop, asks for a name input and searches again
                        case "no":
                            flag2 = false;
                            break;

                        //Catches invalid inputs, repeats only the last loop
                        default:
                            Console.Write("Error, enter yes or no!");
                            break;
                    }
                }
            }
        }

        //Sends the array number of the found item to the method that called search
        return searchIndex;
    }

    //Display all items in a category and choose one of them
    static int SearchItemByCategory()
    {
        //Initializer
        int choice = 0;
        //Loop handler
        bool flag = true;
        //This loop repeats in case of invalid input
        while (flag)
        {
            //Displays all the categories
            Console.WriteLine("Here are the categories:\n");
            for (int i = 0; i < category.Length; i++)
            {
                Console.Write($"{i + 1}){category[i]}  ");
            }
            Console.WriteLine();

            //Asks the user for input to choose a category
            Console.Write("\nChoose the number of the category: ");
            while (true)
            {
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }
            Console.WriteLine(SEPERATOR);

            //Catches invalid inputs and repeats the loop
            if (choice == 0 || choice > category.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error, invalid number! Enter a number between 1 and {0}.\n", category.Length);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
            //Exits the loop and continues program
            else
            {
                flag = false;
            }
        }

        //Reduces user input by 1 to correspond to the displayed numbers of the categories
        choice--;

        //Displays all items in the chosen category
        Console.WriteLine("All items in the category:\n");
        for (int i = 0; i < itemIndex; i++)
        {
            if (itemCategory[i] == choice)
            {
                Console.WriteLine($"{i + 1})\tID: {itemID[i]}\tName: {itemName[i]}\t Description: {itemDescription[i]}\t Quantity: {itemQuantity[i]}\t Price: {itemPrice[i]:c2}\t Date of Update: {itemUpdateDate[i]}");
                Console.WriteLine();
            }
        }

        //Asks the user to choose an item from the list
        Console.Write("\nChoose the number of the Item or press 0 to exit: ");
        int searchIndex = 0;

        //This loop repeats in case of invalid input
        while (true)
        {
            while (true)
            {
                try
                {
                    searchIndex = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }

            //Returns -1 to send the program to the main menu
            if (searchIndex == 0)
            {
                Loading();
                return -1;
            }
            //Catches invalid inputs
            else if (searchIndex > itemIndex)
            {
                Console.Write("Error, invalid number! try again: ");
            }
            else
            {
                break;
            }
        }
        return searchIndex - 1;
    }

    //Search for an item by ID
    static int SearchItemByID()
    {
        //Initializer
        int id = 0;
        //Loop handler
        bool flag = true;
        //Returns - 1 to send the program to the main menu
        int searchIndex = -1;
        Console.WriteLine();
        //This loop repeats in case of invalid inputs
        while (flag)
        {
            Console.Write("Enter the item ID or 0 to exit: ");
            while (true)
            {
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                //Catches null or string inputs
                catch
                {
                    Console.Write("Error, enter a number! try again: ");
                }
            }
            //Returns - 1 to send the program to the main menu
            if (id == 0)
            {
                Console.Clear();
                flag = false;
            }

            //Searches through all items for an item with a matching ID to the input
            searchIndex = Array.IndexOf(itemID, id, 0, itemIndex);

            //Item found, exits the loop and continues program
            if (searchIndex != -1)
            {
                Console.WriteLine(SEPERATOR);
                Console.WriteLine("Item found:");
                Console.WriteLine();
                flag = false;
            }

            //Invalid input, repeats loop
            else if (id < 1000 || id > 10000)
            {
                Console.WriteLine("Item must be 4 digit!");
                Console.WriteLine();
            }

            //Item not found
            else
            {
                Console.WriteLine("Item not found!");
                Console.WriteLine();

                //Asks the user if to continue or return to search menu
                bool flag2 = true;
                while (flag2)
                {
                    string choice = "";
                    Console.Write("Return to main menu? ");
                    while (true)
                    {
                        try
                        {
                            choice = Console.ReadLine();
                            choice = CleanString(choice);
                            //Catches null inputs
                            if (choice == "" || choice == " ")
                            {
                                Console.Write("Error, enter yes or no! try again: ");
                            }
                            else
                            {
                                break;
                            }
                        }
                        //Catches int inputs
                        catch
                        {
                            Console.Write("Error, enter yes or no! try again: ");
                        }
                    }
                    switch (choice)
                    {
                        //Exits both loops and return to the search menu
                        case "yes":
                            flag2 = false;
                            flag = false;
                            Console.Clear();
                            break;

                        //Repeats the loop, asks for an ID input and searches again
                        case "no":
                            flag2 = false;
                            break;

                        //Catches invalid inputs, repeats only the last loop
                        default:
                            Console.Write("Error, enter yes or no! ");
                            break;
                    }
                }
            }
        }

        //Sends the array number of the found item to the method that called search
        return searchIndex;
    }
    
    
    public static void CreateRandomItem()//Create an item with random information for testing purposes
    {
        //Arrays for random names and description
        string[] names = { "acer", "dell", "lenovo", "rog", "msi", "steelseries", "razer", "intel", "amd" };
        string[] descriptions = { "Mouse", "Keyboard", "Gaming Mouse", "Computer", "Laptop", "Mousepad", "Speakers" };

        //Item information local variables
        string itemRandomName = "";             //Name
        string itemRandomDescription = "";      //Description
        int itemRandomQuantity = 0;             //Quantity
        double itemRandomPrice = 0;             //Price
        int categoryRandom = 0;                 //Category

        //Loop handler
        bool flag = true;

        //Title
        Console.Clear();
        Console.WriteLine(SEPERATOR);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\t\t\tCREATE A RANDOM ENTRY");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(SEPERATOR);

        //Create random name
        do
        {
            itemRandomName = names[random.Next(names.Length)];
        } while (Array.IndexOf(names, itemRandomName, 0, itemIndex) != -1); // Ensure ID is unique

        //Create random description
        do
        {
            itemRandomDescription = descriptions[random.Next(descriptions.Length)];
        } while (Array.IndexOf(descriptions, itemRandomDescription, 0, itemIndex) != -1); // Ensure ID is unique

        //Create random quantity
        itemRandomQuantity = random.Next(0, 1000);

        //Create random price
        itemRandomPrice = random.NextDouble() * (1000 - 0) + 0;

        //Create random category
        categoryRandom = random.Next(0, category.Length);

        //Generate new ID
        int newID = GenerateID();

        //Gets the current date and time
        DateTime currentDateAndTime = DateTime.Now;

        //Display the item information
        Console.Write("This is the random item information:\n");
        Console.WriteLine($"\nName:{itemRandomName}\tDescription:{itemRandomDescription}\tQuantity:{itemRandomQuantity}\tPrice:{itemRandomPrice:c2}\tCategory:{category[categoryRandom]}\n");

        //Display the item ID
        Console.Write($"This is the item ID: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{newID}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" ! please remember that.");

        //Puts the information into parallel arrays
        itemName[itemIndex] = itemRandomName;                   //Name
        itemDescription[itemIndex] = itemRandomDescription;     //Description
        itemQuantity[itemIndex] = itemRandomQuantity;           //Quantity
        itemPrice[itemIndex] = itemRandomPrice;                 //Price
        itemID[itemIndex] = newID;                              //ID
        itemCategory[itemIndex] = categoryRandom;               //Category
        itemCreationDate[itemIndex] = currentDateAndTime;       //Creation Date
        itemUpdateDate[itemIndex] = currentDateAndTime;         //Update Date

        //Increases the main index
        itemIndex++;

        //Clears the screen and return to main menu
        Loading();
    }
    //Optional Load Data method, to load excel sheet into inventory
    
    /*public static void DataLoad()
    {

        Spreadsheet document = new Spreadsheet();

        document.LoadFromFile(@"D:\inventory project\Data.xls");

        Worksheet worksheet = document.Workbook.Worksheets.ByName("Sheet1");

        int fileRows = 5;

        int fileColumns = 5;

        string temp = "";

        for (int i = 0; i < fileRows; i++) //Rows

        {

            for (int j = 0; j < fileColumns; j++) // Columns

            {

                switch (j)

                {

                    case 0:

                        itemName[i] = Convert.ToString(worksheet.Cell(i, j));

                        break;

                    case 1:

                        itemDescription[i] = Convert.ToString(worksheet.Cell(i, j));

                        break;

                    case 2:

                        temp = Convert.ToString(worksheet.Cell(i, j));

                        itemQuantity[i] = Convert.ToInt32(temp);

                        break;

                    case 3:

                        temp = Convert.ToString(worksheet.Cell(i, j));

                        itemPrice[i] = Convert.ToDouble(temp);

                        break;
                    case 4:

                        temp = Convert.ToString(worksheet.Cell(i, j));

                        itemCategory[i] = Convert.ToInt32(temp);

                        break;

                    default:

                        break;

                }

                //Console.WriteLine(worksheet.Cell(i, j));

            }

            //fill dates, and id

            DateTime currentDateAndTime = DateTime.Now;

            itemCreationDate[i] = currentDateAndTime;

            itemUpdateDate[i] = currentDateAndTime;

            itemID[i] = GenerateID();

            itemIndex++;

        }

        document.Close();

        //Console.ReadKey();

    }*/
}

