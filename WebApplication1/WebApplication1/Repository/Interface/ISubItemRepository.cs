using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Repository
{
    public interface ISubItemRepository
    {
        SubItem Add(SubItem model);
        List<SubItem> GetAll(int id);
        SubItem Get(int id);
        SubItem Edit(SubItem model);
        int Delete(int id);
    }
}
