
namespace Assets.Scripts.Utils
{
    public enum RegisterType { car, character, building }
    public class Register
    {
        RegisterType type;
        string comment;
        int value;

        public Register(RegisterType type, string comment, int value)
        {

            this.type = type;
            this.comment = comment;
            this.value = value;

        }

        public string GetContent()
        {
            return type.ToString() + " [" + comment + "] " + value.ToString() + "\n";
        }
    }
}