using System.Collections.Generic;
using System.Data;
using Dapper;
using summer2020_dotnet_gregslist.Models;

namespace summer2020_dotnet_gregslist.Repositories
{
  public class HousesRepository
  {
    private readonly IDbConnection _db;
    public HousesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal IEnumerable<House> GetHousesByUserId(string userId)
    {
      string sql = "SELECT * FROM Houses WHERE userId = @userId";
      return _db.Query<House>(sql, new { userId });
    }

    internal House GetById(int id)
    {
      string sql = "SELECT * FROM Houses WHERE id = @id";
      return _db.QueryFirstOrDefault<House>(sql, new { id });
    }

    internal IEnumerable<House> GetAll()
    {
      string sql = "SELECT * FROM Houses";
      return _db.Query<House>(sql);
    }

    internal House Create(House newHouse)
    {
      string sql = @"
            INSERT INTO Houses
            (make, model, userId, year, price, body, imgUrl)
            VALUES
            (@Make, @Model, @UserId, @Year, @Price, @Body, @ImgUrl);
            SELECT LAST_INSERT_ID()";
      newHouse.Id = _db.ExecuteScalar<int>(sql, newHouse);
      return newHouse;
    }

    internal bool BidOnHouse(House HouseToBidOn)
    {
      string sql = @"
            UPDATE Houses
            SET
            price = @Price
            WHERE id = @Id";
      int affectedRows = _db.Execute(sql, HouseToBidOn);
      return affectedRows == 1;
    }

    internal bool Edit(House HouseToUpdate, string userId)
    {
      HouseToUpdate.UserId = userId;
      string sql = @"
            UPDATE Houses
            SET
                price = @Price,
                make = @Make,
                model = @Model,
                imgUrl = @ImgUrl,
                year = @Year,
                body = @Body
            WHERE id = @Id
            AND userId = @UserId";
      int affectedRows = _db.Execute(sql, HouseToUpdate);
      return affectedRows == 1;
    }

    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM Houses WHERE id = @id AND userId = @userId LIMIT 1";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}
