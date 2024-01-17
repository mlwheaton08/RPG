namespace RPG;

internal interface IItem
{
    string SuccessMessage { get; set; }
    string FailMessage { get; set; }
    string Name { get; set; }
    int Quantity { get; set; }

    void Use(Monster monster, Player player);
}