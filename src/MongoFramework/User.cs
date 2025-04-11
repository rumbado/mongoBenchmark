using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace mongobenchmark.MongoFramework;

[Table("users")]
public class User 
{
       public string? Id { get; set; }
        public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime BirthDate { get; set; }
}