using MotorcycleRentalSystem.Responses;

namespace MotorcycleRentalSystem.Extensions;

public static class ApplicationErrorExtensions
{
    public static string GetMessage(this AppMessage message)
    {
        return message switch
        {
            AppMessage.DuplicateRecord => "Registro já cadastrado",
            AppMessage.InvalidData => "Dados inválidos",
            AppMessage.UnexpectedError => "Ocorreu um erro inesperado",
            AppMessage.ModifiedPlate => "Placa modificada com sucesso",
            AppMessage.MotorcycleNotFound => "Moto não encontrada",
            AppMessage.RentNotFound => "Locação não encontrada",
            AppMessage.DevolveDateOk => "Data de devolução informada com sucesso",
            _ => "Erro desconhecido"
        };
    }
}