﻿namespace WebATB.Interfaces;

public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);

    Task DeleteImageAsync(string name);
}
