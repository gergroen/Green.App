namespace Green.App.ServiceWebApi
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