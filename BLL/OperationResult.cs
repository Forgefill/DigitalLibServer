
namespace BLL
{
    public class OperationResult<T>
    {
        public List<string> Errors { get; private set; }

        public T Entity { get; }

        public bool IsSuccess { get; private set; }

        private OperationResult(T entity)
        {
            Entity = entity;
            Errors = new List<string>();
        }

        private OperationResult()
        {
            Errors = new List<string>();
        }

        public static OperationResult<T> Success(T entity)
        {
            var result = new OperationResult<T>(entity);
            result.IsSuccess = true;
            return result;
        }

        public static OperationResult<T> Failture()
        {
            var result = new OperationResult<T>();
            result.IsSuccess = false;
            return result;
        }

        public static OperationResult<T> Failture(params string[] errors)
        {
            var result = Failture();
            result.Errors.AddRange(errors);
            return result;
        }

        public void AddErrors(params string[] errors)
        {
            Errors.AddRange(errors);
        }
    }
}
