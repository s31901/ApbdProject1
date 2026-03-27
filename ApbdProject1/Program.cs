using ApbdProject1.Services;
using ApbdProject1.UI;

var equipmentService = new EquipmentService();
var userService = new UserService();
var rentalService = new RentalService();

var consoleMenu = new ConsoleMenu(equipmentService, userService, rentalService);
consoleMenu.Run();