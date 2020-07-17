using System;
using System.Collections.Generic;
using summer2020_dotnet_gregslist.Models;
using summer2020_dotnet_gregslist.Repositories;

namespace summer2020_dotnet_gregslist.Services
{
  public class JobsService
  {
    private readonly JobsRepository _repo;

    public JobsService(JobsRepository repo)
    {
      _repo = repo;
    }

    internal IEnumerable<Job> GetAll()
    {
      return _repo.GetAll();
    }

    internal IEnumerable<Job> GetByUserId(string userId)
    {
      return _repo.GetJobsByUserId(userId);
    }

    public Job GetById(int id)
    {
      Job foundJob = _repo.GetById(id);
      if (foundJob == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundJob;
    }

    internal Job Create(Job newJob)
    {
      return _repo.Create(newJob);
    }

    internal Job Edit(Job carToUpdate, string userId)
    {
      Job foundJob = GetById(carToUpdate.Id);
      // NOTE Check if not the owner, and price is increasing
      if (foundJob.UserId != userId && foundJob.Price < carToUpdate.Price)
      {
        if (_repo.BidOnJob(carToUpdate))
        {
          foundJob.Price = carToUpdate.Price;
          return foundJob;
        }
        throw new Exception("Could not bid on that car");
      }
      if (foundJob.UserId == userId && _repo.Edit(carToUpdate, userId))
      {
        return carToUpdate;
      }
      throw new Exception("You cant edit that, it is not yo car!");
    }

    internal string Delete(int id, string userId)
    {
      Job foundJob = GetById(id);
      if (foundJob.UserId != userId)
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