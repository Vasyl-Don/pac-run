namespace Helpers
{
    public class DontDestroyObject : Singleton<DontDestroyObject>
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}