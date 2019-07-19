namespace AV_Tool
{
    class Versioning
    {
        public int major = 0;
        public int minor = 0;
        public int patch = 0;

        public Versioning(string rawVersion)
        {
            string[] versions = rawVersion.Split('.');

            if (versions.Length == 3)
            {
                int.TryParse(versions[0], out this.major);
                int.TryParse(versions[1], out this.minor);
                int.TryParse(versions[2], out this.patch);
            }
        }
        public bool IsNewerThan(Versioning version)
        {
            if (this.major > version.major)
            {
                return true;
            }
            else if (this.major == version.major)
            {
                if (this.minor > version.minor)
                {
                    return true;
                }
                else if (this.minor == version.minor && this.patch > version.patch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
