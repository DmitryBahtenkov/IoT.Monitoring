using System.ComponentModel.DataAnnotations;

namespace Iot.Main.Domain.Models.CompanyModel.DTO;

public class CreateCompanyRequest
{
    [Required(ErrorMessage = "Введите название компании")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Введите описание компании")]
    public string Description { get; set; }
}
