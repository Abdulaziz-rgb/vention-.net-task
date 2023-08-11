using api_task.Models;
using CsvHelper.Configuration;

namespace api_task.Mapper;

public sealed class CsvDataMapper: ClassMap<User>
{
    public CsvDataMapper()
    {
        Map(m => m.UserId).Index(0);
        Map(m => m.Username).Index(1);
        Map(m => m.Age).Index(2);
        Map(m => m.City).Index(3);
        Map(m => m.Phonenumber).Index(4);
        Map(m => m.Email).Index(5);
    }
}