namespace Green.App.Service.Service
{
    public interface IManageableService
    {
        /// <summary>
        /// Start service
        /// </summary>
        void Start();

        /// <summary>
        /// Stop service
        /// </summary>
        void Stop();
    }
}