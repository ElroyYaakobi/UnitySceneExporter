namespace UnitySceneExporter.GenericSerialization
{
    public interface ISerializer
    {
        void Serialize(object obj);
        void Deserialize(object obj);
    }
}