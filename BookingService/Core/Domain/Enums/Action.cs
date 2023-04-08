namespace Domain.Enums;

public enum Action
{
    Pay = 0,
    Finish = 1, //after paid and used
    Cancel = 2, //cab never be paid
    Refound = 3, //paid then Refound
    Reopen = 4 //canceled
}
