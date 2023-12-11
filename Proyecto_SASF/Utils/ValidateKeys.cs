using AGE.Middleware.Exceptions.BadRequest;

namespace AGE.Utils
{
    public class ValidateKeys
    {

        public static void ValidarExistenciaKey<TEntity, TPk>(TPk key, string mensaje, Func<TPk, Task<TEntity>> ConsultarCompletePorId)
        {
            TEntity entityObject = ConsultarCompletePorId(key).Result;

            if (entityObject == null)
                throw new ForeignKeyViolationException(mensaje);
        }


        public static void ValidarNoExistenciaKey<TEntity, TPk>(TPk key, string mensaje, Func<TPk, Task<TEntity>> ConsultarCompletePorId)
        {
            TEntity entityObject = ConsultarCompletePorId(key).Result;

            if (entityObject != null)
                throw new ExistingObjectException(mensaje);
        }


        public static void ValidarPKDuplicadas<T, TKey>(List<T> lista, Func<T, TKey> keySelector, string FieldName)
        {
            var duplicateKeys = lista
                .GroupBy(keySelector)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateKeys.Any())
                throw new UniqueFieldException(FieldName);
        }

    }
}
