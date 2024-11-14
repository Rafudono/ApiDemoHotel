using System;
using System.Collections.Generic;

namespace ApiDemoHotel;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? IdRoom { get; set; }

    public int IdRole { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime? LastLogInDate { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual ICollection<Date>? Dates { get; set; } = null;

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual Room? IdRoomNavigation { get; set; }
}
public partial class UserModel
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public int? IdRoom { get; set; }
    public int? RoomNumber { get; set; }
    public int IdRole { get; set; }

    public bool IsBlocked { get; set; }

    public DateTime? LastLogInDate { get; set; }

    public DateTime RegistrationDate { get; set; }
    public string RoleTitle { get; set; } = null!;

    public static explicit operator User(UserModel model)
    {
        return new User
        {
            Id = model.Id,
            Login = model.Login,
            Password = model.Password,
            Name = model.Name,
            Surname = model.Surname,
            Patronymic = model.Patronymic,
            IdRoom = model.IdRoom,
            IdRole = model.IdRole,
            IsBlocked = model.IsBlocked,
            LastLogInDate = model.LastLogInDate,
            RegistrationDate = model.RegistrationDate
        };
    }
    public static explicit operator UserModel(User model)
    {
        return new UserModel
        {
            Id = model.Id,
            Login = model.Login,
            Password = model.Password,
            Name = model.Name,
            Surname = model.Surname,
            Patronymic = model.Patronymic,
            IdRoom = model.IdRoom,
            IdRole = model.IdRole,
            IsBlocked = model.IsBlocked,
            LastLogInDate = model.LastLogInDate,
            RegistrationDate = model.RegistrationDate,
            RoleTitle=model.IdRoleNavigation?.Title ?? "user",
            RoomNumber=model.IdRoomNavigation?.Number 

};
    }
}