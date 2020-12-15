namespace DependencyInjectionModule
{
    // This interface helps decouple the concept of "writing output" from the Console class. 
    // We don't really "care" how the Write operation happens, just that we can write.
    public interface IOutputService
    {
        void Write(string content);
    }
}