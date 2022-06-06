using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using WebApplication_Cw4.Models;

namespace WebApplication_Cw4.Services
{
    public class AnimalDbService : IAnimalDbService
    {
        private const string ConnString = "Data Source=db-mssql;Initial Catalog=jd;Integrated Security=True";

        public async Task<MethodResult> AddAnimalAsync(AnimalDTO animalDTO)
        {
            string sql = "SELECT COUNT(1) FROM Animal WHERE IdAnimal = @idAnimal";

            await using SqlConnection connection = new SqlConnection(ConnString);

            // Zakładamy transakcję na połączenie, z którego będziemy korzystać w ramach obsługi żądania
            await using DbTransaction transaction = await connection.BeginTransactionAsync();

            await using SqlCommand command = new SqlCommand(sql, connection, (SqlTransaction)transaction);

            await connection.OpenAsync();

            command.Parameters.AddWithValue("idAnimal", animalDTO.IdAnimal);

            int? res = (int?)await command.ExecuteScalarAsync();

            if (res > 0)
            {
                // W przypadku gdy chcemy wyjść z metody z powodu niespełnienia jakiegoś warunku, który sobie założymy
                // należy pamiętać o wycofaniu transakcji i przerwaniu jej metodą Rollback
                await transaction.RollbackAsync();

                return new()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Zwierzak o podanym ID już istnieje!"
                };
            }

            sql = "INSERT INTO Animal VALUES (@idAnimal, @name, @description, @category, @area)";

            // Resetuję poprzednie parametry powiązane z SqlCommand żeby móc reużyć obiekt korzystając z innej metody i SQLki
            command.Parameters.Clear();
            command.CommandText = sql;
            command.Parameters.AddWithValue("idAnimal", animalDTO.IdAnimal);
            command.Parameters.AddWithValue("name", animalDTO.Name);
            command.Parameters.AddWithValue("description", animalDTO.Description);
            command.Parameters.AddWithValue("category", animalDTO.Category);
            command.Parameters.AddWithValue("area", animalDTO.Area);

            // Przed pomyślnym zakończeniem wykonywania metody należy pamiętać o zatwierdzeniu transakcji metodą Commit
            await transaction.CommitAsync();

            return new()
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
