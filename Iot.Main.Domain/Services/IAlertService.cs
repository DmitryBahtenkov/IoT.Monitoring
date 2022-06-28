using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.AlertModel;
using Iot.Main.Domain.Models.AlertModel.DTO;

namespace Iot.Main.Domain.Services;

public interface IAlertService
{
    public Task<Alert> Create(AlertRequest request);
    public Task<Alert> Update(int id, AlertRequest request);
    public Task<Alert> Get(AlertFilter filter);
    public Task<List<Alert>> GetAll(AlertFilter filter);

}
