using System;
using MongoDB.Entities;

namespace mongobenchmark.MongoEntities;

[Collection("users")]
public class User : Entity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime BirthDate { get; set; }
}