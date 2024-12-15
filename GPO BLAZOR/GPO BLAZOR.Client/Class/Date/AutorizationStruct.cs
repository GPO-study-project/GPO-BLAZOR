﻿
namespace GPO_BLAZOR.Client.Class.Date
{
    public enum Roles
    {
        Student,
        CafedralLeader,
        FieldCommander,
    }
    public interface IAutorizationStruct : IAsyncDisposable
    {
        string GetString();
        Roles[] Role { get; set; }

        bool HasValue(Roles role);

    }
    public class AutorizationStruct : IAutorizationStruct
    {

        static public Roles RoleSelector(string x)
        {
            switch (x)
            {
                case "Student":
                    return Roles.Student;
                case "CafedralLeader":
                    return Roles.CafedralLeader;
                case "FieldCommander":
                    return Roles.FieldCommander;
                default:
                    return Roles.Student;
            }
        }
        public AutorizationStruct()
        {

        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public string GetString()
        {
            return "1";
        }
        public Roles[] Role { get; set; }

        public bool HasValue(Roles role)
        {
            try
            {
                return Role.Contains(role);
            }
            catch (Exception ex)
            {
                {
#if DEBUG
                    Console.WriteLine($"Role: {role} selector exception -> {ex.Message}");
#endif
                    return false;
                }
            }
        }
    }
}
