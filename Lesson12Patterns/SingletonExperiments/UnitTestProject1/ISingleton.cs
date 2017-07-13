namespace SingletonTests
{
	public interface ISingleton
	{
		SingletonWithInterface GetInstance();
		int GetValue();
	}
}