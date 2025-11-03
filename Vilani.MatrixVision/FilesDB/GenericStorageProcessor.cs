using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vilani.Systems.Common.Core;




namespace Vilani.Files.DB.Core
{

    public class GenericStorageProcessor<C, T> :
    IGenericStorageProcessor<T>
        where T : BaseFileDB
        where C : VilaniFileDataSource, new()
    {
        public GenericStorageProcessor(string fileDBName)
        {
            Context.FileDBName = fileDBName;
        }

        public GenericStorageProcessor(string filePath, string fileDBName)
        {
            Context.FileDBPath = Path.Combine(filePath, fileDBName);
            Context.FileDBName = fileDBName;
        }

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual List<T> GetAll()
        {
            List<T> result = null;

            if (File.Exists(Context.FileDBPath))
            {
                using (var fs = File.OpenRead(Convert.ToString(Context.FileDBPath)))
                {
                    if (fs != null)
                    {
                        result = Serializer.Deserialize<List<T>>(fs).ToList();
                    }
                }
            }
            else
            {
                result = new List<T>();
                WriteToFiles(result);
            }
            return result;
        }

        public int WriteToFiles(List<T> list)
        {
            ProtoContractSerializer.Serialize(list, Context.FileDBPath);
            return list.Count;
        }


        public virtual int Add(T entity)
        {
            List<T> result = null;

            if (!File.Exists(Context.FileDBPath))
            {
                using (var fs = File.Create(Convert.ToString(Context.FileDBPath)))
                {
                    result = Serializer.Deserialize<List<T>>(fs).ToList();
                    entity.ID = result.Count + 1;
                    result.Add(entity);
                }

            }
            else using (var fs = File.OpenRead(Convert.ToString(Context.FileDBPath)))
                {
                    result = Serializer.Deserialize<List<T>>(fs).ToList();
                    entity.ID = result.Count + 1;
                    result.Add(entity);
                }

            WriteToFiles(result);
            return entity.ID;
        }




        public virtual int Delete(T entity)
        {
            List<T> result = null;
            using (var fs = File.OpenRead(Convert.ToString(Context.FileDBPath)))
            {
                result = Serializer.Deserialize<List<T>>(fs).ToList().Where(x => x.ID != entity.ID).ToList();
            }

            WriteToFiles(result);
            return entity.ID;
        }

        public virtual int Edit(T entity)
        {
            List<T> result = null;
            using (var fs = File.OpenRead(Convert.ToString(Context.FileDBPath)))
            {
                result = Serializer.Deserialize<List<T>>(fs).ToList().Where(x => x.ID != entity.ID).ToList();
                result.Add(entity);
            }
            WriteToFiles(result);
            return entity.ID;
        }



        public int RemoveAll()
        {
            try
            {

                List<T> result = new List<T>();
                WriteToFiles(result);

                return result.Count;
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
    }
}
