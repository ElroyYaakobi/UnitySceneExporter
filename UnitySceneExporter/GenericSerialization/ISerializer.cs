namespace UnitySceneExporter.GenericSerialization
{
    public interface ISerializer
    {
        byte[] data { get; set; }

        void Serialize(object obj);
        object Deserialize();
    }
}