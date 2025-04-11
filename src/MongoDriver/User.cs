using System;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace mongobenchmark.MongoDriver;

public class User
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public DateTime BirthDate { get; set; }
}