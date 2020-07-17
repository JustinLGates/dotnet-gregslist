using System;
using System.Collections.Generic;
using summer2020_dotnet_gregslist.Models;
using summer2020_dotnet_gregslist.Repositories;

namespace fullstack_gregslist.Services
{
  public class HousesService
  {
    private readonly HousesRepository _repo;

    public HousesService(HousesRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<House> GetAll()
    {
      return _repo.GetAll();
    }

    internal IEnumerable<House> GetByUserId(string userId)
    {
      return _repo.GetHousesByUserId(userId);
    }

    public House GetById(int id)
    {
      House foundHouse = _repo.GetById(id);
      if (foundHouse == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundHouse;
    }

    internal House Create(House newHouse)
    {
      return _repo.Create(newHouse);
    }

    internal House Edit(House carToUpdate, string userId)
    {
      House foundHouse = GetById(carToUpdate.Id);
      // NOTE Check if not the owner, and price is increasing
      if (foundHouse.UserId != userId && foundHouse.Price < carToUpdate.Price)
      {
        if (_repo.BidOnHouse(carToUpdate))
        {
          foundHouse.Price = carToUpdate.Price;
          return foundHouse;
        }
        throw new Exception("Could not bid on that car");
      }
      if (foundHouse.UserId == userId && _repo.Edit(carToUpdate, userId))
      {
        return carToUpdate;
      }
      throw new Exception("You cant edit that, it is not yo car!");
    }

    internal string Delete(int id, string userId)
    {
      House foundHouse = GetById(id);
      if (foundHouse.UserId != userId)
      {
        throw new Exception("This is not your car!");
      }
      if (_repo.Delete(id, userId))
      {
        return "Sucessfully delorted.";
      }
      throw new Exception("Somethin bad happened");
    }
  }
}