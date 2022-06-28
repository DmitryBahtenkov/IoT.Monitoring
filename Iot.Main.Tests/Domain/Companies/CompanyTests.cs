using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Iot.Main.Tests.Domain.Companies;

    public partial class CompanyTests
    {
        [Fact]
        public void ToDataTest()
        {
            var company = TestCompanies.CompanyWithDevice;

            var data = company.ToData();

            Assert.Equal(data.Id, company.Id);
            Assert.Equal(data.Name, company.Name);
            Assert.Equal(data.Description, company.Description);
        }
        
    }
