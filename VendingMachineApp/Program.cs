using VendingMachineApp.Constants;
using VendingMachineApp.Models;
using VendingMachineApp.Services;
using VendingMachineApp.Utilities;


VendingMachine vendingMachine = new VendingMachine();
Menu userInterface = new Menu();
while (true)
{
    userInterface.RunInterface(vendingMachine);
}