using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Main.Domain.Models.ConstraintModel;
using Iot.Main.Domain.Models.ConstraintModel.DTO;
using Iot.Main.Domain.Models.InputModel;

namespace Iot.Main.Domain.Services;

public interface IConstraintService
{
    public Task<ConstraintData> Create(CreateConstraintRequest request); 
    public Task<ConstraintData> Update(int id, UpdateConstraintRequest request); 
    public Task<bool> CheckInputData(string token, InputData data); 
    public Task<ConstraintData> Get(ConstraintFilter filter); 
    public Task<List<ConstraintData>> GetAll(ConstraintFilter filter); 
}
