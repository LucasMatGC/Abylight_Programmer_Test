
namespace Assets.Scripts.Utils
{
    public enum RegisterType { car, character, building }
    public class Register
    {
        RegisterType type;
        string comment;
        int value;

        /// <summary>
        /// Register constructor
        /// </summary>
        public Register(RegisterType type, string comment, int value)
        {

            this.type = type;
            this.comment = comment;
            this.value = value;

        }

        /// <summary>
        /// Prints the data from the Register
        /// </summary>
        public string GetContent()
        {
            return type.ToString() + " [" + comment + "] " + value.ToString() + "\n";
        }
    }
}