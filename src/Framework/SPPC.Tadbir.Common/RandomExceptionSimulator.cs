using System;
using System.Diagnostics;

namespace SPPC.Tadbir
{
    /// <summary>
    /// Simulates random occurence of different types of error
    /// </summary>
    /// <remarks>
    /// This is a temporary class used for testing error handling middleware.
    /// It can be used by sending an HTTP GET request to the following API URL :
    ///   => branches/{branchId} (GET)
    /// Using a simple random logic, it simulates different runtime exceptions in all architecture layers.
    /// </remarks>
    public static class RandomExceptionSimulator
    {
        /// <summary>
        /// Simulates an exception in controller level
        /// </summary>
        public static void ThrowControllerException()
        {
            if (_current % 8 == 0)
            {
                throw new Exception("Error in API controller. (Error Code : 1001)");
            }
        }

        /// <summary>
        /// Simulates an exception in application repository level
        /// </summary>
        public static void ThrowRepositoryException()
        {
            if (_current % 3 == 0)
            {
                throw new Exception("Error in Repository. (Error Code : 2001)");
            }
        }

        /// <summary>
        /// Simulates an exception in framework repository level
        /// </summary>
        public static void ThrowFrameworkRepositoryException()
        {
            if (_current % 5 == 0)
            {
                throw new Exception("Error in Framework Repository. (Error Code : 3001)");
            }
        }

        /// <summary>
        /// Simulates an exception in mapping from model to view model and vice versa
        /// </summary>
        public static void ThrowMapperException()
        {
            if (_current % 7 == 0)
            {
                throw new Exception("Error in model<->viewmodel mapping. (Error Code : 4001)");
            }
        }

        /// <summary>
        /// Simulates an exception in EF Core entity mapping
        /// </summary>
        public static void ThrowOrmMapperException()
        {
            if (_current % 11 == 0)
            {
                throw new Exception("Error in ORM mapping. (Error Code : 5001)");
            }
        }

        /// <summary>
        /// Simulates a new request context by changing the underlying random number
        /// </summary>
        public static void NextRequest()
        {
            _random = new Random((int)DateTime.Now.Ticks);
            _current = _random.Next(100);
            Debug.WriteLine(Environment.NewLine);
            Debug.WriteLine("[{0}] INFO: New request, _current = {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), _current);
        }

        private static int _current = 0;
        private static Random _random;
    }
}
