namespace Proyecto_SASF.Utils
{
    public class ConversorEntityToDAOList<TEntity, TDao>
    {
        public static List<TDao> FromEntityToDAOList(List<TEntity> entityObjectList, Func<TEntity, TDao> FromEntityToDAO)
        {
            List<TDao> DAOobjectList = new List<TDao>();

            foreach (TEntity entityObject in entityObjectList)
            {
                DAOobjectList.Add(FromEntityToDAO(entityObject));
            }

            return DAOobjectList;
        }
    }
}
