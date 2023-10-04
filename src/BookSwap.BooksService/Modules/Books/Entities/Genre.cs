﻿using BookSwap.Shared.Core.Models;

namespace BookSwap.BooksService.Modules.Books.Entities;

public class Genre : Entity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    private Genre() { }
    
    public Genre(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}