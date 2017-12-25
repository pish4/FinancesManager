using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancesManager.Services.Interfaces
{
    public interface IBaseBL<TDto>
    {
        IEnumerable<TDto> GetAll();

        TDto GetById(long id);

        void Update(TDto dto);

        void Delete(long id);
    }
}
