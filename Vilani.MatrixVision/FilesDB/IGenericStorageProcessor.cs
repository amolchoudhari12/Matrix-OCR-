using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vilani.Files.DB.Core
{
    public interface IGenericStorageProcessor<T>
    {
        List<T> GetAll();
        int WriteToFiles(List<T> list);     
        int Add(T entity);
        int Delete(T entity);
        int Edit(T entity);

        int RemoveAll();
    }
}
